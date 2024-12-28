using System;
using System.Collections.Generic;
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
        private List<Image> frames = new List<Image>(); //array per memorizzare i frame dell'animazione
        private int currentFrame;
        private int totalFrames;
        private String personaggio;

        public CharacterAnimation(String personaggio)
        {
            InitializeComponent();
            this.personaggio = personaggio;
            this.Dock = DockStyle.Fill;
        }

        private void CharacterAnimation_Load(object sender, EventArgs e)
        {
            try
            {
                //carica i frame dalla cartella Idle come esempio predefinito
                LoadFrames("Sprites/"+this.personaggio+"/Idle");
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

        private void OnAnimationTick(object sender, EventArgs e)
        {
            currentFrame = (currentFrame + 1) % totalFrames;
            this.BackgroundImage = frames[currentFrame];
        }
        public void LoadFrames(string spriteFolderPath)
        {
            try
            {
                //recupera tutti i file PNG nella cartella specificata
                string[] files = Directory.GetFiles(spriteFolderPath, "*.png").ToArray();

                if (files.Length == 0)
                {
                    throw new Exception("Nessun frame trovato nella cartella specificata.");
                }

                //carica i file come immagini
                foreach (string immagine in files)
                {
                    frames.Add(Image.FromFile(immagine)) ;
                    totalFrames = frames.Count;
                }

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
