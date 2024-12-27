using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGameClient
{
    public class Animation
    {
        private List<Image> frames;
        private int currentFrame;
        private int frameDelay;
        private int frameCounter;

        public Animation(string spriteSheetPath, int frameDelay)
        {
            this.frames = aggiungiFrame(spriteSheetPath);
            this.frameDelay = frameDelay;
            this.currentFrame = 0;
            this.frameCounter = 0;
        }

        private List<Image> aggiungiFrame(string folderPath)
        {
            List<Image> frames = new List<Image>();

            //ottieni tutti i file immagine nella directory specificata
            string[] frameFiles = System.IO.Directory.GetFiles(folderPath, "*.png");

            //ordina i file per nome (es: Attack_1_1.png, Attack_1_2.png, ecc.)
            Array.Sort(frameFiles);

            //carica ogni immagine nella lista
            foreach (string frameFile in frameFiles)
            {
                Image frame = Image.FromFile(frameFile);
                frames.Add(frame);
            }

            return frames;
        }


        public Image GetCurrentFrame()
        {
            return frames[currentFrame];
        }

        public void Update()
        {
            frameCounter++;
            if (frameCounter >= frameDelay)
            {
                frameCounter = 0;
                currentFrame = (currentFrame + 1) % frames.Count;
            }
        }
    }

}

