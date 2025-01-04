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

        public bool isJumping = false;

        public bool isDashing = false;
        private int dashCount = 0;
        public string nome { get; set; }
        private int gravita = 1;

        private AnimationBox characterBox;

        public Player()
        {
            characterBox = null;
            Speed = 5; //velocità predefinita
        }

        public Player(string id, string characterName, int startX, int startY, Direction direction)
        {
            Id = id;
            nome=characterName;
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
            return characterBox.getDirection();
        }

        //fa movimento se non c'è collisione
        public void Update(List<Rect> obstacles)
        {
            int newX = this.X + SpeedX;
            int newY = this.Y - SpeedY;
            controllaGravita(obstacles);
            //se sta facendo il dash, si controlla a che punto è
            if(characterBox.getCurrentAnimation() == "Roll")
            {
                if(this.dashCount <= 6)
                {
                    //è nei frame del dash, viene incrementato a ogni frame
                    this.dashCount++;
                }
                else
                {
                    this.isDashing = false;
                    this.SpeedX = 0;
                    this.dashCount = 0;
                }
            }
            if (CheckCollisionWithTerrain(new Rect(newX, newY-1, this.characterBox.Width, this.characterBox.Height), obstacles) == null)
            {
                this.X += SpeedX;
                this.Y -= SpeedY;
                if (this.characterBox != null)
                    this.characterBox.setPosition(X, Y);
            }

        }

        private Rect? CheckCollisionWithTerrain(Rect playerBox, List<Rect> obstacles)
        {
            foreach (var terrain in obstacles)
            {
                if (playerBox.IntersectsWith(terrain))
                {
                    return terrain;
                }
            }
            return null;
        }

        private void controllaGravita( List<Rect> obstacles)
        {
            Rect? collidedWith = CheckCollisionWithTerrain(new Rect(X, Y - SpeedY, characterBox.Width, characterBox.Height), obstacles);
            if (collidedWith!= null)//atterrato
            {
                this.Y =(int)(collidedWith.Value.Y - characterBox.Height);
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