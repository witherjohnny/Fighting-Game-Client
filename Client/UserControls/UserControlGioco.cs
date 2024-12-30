using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FightingGameClient.Enums;

namespace FightingGameClient.UserControls
{
    public partial class UserControlGioco : UserControl
    {
        private string personaggio;

        //private Timer gameTimer; // timer per aggiornare il gioco
        private Player playerLocal; // player locale controllato dall'utente
        private Player playerRemote; // player remoto aggiornato dal server

        private const int FrameRate = 60; //frame al secondo (FPS)

        private UdpClient client = UdpClientSingleton.Instance;



        //costruttore del controllo
        public UserControlGioco(string personaggio)
        {
            InitializeComponent();
            this.personaggio = personaggio;

            InitializeGame();
            //avvia la comunicazione con il server
            StartServerCommunication();

            //grafica
            stileLabels();

            //es rosso poca vita, verde tanta vita
            //stileProgressBars();

            disegnaBackground();
            disegnaPavimento();

            label1.Text = $"Personaggio selezionato: {personaggio}";
        }

        private void stileLabels()
        {
            label1.Font = new Font("Arial", 14, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.BackColor = Color.Transparent; //trasparente per mostrare sfondo del gioco
            label1.TextAlign = ContentAlignment.MiddleCenter;

            label2.Font = new Font("Arial", 14, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.BackColor = Color.Transparent; //trasparente per mostrare sfondo del gioco
            label2.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void disegnaBackground()
        {
            //percorso dell'immagine di sfondo
            //sfondoGioco.png per sfondo normale
            //sfondoBlur.png per sfondo blurrato

            string immagineSfondo = Path.Combine("Sfondi", "sfondoGioco.png");

            //verifica se il file esiste
            if (!File.Exists(immagineSfondo))
            {
                MessageBox.Show($"Immagine dello sfondo non trovata: {immagineSfondo}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //carica l'immagine e imposta lo sfondo
                this.BackgroundImage = Image.FromFile(immagineSfondo);
                this.BackgroundImageLayout = ImageLayout.Stretch; //adatta l'immagine al pannello
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento dello sfondo: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void disegnaPavimento()
        {
            //percorso dell'immagine di sfondo del pavimento
            string immaginePavimento = Path.Combine("Pavimenti", "pavimento1.png");

            //verifica se il file esiste
            if (!File.Exists(immaginePavimento))
            {
                MessageBox.Show($"Immagine del pavimento non trovata: {immaginePavimento}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //dimensioni del pavimento
            int numeroBlocchiTotali = 10; //numero totale di blocchi
            int numeroBlocchiPavimento = numeroBlocchiTotali - 4; //blocchi effettivi (lasciando 2 vuoti per lato)
            int larghezzaBlocco = this.Width * 2 / numeroBlocchiTotali;
            int altezzaBlocco = 100; //altezza fissa del pavimento
            int yPavimento = this.Height - altezzaBlocco; //posizione verticale del pavimento
            int xInizioPavimento = larghezzaBlocco * 2; //inizio del pavimento (salta 2 blocchi a sinistra)

            for (int i = 0; i < numeroBlocchiPavimento; i++)
            {
                Panel blocco = new Panel
                {
                    Width = larghezzaBlocco,
                    Height = altezzaBlocco,
                    BackColor = Color.Brown, //colore di fallback
                    Location = new Point(xInizioPavimento + (i * larghezzaBlocco), yPavimento),
                    BackgroundImage = Image.FromFile(immaginePavimento),
                    BackgroundImageLayout = ImageLayout.Stretch //adatta l'immagine al blocco
                };

                //aggiungi ogni blocco al controllo
                this.Controls.Add(blocco);
            }
        }



        //inizializza la UI di gioco
        private void InitializeGame()
        {
            this.DoubleBuffered = true; // evita sfarfallio
            this.Width = 800;
            this.Height = 600;

            //inizializza i player
            playerLocal = new Player(personaggio, 100, 300); // personaggio locale
            //coordinate e nome date dal server
            playerRemote = new Player("Warrior_2", 500, 300); // personaggio remoto

            this.Controls.Add(playerLocal.getCharacterBox());
            this.Controls.Add(playerRemote.getCharacterBox());

            //inizializza il timer del gioco
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000 / FrameRate; //intervallo in millisecondi
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            //gestione eventi della tastiera
            this.KeyDown += UserControlGioco_KeyDown;
            this.KeyUp += UserControlGioco_KeyUp;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //aggiorna lo stato dei player
            //playerLocal.Update();
            //playerRemote.Update();

            //aggiorna il disegno
            //this.Invalidate(); //chiama OnPaint
        }


        //gestione del movimento con tastiera
        private void UserControlGioco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                playerLocal.X -= playerLocal.Speed;
                playerLocal.setAnimation("Run");
            }
            else if (e.KeyCode == Keys.Right)
            {
                playerLocal.X += playerLocal.Speed;
                playerLocal.setAnimation("Run");
            }
            else if (e.KeyCode == Keys.Space)
            {
                playerLocal.setAnimation("Run");
            }
        }


        private void UserControlGioco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Space)
            {
                playerLocal.setAnimation("Idle");
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

        //invia i dati del giocatore locale al server
        /*
        private void SendPlayerData()
        {
            try
            {
                if (client != null)
                {
                    string message = $"1;{playerLocal.X};{playerLocal.Y};{personaggio}";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    client.Send(data, data.Length, ServerSettings.Ip, (int)ServerSettings.Port);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'invio dei dati: {ex.Message}", "Errore");
            }
        }*/
    }
}