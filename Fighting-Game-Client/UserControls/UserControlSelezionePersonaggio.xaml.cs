using Fighting_Game_Client.Data;
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

namespace Fighting_Game_Client.UserControls
{
    //schermata per selezionare il personaggio
    public partial class UserControlSelezionePersonaggio : UserControl
    {
        public event EventHandler PlayClicked;

        private string personaggioScelto = null;
        private bool isReady = false;
        protected virtual void OnPlayClicked()
        {
            if (PlayClicked != null)
            {
                PlayClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public UserControlSelezionePersonaggio()
        {
            InitializeComponent();
            stileButtonPlay();
        }

        private void stileButtonPlay()
        {
            // Mouse hover effect
            buttonPlay.MouseEnter += (s, e) =>
            {
                buttonPlay.BorderBrush = new SolidColorBrush(Colors.White);
                buttonPlay.BorderThickness = new Thickness(2);
            };

            buttonPlay.MouseLeave += (s, e) =>
            {
                buttonPlay.BorderBrush = null;
                buttonPlay.BorderThickness = new Thickness(0);
            };
            buttonPlay.Content = "PLAY";
            buttonPlay.FontSize = 18;
            buttonPlay.FontWeight = FontWeights.Bold;
            buttonPlay.Foreground = System.Windows.Media.Brushes.White;
            buttonPlay.Background = System.Windows.Media.Brushes.Red;
        }

        private void loadSelected(String nomePersonaggio)
        {
            try
            {
                buttonPlay.IsEnabled = true;
                // rimuovi eventuali controlli esistenti nel TableLayoutPanel
                PanelPersonaggio.Children.Clear();

                AnimationBox characterBox = new AnimationBox(nomePersonaggio, "Idle", 0, 0, PanelPersonaggio.ActualWidth, PanelPersonaggio.ActualHeight, Enums.Direction.Right);

                personaggioScelto = nomePersonaggio;
                PanelPersonaggio.Children.Add(characterBox);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel caricamento delle animazioni: {ex.Message}", "Errore");
            }

        }
        private void pictureBoxWarrior_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isReady)
            {
                // Example character selection process
                string nomePersonaggio = "Warrior_2"; // example
                labelNomeGiocatoreSelezionato.Content = "Warrior";
                loadSelected(nomePersonaggio);
            }
        }


        private void pictureBoxFireWizard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isReady)
            {
                string nomePersonaggio = "FireWizard"; // example
                labelNomeGiocatoreSelezionato.Content = "Fire Wizard";
                loadSelected(nomePersonaggio);
            }
        }
        private async void buttonPlay_Click_1(object sender, RoutedEventArgs e)
        {
            if (!isReady)
            {
                isReady =true;
                //vuol dire che il personaggio è stato confermato, si comunica al server il personaggio selezionato+
                String personaggio = personaggioScelto;
                String messaggio = "ready;" + personaggio;

                //lo manda al server
                string serverAddress = ServerSettings.Ip; //IP del server
                int serverPort = (int)ServerSettings.Port; //porta del server

                //scrittura
                //usa UdpClient per la comunicazione
                UdpClient udpClient = UdpClientSingleton.Instance;

                //messaggio di richiesta al server
                byte[] data = Encoding.ASCII.GetBytes(messaggio);

                //invia il messaggio al server
                udpClient.Send(data, data.Length, serverAddress, serverPort);

                buttonPlay.Background = System.Windows.Media.Brushes.Green;
                //aspetta dal server la conferma per poter giocare
                // (che entrambi i giocatori hanno scelto)

                //lettura
                IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

                byte[] dataReceived = null;
                while (true)
                {
                    //gestione thread
                    Task t = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            dataReceived = udpClient.Receive(ref riceveEP);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Errore :" + ex.ToString());
                        }
                    });
                    await t;

                    if (dataReceived == null)
                        return;
                    String risposta = Encoding.ASCII.GetString(dataReceived);

                    //gestisce la risposta del server
                    if (risposta == "Gioco iniziato")
                    {
                        OnPlayClicked(); // passa il personaggio selezionato
                        break;
                    }
                    else
                    {
                        Console.WriteLine("In attesa del secondo player");
                    }

                }

            }
            else
            {
                isReady= false;
                String messaggio = "not ready";

                //lo manda al server
                string serverAddress = ServerSettings.Ip; //IP del server
                int serverPort = (int)ServerSettings.Port; //porta del server

                //scrittura
                //usa UdpClient per la comunicazione
                UdpClient udpClient = UdpClientSingleton.Instance;

                //messaggio di richiesta al server
                byte[] data = Encoding.ASCII.GetBytes(messaggio);

                //invia il messaggio al server
                udpClient.Send(data, data.Length, serverAddress, serverPort);
                buttonPlay.Background = System.Windows.Media.Brushes.Red;

            }


        }


    }
}
