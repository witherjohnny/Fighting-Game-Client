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
            //connessione al server
            //controllo con server
            //scrittura
            UdpClient client = new UdpClient();
            string message = "Join";

            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data, data.Length, "localhost", 12345);


            //lettura
            IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] dataReceived = null;

            //gestione thread
            
            Task t = Task.Factory.StartNew(() =>
            {
                dataReceived = client.Receive(ref riceveEP);
            });
            await t;

            String risposta = Encoding.ASCII.GetString(dataReceived);


            //se risposta inizia con...
            if (risposta.StartsWith("Benvenuto"))
            {
                if (PlayClicked != null)
                {
                    PlayClicked.Invoke(this, EventArgs.Empty);
                }
            }
            else if (risposta.Equals("Server pieno"))
            {
                MessageBox.Show("Server pieno. Riprova più tardi.");
            }
            else
            {
                MessageBox.Show("Errore di connessione al server.");
            }
        }

        private void buttonPlay_Click_1(object sender, EventArgs e)
        {
            OnPlayClicked();
        }

        protected virtual void OnPlayClicked()
        {
            if (PlayClicked != null)
            {
                PlayClicked.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
