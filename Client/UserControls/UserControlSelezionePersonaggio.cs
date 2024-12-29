using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FightingGameClient.UserControls
{
    //schermata per selezionare il personaggio
    public partial class UserControlSelezionePersonaggio : UserControl
    {
        public event Action<string> PlayClicked; // Usa un Action con un parametro stringa
        private string personaggioScelto = null;
        protected virtual void OnPlayClicked(string personaggio)
        {
            if (PlayClicked != null)
            {
                PlayClicked?.Invoke(personaggio);
            }
        }

        public UserControlSelezionePersonaggio()
        {
            InitializeComponent();
        }
        private void loadSelected(String nomePersonaggio)
        {
            //configura il percorso delle animazioni
            string spriteFolderPath = "Sprites/" + nomePersonaggio + "/Idle";

            //ANIMAZIONE IDLE 
            try
            {
                // rimuovi eventuali controlli esistenti nel TableLayoutPanel
                PanelPersonaggio.Controls.Clear();

                CharacterAnimation animationControl = new CharacterAnimation(nomePersonaggio, PanelPersonaggio.Width, PanelPersonaggio.Height);

                //configura il controllo per caricare i frame e avviare l'animazione
                animationControl.LoadFrames(spriteFolderPath);
                animationControl.Visible = true;
                animationControl.StartAnimation();
                personaggioScelto = nomePersonaggio;
                PanelPersonaggio.Controls.Add(animationControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel caricamento delle animazioni: {ex.Message}", "Errore");
            }
            
        }

        private void pictureBoxWarrior_2_Click(object sender, EventArgs e)
        {
            if (pictureBoxWarrior_2.Image != null)
            {
                //nome del personaggio selezionato
                string nomePersonaggio = pictureBoxWarrior_2.AccessibleDescription;

                //aggiorna il nome del personaggio selezionato
                labelNomeGiocatoreSelezionato.Text = "Warrior";
                loadSelected(nomePersonaggio);
                //abilita il pulsante "Play"
                buttonPlay.Enabled = true;

            }
            else
            {
                MessageBox.Show("L'immagine non è stata caricata correttamente.", "Errore");
            }

}


        private void pictureBoxWizard_Click(object sender, EventArgs e)
        {
            if (pictureBoxFireWizard.Image != null)
            {
                //nome del personaggio selezionato
                string nomePersonaggio = pictureBoxFireWizard.AccessibleDescription;

                //aggiorna il nome del personaggio selezionato
                labelNomeGiocatoreSelezionato.Text = "Fire Wizard";

                loadSelected(nomePersonaggio);
                //abilita il pulsante "Play"
                buttonPlay.Enabled = true;
            }
            else
            {
                MessageBox.Show("L'immagine non è stata caricata correttamente.", "Errore");
            }
        }

        private async void buttonPlay_Click(object sender, EventArgs e)
        {
            //vuol dire che il personaggio è stato confermato, si comunica al server il personaggio selezionato+
            String personaggio = personaggioScelto;
            String messaggio = "ready;" + personaggio;

            //lo manda al server
            string serverAddress = "127.0.0.1"; //IP del server
            int serverPort = 12345; //porta del server

            //scrittura
            //usa UdpClient per la comunicazione
            UdpClient udpClient = UdpClientSingleton.Instance;

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
                try
                {
                    dataReceived = udpClient.Receive(ref riceveEP);
                }catch(Exception ex)
                {
                    MessageBox.Show("Errore :"+ex.ToString());
                }
            });
            await t;

            if (dataReceived == null)
                return;
            String risposta = Encoding.ASCII.GetString(dataReceived);


            //gestisce la risposta del server
            if (risposta == "Gioco iniziato")
            {
                OnPlayClicked(personaggio); // passa il personaggio selezionato
            }
            else
            {
                MessageBox.Show("In attesa del secondo player");
            }
        }
    }
}