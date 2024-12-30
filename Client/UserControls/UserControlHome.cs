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
using FightingGameClient.Data;

namespace FightingGameClient.UserControls
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
            // imposta il testo
            buttonPlay.Text = "PLAY";
            buttonPlay.Font = new Font("Times New Roman", 20, FontStyle.Bold); 
            buttonPlay.ForeColor = Color.White; // testo bianco
            buttonPlay.BackColor = Color.LightBlue; // sfondo azzurro
            buttonPlay.FlatStyle = FlatStyle.Flat; // stile piatto
            buttonPlay.FlatAppearance.BorderSize = 0; // senza bordo

            // bordi arrotondati
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(10, 10, 30, 30, 180, 90); // angolo superiore sinistro
            path.AddArc(buttonPlay.Width - 40, 10, 30, 30, 270, 90); // angolo superiore destro
            path.AddArc(buttonPlay.Width - 40, buttonPlay.Height - 40, 30, 30, 0, 90); // angolo inferiore destro
            path.AddArc(10, buttonPlay.Height - 40, 30, 30, 90, 90); // angolo inferiore sinistro
            path.CloseAllFigures();
            buttonPlay.Region = new Region(path);

            // aggiungi un'ombra per dare profondità
            buttonPlay.FlatAppearance.MouseOverBackColor = Color.Blue; // cambia colore al passaggio del mouse

            // evento hover per un bordo bianco
            buttonPlay.MouseEnter += (s, e) =>
            {
                buttonPlay.FlatAppearance.BorderSize = 2;
                buttonPlay.FlatAppearance.BorderColor = Color.White; // bordo bianco
            };
            buttonPlay.MouseLeave += (s, e) =>
            {
                buttonPlay.FlatAppearance.BorderSize = 0; // rimuove il bordo
            };
        }

        private async void buttonPlay_Click(object sender, EventArgs e)
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

            //gestione thread
            Task t = Task.Factory.StartNew(() =>
            {
                try
                {
                    dataReceived = udpClient.Receive(ref riceveEP);
                }catch(Exception ex) {
                    MessageBox.Show("Errore :" + ex.ToString());
                }
            });
            await t;

            if(dataReceived == null)
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
                PlayClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
