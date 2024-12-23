using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace corretto
{
    public partial class CharacterAnimation : UserControl
    {
        private Timer animationTimer;
        private Image spriteSheet;
        private int frameWidth;
        private int frameHeight;
        private int currentFrame;
        private int totalFrames;
        private Rectangle destinationRect;

        //array per memorizzare gli offset X e Y per ogni frame
        private Point[] frameOffsets;

        public CharacterAnimation()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                spriteSheet = Image.FromFile("Idle.png");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File 'Idle.png' non trovato. Assicurati che sia nella directory corretta.");
                Application.Exit();
            }

            totalFrames = 6;
            frameWidth = spriteSheet.Width / totalFrames;
            frameHeight = spriteSheet.Height;

            // Inizializza gli offset per ogni frame
            // Modifica questi valori in base al tuo sprite sheet
            frameOffsets = new Point[totalFrames];
            for (int i = 0; i < totalFrames; i++)
            {
                frameOffsets[i] = new Point(0, 0); // Offset predefinito (0,0)
            }


            this.ClientSize = new Size(frameWidth * 2, frameHeight * 2);

            int x = (this.ClientSize.Width - frameWidth) / 2;
            int y = (this.ClientSize.Height - frameHeight) / 2;
            destinationRect = new Rectangle(x, y, frameWidth, frameHeight);

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

            Rectangle sourceRect = new Rectangle(
                currentFrame * frameWidth,
                0,
                frameWidth,
                frameHeight
            );

            // Applica l'offset del frame corrente
            Rectangle adjustedDestRect = new Rectangle(
                destinationRect.X + frameOffsets[currentFrame].X,
                destinationRect.Y + frameOffsets[currentFrame].Y,
                destinationRect.Width,
                destinationRect.Height
            );

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            e.Graphics.DrawImage(spriteSheet, adjustedDestRect, sourceRect, GraphicsUnit.Pixel);
        }

        private void CharacterAnimation_Load(object sender, EventArgs e)
        {
            //codice da eseguire quando il controllo viene caricato
            //Console.WriteLine("CharacterAnimation caricato correttamente.");
        }
        public void LoadFrames(string spriteFolderPath)
        {
            try
            {
                spriteSheet = Image.FromFile(Path.Combine(spriteFolderPath, "Idle.png"));

                // Supponiamo che il totale dei frame sia determinato dal numero di frame nel file.
                totalFrames = spriteSheet.Width / frameWidth;
                frameWidth = spriteSheet.Width / totalFrames;
                frameHeight = spriteSheet.Height;

                // Reimposta l'animazione al primo frame.
                currentFrame = 0;

                // Reinizializza gli offset (se necessario).
                frameOffsets = new Point[totalFrames];
                for (int i = 0; i < totalFrames; i++)
                {
                    frameOffsets[i] = new Point(0, 0); // Offset predefinito
                }

                this.Invalidate(); // Forza il ridisegno.
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il caricamento del sprite sheet: {ex.Message}");
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