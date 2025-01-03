using Fighting_Game_Client.Enums;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Speed { get; set; }
        private string Id { get; }
        private bool isJumping;
        private bool isFalling;
        private int jumpHeight = 50;
        private int fallSpeed = 5; //velocità della discesa
        public string nome { get; set; }

        private AnimationBox characterBox;
        private Direction currentDirection; //direzione corrente

        public Player()
        {
            characterBox = null;
            currentDirection = Direction.Right;
            Speed = 5; //velocità predefinita
        }

        public Player(string id, string characterName, int startX, int startY, Direction direction)
        {
            Id = id;
            X = startX;
            Y = startY;
            Speed = 5;

            characterBox = new AnimationBox(characterName, "Idle", X, Y, 128, 128, direction);
        }

        public void setAnimation(string animationName, Direction direction, bool runOnce, bool isCancelable)
        {
            characterBox.setAnimation(animationName, direction, runOnce, isCancelable);
        }

        public void setPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
            characterBox.setPosition(X, Y);
        }

        public string getId() { return Id; }
        public AnimationBox getCharacterBox() { return this.characterBox; }

        //movimento a sinistra
        public void MoveLeft(List<Rect> obstacles)
        {
            int newX = X - Speed;
            if (!CheckCollisionWithTerrain(new Rect(newX, Y, characterBox.Width, characterBox.Height), obstacles))
            {
                X = newX;
                setPosition(X, Y);  //sincronizza posizione con la animazione
                currentDirection = Direction.Left;
                setAnimation("Run", Direction.Left, false, true);
            }
        }

        //movimento a destra
        public void MoveRight(List<Rect> obstacles)
        {
            int newX = X + Speed;
            if (!CheckCollisionWithTerrain(new Rect(newX, Y, characterBox.Width, characterBox.Height), obstacles))
            {
                X = newX; 
                setPosition(X, Y);  //sincronizza posizione con la animazione
                currentDirection = Direction.Right;
                setAnimation("Run", Direction.Right, false, true);
            }
        }

        //gestisce il salto
        public void Jump(List<Rect> obstacles)
        {
            if (isJumping) return; //evita doppi salti
            isJumping = true;
            Y -= jumpHeight; //salto
            setPosition(X, Y);  //sincronizza posizione con la animazione
            setAnimation("Jump", currentDirection, false, true);
        }

        //gestisce la discesa
        public void Fall()
        {
            if (!isJumping && Y < 550)  //se non sta saltando e non ha raggiunto il pavimento
            {
                Y += fallSpeed;  //discesa con gravità
                setPosition(X, Y);  //sincronizza posizione con la animazione
                setAnimation("Fall", currentDirection, false, true);
            }
        }

        //controlla la collisione con il terreno
        private bool CheckCollisionWithTerrain(Rect playerBox, List<Rect> obstacles)
        {
            foreach (var terrain in obstacles)
            {
                if (playerBox.IntersectsWith(terrain))
                {
                    return true;
                }
            }
            return false;
        }
       
        //gestisce la fine del salto quando il personaggio tocca il pavimento
        public void Land()
        {
            if (Y >= 550)  //quando il giocatore tocca il pavimento (Y = 550)
            {
                Y = 550;
                isJumping = false;
                setAnimation("Idle", currentDirection, true, true); //torna all'animazione idle
            }
        }

        //funzione per aggiornare il movimento verticale (salto e gravità)
        public void Update(List<Rect> obstacles)
        {
            if (isJumping)
            {
                Fall();  //gestisce la discesa dopo il salto
            }
            else
            {
                Land();  //se il personaggio ha raggiunto il pavimento, ferma la discesa
            }
        }
    }
}