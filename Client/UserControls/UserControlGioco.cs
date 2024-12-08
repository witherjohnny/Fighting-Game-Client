using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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
        public UserControlGioco(string personaggioSelezionato)
        {
            InitializeComponent();

            personaggio = personaggioSelezionato;

            client = new UdpClient();
            serverEndpoint = new IPEndPoint(IPAddress.Parse("localhost"), 12345);

            this.KeyDown += OnKeyDown;
            StartReceiving();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //invio movimento al server
            string movimento = string.Empty;

            if (e.KeyCode == Keys.W)
            {
                movimento = "0;-1"; // su
            }
            else if (e.KeyCode == Keys.S)
            {
                movimento = "0;1"; // giù
            }
            else if (e.KeyCode == Keys.A)
            {
                movimento = "-1;0"; // sinistra
            }
            else if (e.KeyCode == Keys.D)
            {
                movimento = "1;0"; // destra
            }

            //se movimento fatto da client
            if (!string.IsNullOrEmpty(movimento))
            {
                //lo manda al server
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

                //aggiorna posizione degli altri giocatori
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
    }
}
