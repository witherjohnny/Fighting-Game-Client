using corretto.UserControls;
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

namespace corretto
{
    //la finestra principale che gestisce i UserControl
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // carica il primo controllo utente
            LoadHome();
        }
        private void LoadHome()
        {
            //creazione controllo utente schermata home
            var home = new UserControlHome();
            //occupare tutto lo spazio disponibile nel pannello
            home.Dock = DockStyle.Fill;

            //all'evento del pulsante play cliccato, viene associata funzione
            home.PlayClicked += (s, e) => LoadSelezionePersonaggio();
     
            panelContainer.Controls.Clear();

            //mette controllo pagina home nel panel del form principale
            panelContainer.Controls.Add(home);
        }

        private void LoadSelezionePersonaggio()
        {
            //creazione controllo utente schermata selezione personaggi
            var selezionePersonaggio = new UserControlSelezionePersonaggio();
            //occupare tutto lo spazio disponibile nel pannello
            selezionePersonaggio.Dock = DockStyle.Fill;

            selezionePersonaggio.PersonaggioSelezionato += (s, personaggio) => LoadGioco(personaggio);
            
            //vengono tolti i controlli precedenti
            panelContainer.Controls.Clear();
            
            //e viene aggiunto il controllo utente selezione personaggio
            panelContainer.Controls.Add(selezionePersonaggio);
        }

        private void LoadGioco(string personaggio)
        {
            //creazione controllo utente schermata di gioco
            var gioco = new UserControlGioco(personaggio);
            //occupare tutto lo spazio disponibile nel pannello
            gioco.Dock = DockStyle.Fill;
            //vengono tolti i controlli precedenti
            panelContainer.Controls.Clear();
            
            //e viene aggiunto il controllo utente del gioco
            panelContainer.Controls.Add(gioco);
        }

        private void StartListeningForServerMessages()
        {
            UdpClient udpClient = new UdpClient(12345);  // Porta del server
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 12345);

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        // Riceve il messaggio dal server
                        byte[] receivedData = udpClient.Receive(ref remoteEndPoint);
                        string message = Encoding.UTF8.GetString(receivedData);

                        // Gestisce il messaggio ricevuto
                        HandleServerResponse(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Errore durante la ricezione del messaggio: " + ex.Message);
                    }
                }
            });
        }

        private void HandleServerResponse(string message)
        {
            if (message == "Gioco iniziato")
            {
                // Supponiamo che il server invii anche il nome del personaggio, per esempio "Mario" o "Luigi".
                // Questo è solo un esempio, in base a come hai deciso di strutturare il messaggio del server.
                string personaggio = "Mario";  // Esempio, devi ottenere il nome del personaggio dal messaggio
                LoadGioco(personaggio);  // Passiamo il nome del personaggio alla schermata di gioco
            }
            else
            {
                // Gestisci altre risposte, come errori o aggiornamenti di stato
                MessageBox.Show("Messaggio ricevuto: " + message);
            }
        }


        private void UpdateGameState(string gameData)
        {
            // Assumendo che il messaggio contenga dati separati da ";"
            string[] playerData = gameData.Split('\n');
            foreach (string playerInfo in playerData)
            {
                string[] data = playerInfo.Split(';');
                if (data.Length == 3)
                {
                    string playerId = data[0];
                    float x = float.Parse(data[1]);
                    float y = float.Parse(data[2]);

                    // Aggiorna la posizione del giocatore sulla mappa o nel gioco
                    UpdatePlayerPosition(playerId, x, y);
                }
            }
        }

        private void UpdatePlayerPosition(string playerId, float x, float y)
        {
            // Aggiorna la posizione del giocatore sulla UI (ad esempio, spostare un PictureBox o un altro controllo che rappresenta il giocatore)
            var playerControl = FindPlayerControl(playerId);
            if (playerControl != null)
            {
                playerControl.Left = (int)x;
                playerControl.Top = (int)y;
            }
        }

        private Control FindPlayerControl(string playerId)
        {
            // Cerca un controllo associato all'ID del giocatore nella UI (ad esempio, un PictureBox)
            return this.Controls.Cast<Control>().FirstOrDefault(c => c.Name == playerId);
        }




        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
