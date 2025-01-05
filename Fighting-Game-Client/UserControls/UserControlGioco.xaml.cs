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
using System.Windows.Markup;
using System.Linq;
using System.Threading;

namespace Fighting_Game_Client.UserControls
{
    public partial class UserControlGioco : UserControl
    {
        private Player playerLocal = null; // player locale controllato dall'utente
        private Player playerRemote = null; // player remoto aggiornato dal server

        private const int FrameRate = 60; //frame al secondo (FPS)

        private UdpClient client = UdpClientSingleton.Instance;
        private List<Rect> obstacles = new List<Rect>(); //lista di ostacoli (pavimento)
        private List<HitBox> hitboxes = new List<HitBox>(); 
        private List<HitBox> remoteHitboxes = new List<HitBox>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public UserControlGioco()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.Focus();
            //avvia la comunicazione con il server
            ReceivePlayerData();
            disegnaPavimento();
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
            // Percorso dell'immagine di sfondo del pavimento
            string immaginePavimento = System.IO.Path.Combine("Images", "Floor", "pavimento1.png");

            // Verifica se il file esiste
            if (!File.Exists(immaginePavimento))
            {
                MessageBox.Show($"Immagine del pavimento non trovata: {immaginePavimento}", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Dimensioni del pavimento
            int larghezzaPavimento = (int)(this.ActualWidth * 0.6); // Larghezza del pavimento: 60% della larghezza totale
            int altezzaPavimento = 141; // Altezza fissa del pavimento
            int yPavimento = (int)(this.ActualHeight - altezzaPavimento - 50); // Posizione verticale (in fondo alla finestra)
            int xPavimento = (int)((this.ActualWidth - larghezzaPavimento) / 2); // Posizione orizzontale centrata

            // Crea l'immagine del pavimento come un unico blocco
            Image pavimento = new Image
            {
                Width = larghezzaPavimento,  // Larghezza del pavimento
                Height = altezzaPavimento,  // Altezza del pavimento
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Stretch = Stretch.Fill // Ridimensiona l'immagine per coprire tutto il blocco
            };

            // Carica l'immagine del pavimento
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(immaginePavimento, UriKind.RelativeOrAbsolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();

            pavimento.Source = bitmap;

            // Posiziona l'immagine del pavimento sul Canvas
            Canvas.SetLeft(pavimento, xPavimento); // Posiziona il pavimento centrato orizzontalmente
            Canvas.SetTop(pavimento, yPavimento); // Posizione verticale (in basso)

            // Aggiungi il pavimento al Canvas
            canvas.Children.Add(pavimento);

            // Aggiungi l'ostacolo solo per l'area del pavimento
            obstacles.Add(new Rect(xPavimento, yPavimento, larghezzaPavimento, altezzaPavimento));
        }

        private void InitializeGame()
        {
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
                if (!playerLocal.getCharacterBox().isAnimating())
                {
                    playerLocal.setAnimation("Idle", playerLocal.getDirection(), false, true);
                }
                playerLocal.Update(obstacles);
            }
            SendPlayerData();
            SendHitboxesData();
        }


        private async void ReceivePlayerData()
        {
            UdpClient udpClient = UdpClientSingleton.Instance;

            string messaggio = "GameLoaded";
            byte[] bytes = Encoding.ASCII.GetBytes(messaggio);

            udpClient.Send(bytes, bytes.Length, ServerSettings.Ip, (int)ServerSettings.Port);
            await Task.Run(() =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);
                        byte[] dataReceived = client.Receive(ref receiveEP);
                        string serverResponse = Encoding.ASCII.GetString(dataReceived);
                        if(serverResponse.StartsWith("Hurt"))
                        {
                            playerLocal.setAnimation("Hurt", playerLocal.getDirection(), true, false);
                        }
                        
                        string[] allData = serverResponse.Split('\n');

                        List<HitBox> necessaryHitboxes = new List<HitBox>();
                        string typeOfMessage = null;
                        foreach (string riga in allData)
                        {
                            if(typeOfMessage == null)
                            {
                                typeOfMessage = riga;
                                continue;
                            }
                            string[] data = riga.Split(';');
                            if (typeOfMessage == "playerInfo")
                            {
                                bool isLocal = data[0] == "local" ? true : false;
                                string id = data[1];
                                int x = int.Parse(data[2]);
                                int y = int.Parse(data[3]);
                                string character = data[4];
                                Direction direction = data[5] == "Right" ? Direction.Right : Direction.Left;
                                string action = data[6];
                                int health = int.Parse(data[7]);
                                int width = int.Parse(data[8]);
                                int height = int.Parse(data[9].Trim());

                                this.Dispatcher.Invoke(() =>
                                {
                                    if (playerLocal == null && isLocal)
                                    {
                                        PlayerHitBox playerHitbox = new PlayerHitBox(id, x, y, width, height);

                                        playerLocal = new Player(id, character, x, y, direction,playerHitbox);
                                        playerLocal.Health = health;
                                        progressBarVitaGiocatore1.Value = health;
                                        label1.Content = $"P1: {character}";
                                        canvas.Children.Add(playerLocal.getCharacterBox());
                                    }
                                    else if (playerRemote == null && !isLocal)
                                    {
                                        PlayerHitBox playerHitbox = new PlayerHitBox(id, x, y, width, height);

                                        playerRemote = new Player(id, character, x, y, direction, playerHitbox);
                                        label2.Content = $"P2: {character}";
                                        progressBarVitaGiocatore2.Value = health;
                                        playerRemote.Health = health;
                                        canvas.Children.Add(playerRemote.getCharacterBox());
                                    }
                                    else if (id == playerRemote.getId() && !isLocal)
                                    {
                                        playerRemote.setPosition(x, y);
                                        playerRemote.setAnimation(action, direction, false, true);
                                        progressBarVitaGiocatore2.Value = health;
                                    }
                                    else if (id == playerLocal.getId() && isLocal)
                                    {
                                        playerLocal.Health = health;
                                        progressBarVitaGiocatore1.Value = health;
                                    }
                                });
                            }else if(typeOfMessage == "hitboxes")
                            {
                                string id = data[0];
                                string name = data[1];
                                int x = int.Parse(data[2]);
                                int y = int.Parse(data[3]);
                                int width = int.Parse(data[4]);
                                int height = int.Parse(data[5]);
                                var hitbox = remoteHitboxes.Find(h => h.Id == id);
                                if (hitbox != null)
                                {
                                    necessaryHitboxes.Add(hitbox);
                                    hitbox.X = x;
                                    hitbox.Y = y;
                                    hitbox.Width = width;
                                    hitbox.Height = height;
                                }
                                else
                                {
                                    // Se la hitbox non esiste, aggiungine una nuova
                                    AttackHitBox h = new AttackHitBox(name, x, y, width, height);
                                    if(h.getAttackBox() != null)
                                        canvas.Children.Add( h.getAttackBox());
                                    remoteHitboxes.Add(h);
                                }
                            }
                        }
                        removeUnnecessaryHitboxes(necessaryHitboxes);
                       
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
        private void SendHitboxesData()
        {
            try
            {
                string message = "hitboxes\n";

                foreach (var hitbox in hitboxes)
                {
                    message+=hitbox.toCSV()+"\n";
                }
                message.Trim();
                if (hitboxes.Count >0 && client != null)
                {
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

            //MOVIMENTO
            //corsa a sinistra
            if (e.Key == Key.A)  
            {
                playerLocal.setAnimation("Run", Direction.Left, false, true);
                playerLocal.SpeedX = -5;
            }

            //corsa a destra
            else if (e.Key == Key.D)
            {
                playerLocal.SpeedX = 5;
                playerLocal.setAnimation("Run", Direction.Right, false, true);
            }

            //salto
            if (e.Key == Key.W && !playerLocal.isJumping) 
            {
                playerLocal.isJumping = true;
                playerLocal.SpeedY = 10;
                playerLocal.setAnimation("Jump", playerLocal.getDirection(), true, true);
            }

            //dash
            //se premi Shift e non sei già in dash
            if (e.Key == Key.LeftShift && !playerLocal.isDashing)
            {
                playerLocal.isDashing = true;  //abilita il dash
                playerLocal.setAnimation("Roll", playerLocal.getDirection(), true, true);  //attiva l'animazione del dash

                //se sta andando a sinistra, dash a sinistra
                if (playerLocal.getDirection() == Direction.Left)
                {
                    playerLocal.SpeedX = -10;  //velocità del dash verso sinistra
                }
                //se sta andando a destra, dash verso destra
                else
                {
                    playerLocal.SpeedX = 10;  //velocità del dash verso destra
                }
            }



            //ATTACCO
            //attacco 1    se warrior ravvicinato, se mago FIREBALL
            if (e.Key == Key.J)  
            {
                if (playerLocal.nome == "FireWizard")
                {
                    HandleFireballAttack();
                }
                else if(playerLocal.nome == "Warrior_2")
                {
                    HandleAttack1();
                }
            }

            //attacco 2    se warrior ravvicinato, se mago JET FLAME
            if (e.Key == Key.K) 
            {
                if (playerLocal.nome == "FireWizard")
                {
                    //da cambiare se si vuole aggiungere flame jet, ma sprite player e attacco sono insieme
                    HandleAttack2();
                }
                else if (playerLocal.nome == "Warrior_2")
                {
                    HandleAttack2();
                }
            }

            
        }

        private void HandleAttack1()
        {
            playerLocal.setAnimation("Attack_1", playerLocal.getDirection(), true, true);
            // Create a new hitbox for the attack
            int hitboxX = playerLocal.X + (playerLocal.getDirection() == Direction.Right ? 50 : -50);
            int hitboxY = playerLocal.Y + 10;
            int hitboxWidth = 50;
            int hitboxHeight = 20;

            AttackHitBox hitbox = new AttackHitBox("Attack_1", hitboxX, hitboxY, hitboxWidth, hitboxHeight);
            Canvas.SetLeft(hitbox.getAttackBox(), hitboxX);
            Canvas.SetTop(hitbox.getAttackBox(), hitboxY);
            // Add hitbox to the canvas
            if (hitbox.getAttackBox() != null)
                canvas.Children.Add(hitbox.getAttackBox());

            // Add hitbox to the local list
            hitboxes.Add(hitbox);

            // Remove the hitbox after a delay 
            Task.Delay(100).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    // Remove from canvas
                    if (hitbox.getAttackBox() != null)
                        canvas.Children.Remove(hitbox.getAttackBox());

                    // Remove from the hitbox list
                    hitboxes.Remove(hitbox);

                    UdpClient udpClient = UdpClientSingleton.Instance;
                    string messaggio = "removeHitbox;" + hitbox.Id;
                    byte[] bytes = Encoding.ASCII.GetBytes(messaggio);
                    udpClient.Send(bytes, bytes.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                });
            });
        }

        private void HandleAttack2()
        {
            playerLocal.setAnimation("Attack_2", playerLocal.getDirection(), true, true);
            // Create a new hitbox for the attack
            int hitboxX = playerLocal.X + (playerLocal.getDirection() == Direction.Right ? 50 : -50);
            int hitboxY = playerLocal.Y + 10;
            int hitboxWidth = 50;
            int hitboxHeight = 20;

            AttackHitBox hitbox = new AttackHitBox("Attack_2", hitboxX, hitboxY, hitboxWidth, hitboxHeight);
            Canvas.SetLeft(hitbox.getAttackBox(), hitboxX);
            Canvas.SetTop(hitbox.getAttackBox(), hitboxY);
            // Add hitbox to the canvas
            if (hitbox.getAttackBox() != null)
                canvas.Children.Add(hitbox.getAttackBox());

            // Add hitbox to the local list
            hitboxes.Add(hitbox);

            // Remove the hitbox after a delay 
            Task.Delay(100).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    // Remove from canvas
                    if (hitbox.getAttackBox() != null)
                        canvas.Children.Remove(hitbox.getAttackBox());

                    // Remove from the hitbox list
                    hitboxes.Remove(hitbox);

                    UdpClient udpClient = UdpClientSingleton.Instance;
                    string messaggio = "removeHitbox;" + hitbox.Id;
                    byte[] bytes = Encoding.ASCII.GetBytes(messaggio);
                    udpClient.Send(bytes, bytes.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                });
            });
        }

        private void HandleFireballAttack()
        {
            playerLocal.setAnimation("Fireball", playerLocal.getDirection(), true, true);
            // Create a new hitbox for the attack
            int hitboxX = playerLocal.X + (playerLocal.getDirection() == Direction.Right ? 5 : -5);
            int hitboxY = playerLocal.Y + 10;
            int hitboxWidth = 5;
            int hitboxHeight = 5;

            AttackHitBox hitbox = new AttackHitBox("Charge", hitboxX, hitboxY, hitboxWidth, hitboxHeight);
            Canvas.SetLeft(hitbox.getAttackBox(), hitboxX);
            Canvas.SetTop(hitbox.getAttackBox(), hitboxY);

            // Add hitbox to the canvas
            if (hitbox.getAttackBox() != null)
                canvas.Children.Add(hitbox.getAttackBox());

            // Add hitbox to the local list
            hitboxes.Add(hitbox);


            //direzione di movimento (indipendente dal player dopo il lancio)
            int fireballSpeed = 10 * (playerLocal.getDirection() == Direction.Right ? 2 : -2);

            //muovi la fireball per i frame dell'animazione
            int framesRemaining = 6;
            Task.Run(async () =>
            {
                while (framesRemaining > 0)
                {
                    await Task.Delay(100); //100ms per frame
                    this.Dispatcher.Invoke(() =>
                    {
                        //aggiorna la posizione della fireball
                        hitbox.X += fireballSpeed;

                        //aggiorna la posizione nella canvas
                        Canvas.SetLeft(hitbox.getAttackBox(), hitbox.X);
                        Canvas.SetTop(hitbox.getAttackBox(), hitbox.Y);

                        //riduci i frame rimanenti
                        framesRemaining--;
                    });
                }

                //dopo 6 frame, rimuovi la fireball
                this.Dispatcher.Invoke(() =>
                {
                    if (hitbox.getAttackBox() != null)
                        canvas.Children.Remove(hitbox.getAttackBox());

                    hitboxes.Remove(hitbox);

                    //comunica la rimozione della fireball al server
                    UdpClient udpClient = UdpClientSingleton.Instance;
                    string messaggio = "removeHitbox;" + hitbox.Id;
                    byte[] bytes = Encoding.ASCII.GetBytes(messaggio);
                    udpClient.Send(bytes, bytes.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                });
            });
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
        private void removeUnnecessaryHitboxes(List<HitBox> hitboxes)
        {
            // Crea un elenco temporaneo per raccogliere le hitbox da rimuovere
            List<AttackHitBox> hitboxesToRemove = new List<AttackHitBox>();

            foreach (var remoteHitbox in remoteHitboxes)
            {
                // Se la hitbox remota non è contenuta nella lista delle hitbox passate
                if (!hitboxes.Any(h => h.Id == remoteHitbox.Id))
                {
                    // Aggiungi la hitbox remota all'elenco delle hitbox da rimuovere
                    hitboxesToRemove.Add((AttackHitBox)remoteHitbox);
                }
            }

            // Rimuovi dal canvas e dalla lista remoteHitboxes le hitbox non necessarie
            foreach (var hitboxToRemove in hitboxesToRemove)
            {
                // Se la hitbox ha un elemento grafico associato, rimuovilo dal canvas
                if (hitboxToRemove.getAttackBox() != null)
                {
                    canvas.Children.Remove(hitboxToRemove.getAttackBox());
                }

                // Rimuovi la hitbox dalla lista remoteHitboxes
                remoteHitboxes.Remove(hitboxToRemove);
            }
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            

            // Cancel the token to stop the task
            _cancellationTokenSource.Cancel();

            // Dispose of the UdpClient if needed
            client?.Close();
        }
    }
}