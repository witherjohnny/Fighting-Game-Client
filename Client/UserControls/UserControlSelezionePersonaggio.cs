using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corretto.UserControls
{
    //schermata per selezionare il personaggio
    public partial class UserControlSelezionePersonaggio : UserControl
    {
        public event EventHandler<string> PersonaggioSelezionato;

        public UserControlSelezionePersonaggio()
        {
            InitializeComponent();
        }
        private void buttonSpada_Click(object sender, EventArgs e)
        {
            if (PersonaggioSelezionato != null)
            {
                PersonaggioSelezionato(this, "Spada");
            }
        }

        private void buttonArco_Click(object sender, EventArgs e)
        {
            if (PersonaggioSelezionato != null)
            {
                PersonaggioSelezionato(this, "Arco");
            }
        }

        private void buttonMagia_Click(object sender, EventArgs e)
        {
            if (PersonaggioSelezionato != null)
            {
                PersonaggioSelezionato(this, "Magia");
            }
        }
    }
}
