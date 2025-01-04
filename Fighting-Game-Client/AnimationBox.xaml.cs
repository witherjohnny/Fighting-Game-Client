using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Fighting_Game_Client.Enums;
using static Fighting_Game_Client.Data.CharacterData;

namespace Fighting_Game_Client
{
    /// <summary>
    /// Logica di interazione per AnimationBox.xaml
    /// </summary>
    public partial class AnimationBox : UserControl
    {
        //usato sia per il personaggio che per le hitbox che hanno animazioni(ad esempio proiettili)
        private System.Windows.Threading.DispatcherTimer animationTimer;
        private List<BitmapImage> frames = new List<BitmapImage>(); //array per memorizzare i frame dell'animazione
        private int currentFrame;
        private string personaggio;
        private string currentAnimation;
        private bool runOnce; //se false continuera a fare la stessa animazione, ma puo cambiare animazione in qualsiasi momento. se true cicla una volta e ritorna ad idle
        private bool isCancelable; //finche l'animazione corrente non ha finito non puo cambiare animazione
        private Direction direction;
        private bool animating;
        private static readonly object _lock = new object();
        public AnimationBox()
        {
            InitializeComponent();
        }
        public AnimationBox(string personaggio, string startingAnimation, int x, int y, double width, double height, Direction direction)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            this.currentFrame = 0;
            this.Width = width;
            this.Height = height;
            this.currentAnimation = startingAnimation;
            this.runOnce = false;
            this.isCancelable = true;
            this.direction = direction;
            this.animating = false;
            setPosition(x, y);
            //imposta il timer per l'animazione
            animationTimer = new System.Windows.Threading.DispatcherTimer();
            animationTimer.Interval = TimeSpan.FromMilliseconds(100);
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
                        StopAnimation();
                        return;
                    }
                }
                else
                {
                    this.currentFrame = (this.currentFrame + 1) % frames.Count;
                }

                // Get the current frame and flip it if direction is Left
                BitmapImage currentImage = frames[currentFrame];
                AnimationImage.Source = currentImage;

               
                FlipImage(this.direction);
            }
        }
        private void FlipImage(Direction direction)
        {
            // Check if the direction is Left and apply a flip transform
            if (direction == Direction.Left)
            {
                ScaleTransform flipTransform = new ScaleTransform(-1, 1);  // Flip horizontally
                AnimationImage.RenderTransform = flipTransform;
                AnimationImage.RenderTransformOrigin = new Point(0.5, 0.5);  // Flip around the center
            }
            else
            {
                // If direction is not Left (i.e., Right), remove any previous transformations
                AnimationImage.RenderTransform = null;
            }
        }
        public string getCurrentAnimation() { return currentAnimation; }
        public void setAnimation(string animation, Direction direction, bool runOnce, bool isCancelable)
        {
            lock (_lock)
            {
                if (CharactersData.animationExists(this.personaggio, animation))
                {
                    if(animation != this.currentAnimation || direction != this.direction)
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
        }
        public Direction getDirection()
        {
            return this.direction;
        }
        public void setDirection(Direction direction)
        {
            lock (_lock)
            {
                this.direction = direction;
            }
        }
        public void setPosition(int x, int y)
        {
            Canvas.SetLeft(AnimationImage, x);
            Canvas.SetTop(AnimationImage, y);
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
                foreach (string framePath in percorsoFrames)
                {
                    BitmapImage frame = new BitmapImage();
                    frame.BeginInit();
                    frame.UriSource = new Uri(framePath, UriKind.RelativeOrAbsolute);
                    frame.CacheOption = BitmapCacheOption.OnLoad; 
                    frame.EndInit();
                    frames.Add(frame);
                }
                //reimposta l'animazione al primo frame
                currentFrame = 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il caricamento dei frame dalla cartella '{spriteFolderPath}': {ex.Message}");
            }
        }
        public bool isAnimating()
        {
            return this.animating;
        }
        public void StartAnimation()
        {
            if (animationTimer != null && !animationTimer.IsEnabled)
            {
                animating = true;
                animationTimer.Start();
            }
        }

        public void StopAnimation()
        {
            if (animationTimer != null && animationTimer.IsEnabled)
            {
                animating = false;
                animationTimer.Stop();
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            AnimationImage.Width= this.Width;
            AnimationImage.Height= this.Height;
        }
    }
}
