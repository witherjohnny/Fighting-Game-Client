﻿using Fighting_Game_Client.Data;
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
                while (true)
                {
                    try
                    {
                        IPEndPoint receiveEP = new IPEndPoint(IPAddress.Any, 0);
                        byte[] dataReceived = client.Receive(ref receiveEP);
                        string serverResponse = Encoding.ASCII.GetString(dataReceived);
                        string typeOfMessage = null;

                        string[] allData = serverResponse.Split('\n');
                        foreach(string riga in allData)
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
                                    }
                                    else if (id == playerLocal.getId() && isLocal)
                                    {
                                        playerLocal.Health = health;
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
            if (e.Key == Key.A)  //corsa a sinistra
            {
                playerLocal.setAnimation("Run", Direction.Left, false, true);
                playerLocal.SpeedX = -5;
            }
            else if (e.Key == Key.D)  //corsa a destra
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


            if (e.Key == Key.J)  //attacco 1    se warrior ravvicinato, se mago lontano
            {
                if (playerLocal.nome == "FireWizard")
                {
                    playerLocal.setAnimation("Fireball", playerLocal.getDirection(), true, true);
                }
                else if(playerLocal.nome == "Warrior_2")
                {
                    playerLocal.setAnimation("Attack_1", playerLocal.getDirection(), true, true);
                }
            }
            
            /*if (e.Key == Key.K)  //attacco 2 wizard flame
            {
                if (playerLocal.nome == "FireWizard")
                {
                    playerLocal.setAnimation("Flame_jet", playerLocal.getDirection(), true, true);
                }
                else if (playerLocal.nome == "Warrior_2")
                {
                    playerLocal.setAnimation("Attack_2", playerLocal.getDirection(), true, true);
                }
            }*/

            if (e.Key == Key.LeftShift && !playerLocal.isDashing)  //dash
            {
                playerLocal.isDashing = true;
                playerLocal.setAnimation("Roll", playerLocal.getDirection(), true, true);
                //se sta andando a sinistra, dash a sinistra
                if(playerLocal.getDirection() == Direction.Left)
                {
                    playerLocal.SpeedX = -5;
                }
                //se verso destra dash verso destra
                else
                {
                    playerLocal.SpeedX = 5;
                }
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