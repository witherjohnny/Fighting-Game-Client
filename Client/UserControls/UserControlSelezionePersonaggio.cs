using FightingGameClient.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
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
            stileLabelSelezionePersonaggio();
            stileLabelNomeGiocatoreSelezionato();
            stileLabelNomeGiocatoreSelezionato();
            stileButtonPlay();
        }

        private void stileLabelSelezionePersonaggio()
        {
            //configura il testo
            label1.Text = "Selezione Personaggio";
            label1.Font = new Font("Arial", 24, FontStyle.Bold); //font grande e in grassetto
            label1.ForeColor = Color.White; //testo bianco
            label1.BackColor = Color.Transparent; //sfondo trasparente
            label1.TextAlign = ContentAlignment.MiddleCenter; //centra il testo

            //configura dimensioni e posizione
            label1.AutoSize = false; //imposta larghezza e altezza manualmente
            label1.Width = 400;
            label1.Height = 60;
            label1.Location = new Point((this.Width - label1.Width) / 2, 20); //centra orizzontalmente
        }
        private void stileLabelNomeGiocatoreSelezionato()
        {
            // imposta il testo di default
            labelNomeGiocatoreSelezionato.Text = "Nome Giocatore";
            labelNomeGiocatoreSelezionato.Font = new Font("Arial", 18, FontStyle.Bold); // font più piccolo rispetto alla selezione personaggio
            labelNomeGiocatoreSelezionato.ForeColor = Color.White; // testo bianco
            labelNomeGiocatoreSelezionato.BackColor = Color.Transparent; // sfondo trasparente
            labelNomeGiocatoreSelezionato.TextAlign = ContentAlignment.MiddleCenter; // centra il testo
        }

        private void stileButtonPlay()
        {
            // imposta il testo
            buttonPlay.Text = "PLAY";
            buttonPlay.Font = new Font("Arial", 18, FontStyle.Bold); // font grande e in grassetto
            buttonPlay.ForeColor = Color.White; // testo bianco
            buttonPlay.BackColor = Color.Red; // colore di sfondo rosso
            buttonPlay.FlatStyle = FlatStyle.Flat; // stile piatto
            buttonPlay.FlatAppearance.BorderSize = 0; // senza bordo

            // bordi leggermente arrotondati
            buttonPlay.Region = new Region(new Rectangle(0, 0, buttonPlay.Width, buttonPlay.Height));
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(5, 5, 15, 15, 180, 90); // angolo superiore sinistro
            path.AddArc(buttonPlay.Width - 20, 5, 15, 15, 270, 90); // angolo superiore destro
            path.AddArc(buttonPlay.Width - 20, buttonPlay.Height - 20, 15, 15, 0, 90); // angolo inferiore destro
            path.AddArc(5, buttonPlay.Height - 20, 15, 15, 90, 90); // angolo inferiore sinistro
            path.CloseAllFigures();
            buttonPlay.Region = new Region(path);

            // evento click per avviare il gioco
            buttonPlay.Click += (s, e) =>
            {
                MessageBox.Show("Personaggio selezionato con successo!"); 
            };
        }

        private void loadSelected(String nomePersonaggio)
        {
            try
            {
                // rimuovi eventuali controlli esistenti nel TableLayoutPanel
                PanelPersonaggio.Controls.Clear();

                CharacterAnimation animationControl = new CharacterAnimation(nomePersonaggio,0,0, PanelPersonaggio.Width, PanelPersonaggio.Height,Enums.Direction.Right);

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
                buttonPlay.ForeColor = Color.White; // testo bianco
                buttonPlay.BackColor = Color.Green; // colore di sfondo verde
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
                buttonPlay.ForeColor = Color.White; // testo bianco
                buttonPlay.BackColor = Color.Green; // colore di sfondo verde
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
            string serverAddress = ServerSettings.Ip; //IP del server
            int serverPort = (int)ServerSettings.Port; //porta del server

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