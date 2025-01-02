using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fighting_Game_Client.Data;
using System.IO;
using Fighting_Game_Client.Enums;

namespace Fighting_Game_Client.UserControls
{
    /// <summary>
    /// Logica di interazione per UserControlGioco.xaml
    /// </summary>
    public partial class UserControlGioco : UserControl
    {
        private string personaggio;

        //private Timer gameTimer; // timer per aggiornare il gioco
        private Player playerLocal = null; // player locale controllato dall'utente
        private Player playerRemote = null; // player remoto aggiornato dal server

        private const int FrameRate = 1; //frame al secondo (FPS)

        private UdpClient client = UdpClientSingleton.Instance;

        //costruttore del controllo
        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            InitializeGame();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
            //avvia la comunicazione con il server
            //ReceivePlayerData();
            disegnaPavimento();
            label1.Content = $"Personaggio selezionato: {personaggio}";
            //grafica
            stileLabels();

            //es rosso poca vita, verde tanta vita
            //stileProgressBars();
        }
        private void stileLabels()
        {
            label1.FontSize = 14;
            label1.FontWeight = FontWeights.Bold;
            label1.Foreground = new SolidColorBrush(Colors.White);
            label1.Background = Brushes.Transparent;
            label1.HorizontalContentAlignment = HorizontalAlignment.Center;

            label2.FontSize = 14;
            label2.FontWeight = FontWeights.Bold;
            label2.Foreground = new SolidColorBrush(Colors.White);
            label2.Background = Brushes.Transparent;
            label2.HorizontalContentAlignment = HorizontalAlignment.Center;
        }
        
        private void disegnaPavimento()
        {
            //percorso dell'immagine di sfondo del pavimento
            string immaginePavimento = System.IO.Path.Combine("Images", "Floor", "pavimento1.png");

            //verifica se il file esiste
            if (!File.Exists(immaginePavimento))
            {
                MessageBox.Show($"Immagine del pavimento non trovata: {immaginePavimento}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //dimensioni del pavimento
            int numeroBlocchiTotali = 10; //numero totale di blocchi
            int numeroBlocchiPavimento = numeroBlocchiTotali - 4; //blocchi effettivi (lasciando 2 vuoti per lato)
            int larghezzaBlocco = (int)(this.ActualWidth * 2 / numeroBlocchiTotali);
            int altezzaBlocco = 100; //altezza fissa del pavimento
            int yPavimento = (int)(this.ActualHeight - altezzaBlocco); //posizione verticale del pavimento
            int xInizioPavimento = larghezzaBlocco * 2; //inizio del pavimento (salta 2 blocchi a sinistra)
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(immaginePavimento, UriKind.RelativeOrAbsolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // Fully load image into memory
            bitmap.EndInit();
            for (int i = 0; i < numeroBlocchiPavimento; i++)
            {

                Image floorBlock = new Image
                {
                    Width = larghezzaBlocco,
                    Height = altezzaBlocco,
                    Source = bitmap,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };

               
                Canvas.SetLeft(floorBlock, xInizioPavimento + (i * larghezzaBlocco));
                Canvas.SetTop(floorBlock, yPavimento);

                // Add each block to the Canvas
                canvas.Children.Add(floorBlock);
            }
        }
        //inizializza la UI di gioco
        private void InitializeGame()
        {
            this.Width = 800;
            this.Height = 600;

            //inizializza i player
            playerLocal = new Player("local", personaggio, 100, 300, Direction.Right); // personaggio locale
            //coordinate e nome date dal server
            //playerRemote = new Player("Warrior_2", 500, 300, Direction.Left); // personaggio remoto

            canvas.Children.Add(playerLocal.getCharacterBox());
            //this.Controls.Add(playerRemote.getCharacterBox());

            //inizializza il timer del gioco
            System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / FrameRate)
            };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            //gestione eventi della tastiera
            this.KeyDown += UserControlGioco_KeyDown;
            this.KeyUp += UserControlGioco_KeyUp;
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //aggiorna lo stato dei player
            //playerLocal.Update();
            //playerRemote.Update();

            //test grafica movimento random
            //Random random = new Random();
            //// Generate two random numbers between 0 and 600
            //int number1 = random.Next(0, 601); // 601 because the upper bound is exclusive
            //int number2 = random.Next(0, 601);

            //playerLocal.setPosition(number1, number2);
            SendPlayerData();
        }
        //gestione del movimento con tastiera
        private void UserControlGioco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                playerLocal.X -= playerLocal.Speed;
            }
            else if (e.Key == Key.Right)
            {
                playerLocal.X += playerLocal.Speed;
            }
            else if (e.Key == Key.Space)
            {
                // Implement action for Space key
            }
        }
        private void UserControlGioco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Space)
            {
                // Implement action when keys are released
            }
        }
        private async void ReceivePlayerData()
        {
            //simula ricezione dati dal server in un thread separato
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);
                        byte[] dataReceived = client.Receive(ref receiveEP);
                        string serverResponse = Encoding.ASCII.GetString(dataReceived);

                        string[] data = serverResponse.Split(';');
                        if (data.Length == 6)
                        {
                            string id = data[0];
                            int x = int.Parse(data[1]);
                            int y = int.Parse(data[2]);
                            string character = data[3];
                            Direction direction = data[4] == "Right" ? Direction.Right : Direction.Left;
                            string action = data[5];

                            this.Dispatcher.Invoke(() =>
                            {
                                if (playerRemote == null)
                                {
                                    playerRemote = new Player(id, character, x, y, direction);
                                    canvas.Children.Add(playerRemote.getCharacterBox());
                                }

                                if (id == playerRemote.getId())
                                {
                                    playerRemote.setPosition(x, y);
                                    playerRemote.setAnimation(action, direction, false, true);
                                }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore nella comunicazione con il server: " + ex.Message);
                    }
                }
            });
        }

        //invia i dati del giocatore locale al server

        private void SendPlayerData()
        {

            try
            {
                if (playerLocal != null && client != null)
                {
                    string message = $"playerInfo;{playerLocal.X};{playerLocal.Y};{playerLocal.getCharacterBox().getDirection()};{playerLocal.getCharacterBox().getCurrentAnimation()}";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    client.Send(data, data.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante l'invio dei dati: {ex.Message}");
            }

        }

        
    }

}
