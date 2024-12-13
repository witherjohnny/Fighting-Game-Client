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

            //assegna al bottone play funzione LoadGioco
            //all'evento del pulsante play cliccato, viene associata funzione
            selezionePersonaggio.PlayClicked += (s, personaggio) => LoadGioco();

            //vengono tolti i controlli precedenti
            panelContainer.Controls.Clear();
            
            //e viene aggiunto il controllo utente selezione personaggio
            panelContainer.Controls.Add(selezionePersonaggio);
        }

        private void LoadGioco()
        {
            //creazione controllo utente schermata di gioco
            var gioco = new UserControlGioco();
            //occupare tutto lo spazio disponibile nel pannello
            gioco.Dock = DockStyle.Fill;
            //vengono tolti i controlli precedenti
            panelContainer.Controls.Clear();
            
            //e viene aggiunto il controllo utente del gioco
            panelContainer.Controls.Add(gioco);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Verifica se il controllo attuale è UserControlGioco
            if (panelContainer.Controls.Count > 0 && panelContainer.Controls[0] is UserControlGioco gioco)
            {
                gioco.HandleFormClosing(); // Chiamata al metodo specifico del controllo
            }
        }


        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
