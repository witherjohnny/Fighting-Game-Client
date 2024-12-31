using FightingGameClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGameClient
{
    internal class AttackHitBox:HitBox
    {
        public int SpeedX { get; set; } 
        public int SpeedY{ get; set; } 
        public Direction Direction { get; set; }
        public AttackHitBox(int x, int y, int width, int height,Direction direction) : base(x, y, width, height)
        {
            SpeedX = 0;
            SpeedY = 0;
            Direction = direction;
        }
        public void Update()
        {
            this.X += SpeedX;
            this.Y -= SpeedY;
        }
        public override string toCSV()
        {
            return $"AttackHitBox;{X};{Y};{Width};{Height}";
        }
        public override string ToString()
        {
            return $"AttackHitBox(X: {X}, Y: {Y}, Width: {Width}, Height: {Height})";
        }
    }
}
