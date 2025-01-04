using Fighting_Game_Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client
{
    public class AttackHitBox : HitBox
    {
        //attacks can be any time of damaging move, it can have an image or not have it, it can move.
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        private AnimationBox attackBox;

        public AttackHitBox(string name, int x, int y, int width, int height) : base(name, x, y, width, height)
        {
            SpeedX = 0;
            SpeedY = 0;
            attackBox = null;
        }
        public AttackHitBox(string name, int x, int y, int width, int height, string characterName, string animationName, Direction direction) : base(name, x, y, width, height)
        {
            SpeedX = 0;
            SpeedY = 0;
            attackBox = new AnimationBox(characterName, animationName, x, y, width, height, direction);
        }
        public AnimationBox getAttackBox()
        {
            return attackBox;
        }
        public void setSpeed(int speedX, int speedY)
        {
            this.SpeedX = speedX;
            this.SpeedY = speedY;
        }
        public void Update()
        {
            this.X += SpeedX;
            this.Y -= SpeedY;
            if (attackBox != null)
                attackBox.setPosition(this.X, this.Y);
        }
        public override string toCSV()
        {
            return $"{base.toCSV()}";
        }
        public override string ToString()
        {
            return $"AttackHitBox {Name}(X: {X}, Y: {Y}, Width: {Width}, Height: {Height})";
        }
    }
}
