using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using FightingGameClient.Data;
using FightingGameClient.Enums;

namespace FightingGameClient
{
    public partial class AnimationBox : UserControl
    {
        //usato sia per il personaggio che per le hitbox che hanno animazioni(ad esempio proiettili)
        private Timer animationTimer;
        private List<Image> frames = new List<Image>(); //array per memorizzare i frame dell'animazione
        private int currentFrame;
        private string personaggio;
        private string currentAnimation;
        private bool runOnce; //se false continuera a fare la stessa animazione, ma puo cambiare animazione in qualsiasi momento. se true cicla una volta e ritorna ad idle
        private bool isCancelable; //finche l'animazione corrente non ha finito non puo cambiare animazione
        private Direction direction;
        private static readonly object _lock = new object();
        public AnimationBox(string personaggio,string startingAnimation, int x, int y, int width, int height, Direction direction)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            this.currentFrame= 0;
            this.Size= new Size(width, height);
            this.currentAnimation = startingAnimation;
            this.runOnce = false;
            this.isCancelable = true;
            this.direction = direction;
            setPosition(x, y);
            //imposta il timer per l'animazione
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += OnAnimationTick;

            if (CharactersData.animationExists(personaggio, startingAnimation))
            {
                LoadFrames($"Images/Sprites/{this.personaggio}/{this.currentAnimation}");
                
                StartAnimation();
            }
        }
        private void OnAnimationTick(object sender, EventArgs e)
        {
            lock (_lock)
            {
                if (frames.Count == 0)
                {
                    animationTimer.Stop();
                    return;
                }

                if (runOnce)
                {
                    this.currentFrame++;
                    if (this.currentFrame == frames.Count)
                    {


                        this.runOnce = false;
                        this.isCancelable = true;
                        setAnimation("Idle",this.direction, false, true);
                        return;
                    }
                }
                else
                {
                    this.currentFrame = (this.currentFrame + 1) % frames.Count;
                }

                // Get the current frame and flip it if direction is Left
                Image currentImage = frames[currentFrame];
                if (this.direction == Direction.Left)
                {
                    currentImage = FlipImage(currentImage);
                }
                this.BackgroundImage = currentImage;
            }
        }
        private Image FlipImage(Image image)
        {
            if (image == null) return null;

            // Create a new bitmap to hold the flipped image
            Bitmap flippedImage = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(flippedImage))
            {
                // Flip the image horizontally
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(image.Width, 0, -image.Width, image.Height),
                    GraphicsUnit.Pixel);
            }

            return flippedImage;
        }
        public void setAnimation(String animation, Direction direction, bool runOnce, bool isCancelable)
        {
            lock (_lock)
            {
                if (CharactersData.animationExists(this.personaggio, animation))
                {
                    if (this.isCancelable)
                    {
                        this.direction = direction;
                        this.currentAnimation = animation;
                        this.runOnce = runOnce;
                        this.isCancelable = isCancelable;
                        LoadFrames($"Images/Sprites/{this.personaggio}/{this.currentAnimation}");
                        StartAnimation();
                        
                    }
                }
            }
        }
        public void setDirection(Direction direction)
        {
            lock(_lock)
            {
                this.direction = direction;
            }
        }
        public void setPosition(int x, int y)
        {
            this.Location = new Point(x, y);
        }
        
        public void LoadFrames(string spriteFolderPath)
        {
            try
            {
                frames.Clear();

                //recupera tutti i file PNG nella cartella specificata
                string[] percorsoFrames = Directory.GetFiles(spriteFolderPath, "*.png").ToArray();
                Array.Sort(percorsoFrames);
                if (percorsoFrames.Length == 0)
                {
                    throw new Exception("Nessun frame trovato nella cartella specificata.");
                }

                //carica i file come immagini
                foreach (string percorsoImmagine in percorsoFrames)
                {
                    frames.Add(Image.FromFile(percorsoImmagine)) ;
                }
                //reimposta l'animazione al primo frame
                currentFrame = 0;
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
