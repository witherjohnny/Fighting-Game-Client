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
using System.IO;

namespace Client
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            labelErrore.Enabled = false;
            labelErrore.Text = "";
            //controllo con server
            //scrittura
            UdpClient client = new UdpClient();
            byte[] data = Encoding.ASCII.GetBytes("sto facendo una richiesta");
            client.Send(data, data.Length, "localhost", 12345);

            //lettura
            IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

            byte[] dataReceived = null;

            //gestione thread
            try{
                Task t = Task.Factory.StartNew(() =>
                {
                    dataReceived = client.Receive(ref riceveEP);
                });
                await t;
            }
            catch {
                labelErrore.Enabled = true;
                labelErrore.Text = "Errore, richiesta con il server fallita";
            }

            String risposta = Encoding.ASCII.GetString(dataReceived);
            if (risposta == "puoi entrare")
            {
                //crea una nuova istanza del GameForm
                GameForm gameForm = new GameForm();

                //mostra il nuovo form
                gameForm.Show();

                //chiude il form attuale
                this.Hide();
            }
            else
            {
                //mostra messaggio di errore
                labelErrore.Enabled = true;
                labelErrore.Text = "Errore, richiesta con il server fallita";
            }

        }
    }
}
