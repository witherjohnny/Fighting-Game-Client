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
using System.Threading;

namespace corretto.UserControls
{
    public partial class UserControlGioco : UserControl
    {
        /*private ProgressBar progressBarPlayer1;
        private ProgressBar progressBarPlayer2;



        private Image player1Image;
        private Image player2Image;

        private int player1X, player1Y, player1Width, player1Height;
        private int player2X, player2Y, player2Width, player2Height;

        private string player2Character; 
        private TcpClient client;
        private Thread listenerThread;*/

        public UserControlGioco()
        {
            InitializeComponent();
            InitializeGameUI();

            //connessione al server
            ConnectToServer();
        }

        private void InitializeGameUI()
        {
            
        }

        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }


        private void GameTimer_Tick(object sender, EventArgs e)
        {
           
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        //connessione al server
        private void ConnectToServer()
        {
            
        }

        //ricezione dati dal server
        private void ReceiveData()
        {
           
        }

        // Gestione del messaggio ricevuto
        private void HandleServerMessage(string message)
        {
            
        }

     

        // Metodo chiamato dal Form principale alla chiusura
        public void HandleFormClosing()
        {
            
        }
           
        private void UserControlGioco_Load(object sender, EventArgs e)
        {

        }
    }
}
