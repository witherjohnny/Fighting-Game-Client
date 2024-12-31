﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FightingGameClient
{
    internal class PlayerHitBox : HitBox
    {
        public PlayerHitBox(string name,int x, int y, int width, int height) : base(name,x, y, width, height)
        {
        }
        public override string toCSV()
        {
            return $"PlayerHitBox;{base.toCSV()}";
        }
        public override string ToString()
        {
            return $"PlayerHitBox {Name}(X: {X}, Y: {Y}, Width: {Width}, Height: {Height})";
        }
    }
}
