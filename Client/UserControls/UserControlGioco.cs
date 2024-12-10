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
    public partial class UserControlGioco : UserControl
    {
        private string personaggio;
        private UdpClient client;
        private IPEndPoint serverEndpoint;
        public UserControlGioco()
        {
            InitializeComponent();
            //prendere dal server personaggio

            client = new UdpClient();
            serverEndpoint = new IPEndPoint(IPAddress.Parse("localhost"), 12345);

            this.KeyDown += OnKeyDown;
            StartReceiving();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Invio movimento al server
            string movimento = string.Empty;

            switch (e.KeyCode)
            {
                case Keys.W: movimento = "0;-1"; break; // Su
                case Keys.S: movimento = "0;1"; break; // Giù
                case Keys.A: movimento = "-1;0"; break; // Sinistra
                case Keys.D: movimento = "1;0"; break; // Destra
            }

            if (!string.IsNullOrEmpty(movimento))
            {
                byte[] data = Encoding.ASCII.GetBytes(movimento);
                client.Send(data, data.Length, serverEndpoint);
            }
        }

        private async void StartReceiving()
        {
            while (true)
            {
                var response = await client.ReceiveAsync();
                string message = Encoding.ASCII.GetString(response.Buffer);

                // Aggiorna posizione degli altri giocatori
                string[] lines = message.Split('\n');
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(';');
                        string id = data[0];
                        float x = float.Parse(data[1]);
                        float y = float.Parse(data[2]);

                        // TODO: Aggiorna posizione dei giocatori sulla mappa
                    }
                }
            }
        }

        private void UserControlGioco_Load(object sender, EventArgs e)
        {

        }
    }
}
