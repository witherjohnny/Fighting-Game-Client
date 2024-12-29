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
        
        public CharacterAnimation(string personaggio, int width, int height)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            this.currentFrame= 0;
            this.Size= new Size(width, height);
            this.currentAnimation = "Idle";

        }
        private void CharacterAnimation_Load(object sender, EventArgs e)
        {
            try
            {
                //carica i frame dalla cartella Idle come esempio predefinito
                LoadFrames($"Sprites/{this.personaggio}/{this.currentAnimation}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento dei frame: {ex.Message}");
            }
            
            //imposta il timer per l'animazione
            animationTimer = new Timer();
            animationTimer.Interval = 100;
            animationTimer.Tick += OnAnimationTick;
            animationTimer.Start();
        }
        public void setAnimation(String animation)
        {
            if(CharactersData.animationExists(this.personaggio,animation))
            {
                this.currentAnimation = animation;
                LoadFrames($"Sprites/{this.personaggio}/{this.currentAnimation}");
            }
        }
        public void setPosition(int x, int y)
        {
            this.Location = new Point(x, y);
        }
        private void OnAnimationTick(object sender, EventArgs e)
        {
            this.currentFrame = (this.currentFrame + 1) % frames.Count;
            this.BackgroundImage = frames[currentFrame];
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
