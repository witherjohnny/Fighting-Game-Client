using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using FightingGameClient.Data;

namespace FightingGameClient
{
    public partial class CharacterAnimation : UserControl
    {
        private Timer animationTimer;
        private List<Image> frames = new List<Image>(); //array per memorizzare i frame dell'animazione
        private int currentFrame;
        private string personaggio;
        private string currentAnimation;
        private bool runOnce; //se false continuera a fare la stessa animazione, ma puo cambiare animazione in qualsiasi momento. se true cicla una volta e ritorna ad idle
        private bool isCancelable; //finche l'animazione corrente non ha finito non puo cambiare animazione
        private static readonly object _lock = new object();
        public CharacterAnimation(string personaggio, int width, int height)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            this.currentFrame= 0;
            this.Size= new Size(width, height);
            this.currentAnimation = "Idle";
            this.runOnce = false;
            this.isCancelable = true;

            
            LoadFrames($"Sprites/{this.personaggio}/{this.currentAnimation}");
            //imposta il timer per l'animazione
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += OnAnimationTick;
            StartAnimation();

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
                        setAnimation("Idle", false, true);
                        return;
                    }
                }
                else
                {
                    this.currentFrame = (this.currentFrame + 1) % frames.Count;
                }
                this.BackgroundImage = frames[currentFrame];
            }
        }
        public void setAnimation(String animation)
        {
            lock (_lock)
            {
                if (CharactersData.animationExists(this.personaggio, animation))
                {
                    if (isCancelable)
                    {
                        this.currentAnimation = animation;
                        LoadFrames($"Sprites/{this.personaggio}/{this.currentAnimation}");
                        StartAnimation();
                    }
                }
            }
        }
        public void setAnimation(String animation,bool runOnce, bool isCancelable)
        {
            lock (_lock)
            {
                if (CharactersData.animationExists(this.personaggio, animation))
                {
                    if (this.isCancelable)
                    {
                        this.currentAnimation = animation;
                        LoadFrames($"Sprites/{this.personaggio}/{this.currentAnimation}");
                        StartAnimation();
                        this.runOnce = runOnce;
                        this.isCancelable = isCancelable;
                    }
                }
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
