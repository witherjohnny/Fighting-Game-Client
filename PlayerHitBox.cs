using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightingGameClient
{
    internal class PlayerHitBox : HitBox
    {
        public PlayerHitBox(int x, int y, int width, int height) : base(x, y, width, height)
        {
        }
        public override string toCSV()
        {
            return $"PlayerHitBox;{X};{Y};{Width};{Height}";
        }
        public override string ToString()
        {
            return $"PlayerHitBox(X: {X}, Y: {Y}, Width: {Width}, Height: {Height})";
        }
    }
}
