using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FightingGameClient
{
    public partial class CharacterAnimation : UserControl
    {
        private Timer animationTimer;
        private Image[] frames; //array per memorizzare i frame dell'animazione
        private int currentFrame;
        private int totalFrames;
        private Rectangle destinationRect;

        public CharacterAnimation()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //carica i frame dalla cartella Idle come esempio predefinito
                LoadFrames("Sprites/personaggio/Idle");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento dei frame: {ex.Message}");
                Application.Exit();
            }

            //imposta la dimensione della finestra basata sui frame
            this.ClientSize = new Size(frames[0].Width * 2, frames[0].Height * 2);

            int x = (this.ClientSize.Width - frames[0].Width) / 2;
            int y = (this.ClientSize.Height - frames[0].Height) / 2;
            destinationRect = new Rectangle(x, y, frames[0].Width, frames[0].Height);

            //imposta il timer per l'animazione
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += OnAnimationTick;
            animationTimer.Start();
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //disegna il frame corrente al centro della finestra
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            e.Graphics.DrawImage(frames[currentFrame], destinationRect);
        }

        public void LoadFrames(string spriteFolderPath)
        {
            try
            {
                //recupera tutti i file PNG nella cartella specificata
                string[] files = Directory.GetFiles(spriteFolderPath, "*.png").OrderBy(f => f).ToArray();

                if (files.Length == 0)
                {
                    throw new Exception("Nessun frame trovato nella cartella specificata.");
                }

                //carica i file come immagini
                frames = files.Select(f => Image.FromFile(f)).ToArray();
                totalFrames = frames.Length;

                //reimposta l'animazione al primo frame
                currentFrame = 0;

                this.Invalidate(); //forza il ridisegno
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il caricamento dei frame dalla cartella '{spriteFolderPath}': {ex.Message}");
            }
        }

        public void StartAnimation()
        {
            if (animationTimer != null && !animationTimer.Enabled)
            {
                animationTimer.Start();
            }
        }

        public void StopAnimation()
        {
            if (animationTimer != null && animationTimer.Enabled)
            {
                animationTimer.Stop();
            }
        }
    }
}
