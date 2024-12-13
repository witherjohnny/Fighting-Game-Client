using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corretto.UserControls
{
    //schermata per selezionare il personaggio
    public partial class UserControlSelezionePersonaggio : UserControl
    {
        public event EventHandler PlayClicked;

        public UserControlSelezionePersonaggio()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void UserControlSelezionePersonaggio_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxStickman_Click(object sender, EventArgs e)
        {
            if (pictureBoxStickman.Image != null)
            {
                String nomePersonaggio = pictureBoxStickman.AccessibleDescription;

                labelNomeGiocatoreSelezionato.Text = nomePersonaggio;
                pictureBoxVisualizzaPersonaggio.Image = pictureBoxStickman.Image;

                //abilita il pulsante per giocare
                buttonPlay.Enabled = true;
            }
            else
            {
                MessageBox.Show("L'immagine non è stata caricata correttamente.", "Errore");
            }
        }


        private void pictureBoxVisualizzaPersonaggio_Click(object sender, EventArgs e)
        {

        }

        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            //vuol dire che il personaggio è stato confermato, si comunica al server il personaggio selezionato+
            String personaggio = labelNomeGiocatoreSelezionato.Text;
            String messaggio = "ready;" + personaggio;

            //lo manda al server
            string serverAddress = "127.0.0.1"; //IP del server
            int serverPort = 12345; //porta del server

            //scrittura
            //usa UdpClient per la comunicazione
            UdpClient udpClient = new UdpClient();

            //messaggio di richiesta al server
            byte[] data = Encoding.ASCII.GetBytes(messaggio);

            //invia il messaggio al server
            udpClient.Send(data, data.Length, serverAddress, serverPort);


            //aspetta dal server la conferma per poter giocare
            // (che entrambi i giocatori hanno scelto)

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
            if(risposta == "Gioco iniziato")
            {
                //il server ha accettato il client
                OnPlayClicked();
            }

        }

        protected virtual void OnPlayClicked()
        {
            if (PlayClicked != null)
            {
                PlayClicked.Invoke(this, EventArgs.Empty);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxPersonaggio1_Click(object sender, EventArgs e)
        {
            if (pictureBoxPersonaggio1.Image != null)
            {
                String nomePersonaggio = pictureBoxPersonaggio1.AccessibleDescription;

                labelNomeGiocatoreSelezionato.Text = nomePersonaggio;
                pictureBoxVisualizzaPersonaggio.Image = pictureBoxPersonaggio1.Image;

                //abilita il pulsante per giocare
                buttonPlay.Enabled = true;
            }
            else
            {
                MessageBox.Show("L'immagine non è stata caricata correttamente.", "Errore");
            }
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
