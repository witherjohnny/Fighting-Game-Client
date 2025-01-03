using Fighting_Game_Client.Enums;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Documents;

namespace Fighting_Game_Client
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int Speed { get; set; }
        private string Id { get; }

        private int jumpHeight = 50;
        public bool isJumping = false;
        public string nome { get; set; }
        private int gravita = 1;

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


        public Direction getDirection()
        {
            return currentDirection;
        }

        //fa movimento se non c'è collisione
        public void Update(List<Rect> obstacles)
        {
            int newX = this.X + SpeedX;
            int newY = this.Y - SpeedY;
            controllaGravita(new Rect(newX, newY, this.characterBox.Width, this.characterBox.Height), obstacles);
            if (!CheckCollisionWithTerrain(new Rect(newX, newY, this.characterBox.Width, this.characterBox.Height), obstacles))
            {
                this.X += SpeedX;
                this.Y -= SpeedY;
                if (this.characterBox != null)
                    this.characterBox.setPosition(newX, newY);
            }
        }

        //controllo collisione
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

        private void controllaGravita(Rect playerBox, List<Rect> obstacles)
        {
            if(CheckCollisionWithTerrain(playerBox, obstacles))//atterrato
            {
                this.isJumping = false;
                this.SpeedY = 0;
            }
            else //vuoto sotto
            {
                //characterBox.setAnimation("Fall", this.Direction, false, true);
                this.SpeedY -= this.gravita;
            }
        }
    }
}