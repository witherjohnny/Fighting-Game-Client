using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormSceltaPersonaggio : Form
    {
        public FormSceltaPersonaggio()
        {
            InitializeComponent();
        }

        private void FormSceltaPersonaggio_Load(object sender, EventArgs e)
        {
            //aspetta che il server li dica che entrambi i client sono collegati
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //nella label il nome personaggio
            labelPersonaggioSelezionato.Text = "Personaggio1";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //nella label il nome personaggio
            labelPersonaggioSelezionato.Text = "Personaggio2";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //nella label il nome personaggio
            labelPersonaggioSelezionato.Text = "Personaggio3";
        }

        private void buttonAvanti_Click(object sender, EventArgs e)
        {
            //controlla che ci sia un personaggio selezionato e lo manda al server salvandolo nel client
            if(labelPersonaggioSelezionato.Text != "")
            {
                //manda al server il nome del personaggio

                //va al GameForm
                GameForm gf = new GameForm();

                //mostra il nuovo form
                gf.Show();

                //chiude il form attuale
                this.Hide();
            }
        }
    }
}
