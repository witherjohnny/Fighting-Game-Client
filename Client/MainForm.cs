using FightingGameClient.UserControls;
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
using FightingGameClient.Data;

namespace FightingGameClient
{
    //la finestra principale che gestisce i UserControl
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // carica il primo controllo utente
            this.AllowTransparency = true;
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
            //creazione del controllo utente per la selezione del personaggio
            var selezionePersonaggio = new UserControlSelezionePersonaggio();
            selezionePersonaggio.Dock = DockStyle.Fill;

            //assegna la funzione LoadGioco all'evento PlayClicked
            selezionePersonaggio.PlayClicked += (personaggio) => LoadGioco(personaggio);

            //sostituisce i controlli precedenti nel pannello
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(selezionePersonaggio);
        }

        private void LoadGioco(string personaggio)
        {
            //creazione del controllo utente per il gioco, passando il personaggio selezionato
            var gioco = new UserControlGioco(personaggio);
            gioco.Dock = DockStyle.Fill;

            //sostituisce i controlli precedenti nel pannello
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(gioco);
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string serverAddress = ServerSettings.Ip; 
            int serverPort = (int)ServerSettings.Port;
            UdpClient udpClient = UdpClientSingleton.Instance;

            String messaggio = "leave";
            byte[] data = Encoding.ASCII.GetBytes(messaggio);

            udpClient.Send(data, data.Length, serverAddress, serverPort);
        }
    }
}
