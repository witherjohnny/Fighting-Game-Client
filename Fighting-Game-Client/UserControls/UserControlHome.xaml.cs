using Fighting_Game_Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
    //schermata iniziale con il pulsante "Play"
    //usato TableLayoutPanel per gestire meglio la grafica con il cambio di schermo
    //pulsante bloccato al centro e dock tabella bloccato su tutta la pagina, impostata di base al max
    public partial class UserControlHome : UserControl
    {
        public event EventHandler PlayClicked;
        public UserControlHome()
        {
            InitializeComponent();
            stilePulsantePlayIniziale();
        }
        private void stilePulsantePlayIniziale()
        {
            
            // Accessing the button from XAML
            buttonPlay.Content = "PLAY";

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
            
        }

        protected virtual void OnPlayClicked()
        {
            if (PlayClicked != null)
            {
                PlayClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            string serverAddress = ServerSettings.Ip; //IP del server
            int serverPort = (int)ServerSettings.Port; //porta del server

            //scrittura
            //usa UdpClient per la comunicazione
            UdpClient udpClient = UdpClientSingleton.Instance;

            //messaggio di richiesta al server
            string messaggio = "Join";
            byte[] data = Encoding.ASCII.GetBytes(messaggio);

            //invia il messaggio al server
            udpClient.Send(data, data.Length, serverAddress, serverPort);


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
                if (risposta.StartsWith("Benvenuto"))
                {
                    //server assegna id es: Benvenuto;1
                    //split explode controllo e lo mette in una variabile per tutta la durata (stessa cosa da fare con il personaggio)
                    //dati client deve tenere le sue informazioni, il server prende solo quelle dell'altro


                    //il server ha accettato il client
                    OnPlayClicked();
                    break;
                }
                else if (risposta.Equals("Server pieno"))
                {
                    //mostra un messaggio se il server è pieno
                    MessageBox.Show("Server pieno. Riprova più tardi.");
                    break;
                }
                else
                {
                    //messaggio di errore per risposte non valide
                    MessageBox.Show("Errore di connessione al server.");
                }

            }

        }
    }
}
