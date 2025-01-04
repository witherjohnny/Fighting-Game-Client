using Fighting_Game_Client.Data;
using Fighting_Game_Client.Enums;
using Fighting_Game_Client;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;
using System.Net;
using System.IO;

namespace Fighting_Game_Client.UserControls
{
    public partial class UserControlGioco : UserControl
    {
        private string personaggio;
        private Player playerLocal = null; // player locale controllato dall'utente
        private Player playerRemote = null; // player remoto aggiornato dal server

        private const int FrameRate = 60; //frame al secondo (FPS)

        private UdpClient client = UdpClientSingleton.Instance;
        private List<Rect> obstacles = new List<Rect>(); //lista di ostacoli (pavimento)
        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            InitializeGame();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.Focus();
            //avvia la comunicazione con il server
            ReceivePlayerData();
            disegnaPavimento();
            label1.Content = $"Personaggio selezionato: {personaggio}";
            stileLabels();
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

            //numero fisso di blocchi
            int numeroBlocchiPavimento = 6;
            int larghezzaBlocco = 150;       //larghezza fissa di ciascun blocco
            int altezzaBlocco = 100;         //altezza fissa del pavimento
            int yPavimento = (int)(this.ActualHeight - altezzaBlocco);  //posizione verticale del pavimento (in fondo alla finestra)

            //crea l'immagine del pavimento
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(immaginePavimento, UriKind.RelativeOrAbsolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            //calcola lo spazio vuoto ai lati
            int spazioLato = (int)((this.ActualWidth - (numeroBlocchiPavimento * larghezzaBlocco)) / 2);

            //aggiungi i blocchi del pavimento uno accanto all'altro
            for (int i = 0; i < numeroBlocchiPavimento; i++)
            {
                Image floorBlock = new Image
                {
                    Width = larghezzaBlocco,  //imposta la larghezza del blocco
                    Height = altezzaBlocco,   //imposta l'altezza del blocco
                    Source = bitmap,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };

                //posizione X di ciascun blocco (spazio vuoto a sinistra e posizionamento dei blocchi)
                Canvas.SetLeft(floorBlock, spazioLato + i * larghezzaBlocco);

                //posizione Y fissa (il pavimento è sempre in fondo alla finestra)
                Canvas.SetTop(floorBlock, yPavimento);

                //aggiungi ogni blocco al Canvas
                canvas.Children.Add(floorBlock);
            }
            obstacles.Add(new Rect(0, 550, 800, 50));
        }

        private void InitializeGame()
        {
            

            obstacles = new List<Rect>
            {
                new Rect(0, 550, 800, 50) //pavimento (ostacolo)
            };

            System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / FrameRate)
            };
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if(playerLocal!= null)
            {
                playerLocal.Update(obstacles);
            }
            SendPlayerData();
        }


        private async void ReceivePlayerData()
        {
            UdpClient udpClient = UdpClientSingleton.Instance;

            string messaggio = "GameLoaded";
            byte[] bytes = Encoding.ASCII.GetBytes(messaggio);

            udpClient.Send(bytes, bytes.Length, ServerSettings.Ip, (int)ServerSettings.Port);
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
                        if (data.Length == 7)
                        {
                            bool isLocal = data[0] == "local" ? true : false; 
                            string id = data[1];
                            int x = int.Parse(data[2]);
                            int y = int.Parse(data[3]);
                            string character = data[4];
                            Direction direction = data[5] == "Right" ? Direction.Right : Direction.Left;
                            string action = data[6].Trim();

                            this.Dispatcher.Invoke(() =>
                            {
                                if (playerLocal == null && isLocal)
                                {
                                    playerLocal = new Player(id, character, x, y, direction);
                                    canvas.Children.Add(playerLocal.getCharacterBox());
                                }
                                else if (playerRemote == null && !isLocal)
                                {
                                    playerRemote = new Player(id, character, x, y, direction);
                                    canvas.Children.Add(playerRemote.getCharacterBox());
                                }
                                else if (id == playerRemote.getId() && !isLocal)
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


        private void canvas_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (playerLocal == null)
                return;
            if (e.Key == Key.A)  //movimento a sinistra
            {
                playerLocal.setAnimation("Run", Direction.Left, false, true);
                playerLocal.SpeedX = -5;
            }
            else if (e.Key == Key.D)  //movimento a destra
            {
                playerLocal.SpeedX = 5;
                playerLocal.setAnimation("Run", Direction.Right, false, true);
            }
            if (e.Key == Key.W && !playerLocal.isJumping)  //salto
            {
                playerLocal.isJumping = true;
                playerLocal.SpeedY = 10;
                playerLocal.setAnimation("Jump", playerLocal.getDirection(), true, true);
            }
        }

        private void canvas_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (playerLocal == null)
                return;
            if (e.Key == Key.A || e.Key == Key.D)
            {
                playerLocal.SpeedX = 0;
                playerLocal.setAnimation("Idle", playerLocal.getDirection(), false, true);

            }
        }
    }
}