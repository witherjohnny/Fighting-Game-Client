using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace corretto.UserControls
{
    public partial class UserControlGioco : UserControl
    {
        private string personaggio;

        //dichiarazioni variabili per UI e logica di gioco
        private ProgressBar progressBarPlayer1;
        private ProgressBar progressBarPlayer2;

        private Image player1Image;
        private Image player2Image;

        private int player1X, player1Y, player1Width, player1Height;
        private int player2X, player2Y, player2Width, player2Height;

        private string player2Character;

        private UdpClient client;
        private Thread listenerThread;

        private bool isRunning = true;

        //costruttore del controllo
        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            InitializeGameUI();

            this.personaggio = personaggio;
            label1.Text = $"Personaggio selezionato: {personaggio}";
            client = UdpClientSingleton.Instance;

            //connessione al server
            ConnectToServer();
        }

        // Inizializza la UI di gioco
        private void InitializeGameUI()
        {
            // Inizializza le dimensioni e le immagini dei giocatori
            player1Width = player1Height = 50;
            player2Width = player2Height = 50;

            player1X = 100;
            player1Y = 100;

            player2X = 200;
            player2Y = 200;

            // Carica immagini di default (modifica i percorsi secondo necessità)
            try
            {
                player1Image = Image.FromFile("Images/personaggio.png");
                player2Image = Image.FromFile("Images/stickman.png");
            }
            catch
            {
                MessageBox.Show("Errore nel caricamento delle immagini dei personaggi!", "Errore");
            }

            // Inizializza barre di progresso
            progressBarPlayer1 = new ProgressBar { Value = 100, Maximum = 100, Minimum = 0, Location = new Point(10, 10), Size = new Size(200, 20) };
            progressBarPlayer2 = new ProgressBar { Value = 100, Maximum = 100, Minimum = 0, Location = new Point(10, 40), Size = new Size(200, 20) };

            // Aggiungi elementi al controllo
            this.Controls.Add(progressBarPlayer1);
            this.Controls.Add(progressBarPlayer2);
        }

        // Disegna i giocatori sul pannello canvas
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Disegna il giocatore 1
            if (player1Image != null)
                g.DrawImage(player1Image, player1X, player1Y, player1Width, player1Height);

            // Disegna il giocatore 2
            if (player2Image != null)
                g.DrawImage(player2Image, player2X, player2Y, player2Width, player2Height);
        }

        // Timer per aggiornamento grafico
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            CanvasPanel.Invalidate();
        }

        // Gestisce il movimento del giocatore 1 tramite input da tastiera
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: player1Y -= 10; break; // Su
                case Keys.S: player1Y += 10; break; // Giù
                case Keys.A: player1X -= 10; break; // Sinistra
                case Keys.D: player1X += 10; break; // Destra
            }

            // Invia aggiornamenti al server
            SendPlayerData();
        }

        // Connessione al server
        private void ConnectToServer()
        {
            try
            {
                listenerThread = new Thread(ReceiveData);
                listenerThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore di connessione: {ex.Message}", "Errore");
            }
        }

        // Ricezione dati dal server
        private async void ReceiveData()
        {
            try
            {
                String risposta = "";
                do
                {
                    //lettura
                    IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

                    byte[] dataReceived = null;

                    //gestione thread
                    Task t = Task.Factory.StartNew(() =>
                    {
                        dataReceived = client.Receive(ref riceveEP);
                    });
                    await t;

                    risposta = Encoding.ASCII.GetString(dataReceived);

                    //gestisci il messaggio ricevuto
                    HandleServerMessage(risposta);
                } while (risposta != null);
            }
            catch (Exception ex)
            {
                if (isRunning)
                    MessageBox.Show($"Errore durante la ricezione dei dati: {ex.Message}", "Errore");
            }
        }
        /*

        // Gestisce il messaggio ricevuto dal server
        private void HandleServerMessage(string message)
        {
            // Messaggio formato: "id;x;y;character"
            string[] parts = message.Split(';');
            if (parts.Length == 4)
            {
                int id = int.Parse(parts[0]);
                int x = int.Parse(parts[1]);
                int y = int.Parse(parts[2]);
                string character = parts[3];

                // Aggiorna i dati del giocatore 2
                if (id == 2)
                {
                    player2X = x;
                    player2Y = y;
                    if (player2Character != character)
                    {
                        player2Character = character;
                        player2Image = Image.FromFile($"Images/{character}.png");
                    }
                }
            }
        }

        // Invia i dati del giocatore 1 al server
        private void SendPlayerData()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    string message = $"1;{player1X};{player1Y};{personaggio}";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    client.GetStream().Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'invio dei dati: {ex.Message}", "Errore");
            }
        }

        // Metodo chiamato alla chiusura del controllo
        public void HandleFormClosing()
        {
            isRunning = false;

            if (listenerThread != null && listenerThread.IsAlive)
                listenerThread.Join();

            if (client != null)
                client.Close();
        }

        private void UserControlGioco_Load(object sender, EventArgs e)
        {
            // Eseguito al caricamento del controllo
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Eseguito al click sulla label2
        }
    }*/
    }
}