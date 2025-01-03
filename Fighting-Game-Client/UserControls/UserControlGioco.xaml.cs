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

        private const int FrameRate = 1; //frame al secondo (FPS)

        private UdpClient client = UdpClientSingleton.Instance;
        private List<Rect> obstacles; //lista di ostacoli (pavimento)
        private bool isJumping = false; //stato del salto
        private bool isMoving = false; //stato del movimento orizzontale
        private bool isFalling = false; //stato per la discesa

        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            InitializeGame();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
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
            for (int i = 0; i<numeroBlocchiPavimento; i++)
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
                Canvas.SetLeft(floorBlock, spazioLato + i* larghezzaBlocco);

                //posizione Y fissa (il pavimento è sempre in fondo alla finestra)
                Canvas.SetTop(floorBlock, yPavimento);

                //aggiungi ogni blocco al Canvas
                canvas.Children.Add(floorBlock);
            }
        }

        private void InitializeGame()
        {
            this.Width = 800;
            this.Height = 600;

            playerLocal = new Player("local", personaggio, 100, 300, Direction.Right); //posizione iniziale a sinistra
            canvas.Children.Add(playerLocal.getCharacterBox());

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

            this.KeyDown += UserControlGioco_KeyDown;
            this.KeyUp += UserControlGioco_KeyUp;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (isJumping)
            {
                HandleJump();
            }

            if (isMoving)
            {
                SendPlayerData();
            }

            if (isFalling)
            {
                HandleFall();
            }

            ReceivePlayerData();
        }

        private void UserControlGioco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)  //movimento a sinistra
            {
                isMoving = true;
                playerLocal.MoveLeft(obstacles);
                SendPlayerData();
            }
            else if (e.Key == Key.D)  //movimento a destra
            {
                isMoving = true;
                playerLocal.MoveRight(obstacles);
                SendPlayerData();
            }
            else if (e.Key == Key.W && !isJumping)  //salto
            {
                isJumping = true;
                playerLocal.Jump(obstacles);
                SendPlayerData();
            }
            else if (e.Key == Key.S)  //discesa
            {
                isFalling = true;
            }
        }

        private void UserControlGioco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.D)
            {
                isMoving = false;
                playerLocal.setAnimation("Idle", playerLocal.getCharacterBox().getDirection(), true, true);
            }

            if (e.Key == Key.S)
            {
                isFalling = false;
            }
        }

        private async void ReceivePlayerData()
        {
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
                                if (playerLocal == null && id == "local")
                                {
                                    playerLocal = new Player(id, character, 100, 300, Direction.Right);
                                    canvas.Children.Add(playerLocal.getCharacterBox());
                                }
                                else if (playerRemote == null && id != "local")
                                {
                                    playerRemote = new Player(id, character, 600, 300, Direction.Left);
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

        private void SendPlayerData()
        {
            try
            {
                if (playerLocal != null && client != null)
                {
                    string message = "playerInfo;playerLocal.X;playerLocal.Y;playerLocal.getCharacterBox().getDirection();playerLocal.getCharacterBox().getCurrentAnimation()";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    client.Send(data, data.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                }
            }
            catch (Exception ex) { 
                Console.WriteLine("Errore durante l'invio dei dati: {ex.Message}");
            }
        }

        private bool CheckCollisionWithTerrain(Rect playerBox)
        {
            foreach (var terrain in obstacles)
            {
                if (playerBox.IntersectsWith(terrain))
                {
                    return true;
                }
            }
            return false;
        }

        private void HandleJump()
        {
            if (playerLocal.Y > 300)
            {
                playerLocal.Y -= 5; //salita durante il salto
                playerLocal.setAnimation("Jump", playerLocal.getCharacterBox().getDirection(), false, true);
            }

            else
            {
                isJumping = false;
                isFalling = true;
                playerLocal.setAnimation("Fall", playerLocal.getCharacterBox().getDirection(), false, true);
            }
        }

        private void HandleFall()
        {
            if (playerLocal.Y < 550)
            {
                playerLocal.Y += 5;  //discesa con gravità
            }
            else
            {
                playerLocal.Y = 550; //allinea al terreno
                isFalling = false;
                playerLocal.setAnimation("Idle", playerLocal.getCharacterBox().getDirection(), true, true); //torna in Idle
            }
        }
    }
}