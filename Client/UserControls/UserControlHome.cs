using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace corretto.UserControls
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
        }
        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            string serverAddress = "127.0.0.1"; //IP del server
            int serverPort = 12345; //porta del server

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

            //gestione thread
            Task t = Task.Factory.StartNew(() =>
            {
                dataReceived = udpClient.Receive(ref riceveEP);
            });
            await t;

            String risposta = Encoding.ASCII.GetString(dataReceived);

            //gestisce la risposta del server
            if (risposta.StartsWith("Benvenuto"))
            {
                //server assegna id es: Benvenuto;1
                //split explode controllo e lo mette in una variabile per tutta la durata (stessa cosa da fare con il personaggio)
                //dati client deve tenere le sue informazioni, il server prende solo quelle dell'altro


                //il server ha accettato il client
                OnPlayClicked();
            }
            else if (risposta.Equals("Server pieno"))
            {
                //mostra un messaggio se il server è pieno
                MessageBox.Show("Server pieno. Riprova più tardi.");
            }
            else
            {
                //messaggio di errore per risposte non valide
                MessageBox.Show("Errore di connessione al server.");
            }
        }

        protected virtual void OnPlayClicked()
        {
            if (PlayClicked != null)
            {
                PlayClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
