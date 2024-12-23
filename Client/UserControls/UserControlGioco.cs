using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
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

        //private Timer gameTimer; // timer per aggiornare il gioco
        private Player playerLocal; // player locale controllato dall'utente
        private Player playerRemote; // player remoto aggiornato dal server

        private const int FrameRate = 60; //frame al secondo (FPS)

        private UdpClient client;

        //costruttore del controllo
        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            InitializeGame();

            this.personaggio = personaggio;
            label1.Text = $"Personaggio selezionato: {personaggio}";
            client = UdpClientSingleton.Instance;
        }

        //inizializza la UI di gioco
        private void InitializeGame()
        {
            this.DoubleBuffered = true; // evita sfarfallio
            this.Width = 800;
            this.Height = 600;

            //inizializza i player
            playerLocal = new Player(personaggio, 100, 300); // personaggio locale
            playerRemote = new Player("warrior2", 500, 300); // personaggio remoto

            //inizializza il timer del gioco
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000 / FrameRate; //intervallo in millisecondi
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            //gestione eventi della tastiera
            this.KeyDown += UserControlGioco_KeyDown;
            this.KeyUp += UserControlGioco_KeyUp;

            //avvia la comunicazione con il server
            StartServerCommunication();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //aggiorna lo stato dei player
            playerLocal.Update();
            playerRemote.Update();

            //aggiorna il disegno
            this.Invalidate(); //chiama OnPaint
        }

        //disegna i giocatori sul pannello canvas
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {
            //ottieni l'oggetto Graphics per disegnare
            Graphics g = e.Graphics;

            //disegna lo sfondo del gioco
            g.Clear(Color.Black); // Sfondo nero

            //disegna il player locale
            playerLocal.Draw(g);

            //disegna il player remoto (dati dal server)
            playerRemote.Draw(g);

            //altri elementi grafici, come effetti, punteggi, ecc.

        }


        //metodo per ridisegnare il controllo
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            //disegna entrambi i player
            playerLocal.Draw(g);
            playerRemote.Draw(g);
        }

        //gestione del movimento con tastiera
        private void UserControlGioco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playerLocal.X -= playerLocal.Speed;
                playerLocal.ChangeAnimation(Action.Run);
            }
            else if (e.KeyCode == Keys.Right)
            {
                playerLocal.X += playerLocal.Speed;
                playerLocal.ChangeAnimation(Action.Run);
            }
            else if (e.KeyCode == Keys.Space)
            {
                playerLocal.ChangeAnimation(Action.Attack);
            }
        }


        private void UserControlGioco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Space)
            {
                playerLocal.ChangeAnimation(Action.Idle);
            }
        }
        private async void StartServerCommunication()
        {
            //simula ricezione dati dal server in un thread separato
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        //ricezione di una risposta dal server
                        IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

                        byte[] dataReceived = client.Receive(ref riceveEP);

                        //decodifica la risposta del server
                        string serverResponse = Encoding.ASCII.GetString(dataReceived);

                        //aggiorna le coordinate del player remoto
                        string[] data = serverResponse.Split(',');
                        if (data.Length == 4)
                        {
                            int id = int.Parse(data[0]);
                            int x = int.Parse(data[1]);
                            int y = int.Parse(data[2]);
                            string character = data[3];

                            //aggiorna i dati del giocatore remoto
                            if (id != playerLocal.Id)
                            {
                                playerRemote.X = x;
                                playerRemote.Y = y;
                                if (playerRemote.nome != character)
                                {
                                    playerRemote.nome = character;
                                }
                            }
                        }

                        Thread.Sleep(100); //aspetta prima del prossimo aggiornamento
                    }
                    catch (Exception ex)
                    {
                        //gestione errori (se necessario, log o debug)
                        Console.WriteLine("Errore nella comunicazione con il server: " + ex.Message);
                    }
                }
            });
        }

        // Invia i dati del giocatore 1 al server
        private void SendPlayerData()
        {
            try
            {
                if (client != null)
                {
                    string message = $"1;{playerLocal.X};{playerLocal.Y};{personaggio}";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    client.Send(data, data.Length, "127.0.0.1", 12345);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'invio dei dati: {ex.Message}", "Errore");
            }
        }

        private void UserControlGioco_Load(object sender, EventArgs e)
        {
            //eseguito al caricamento del controllo
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //eseguito al click sulla label2
        }
    }
}