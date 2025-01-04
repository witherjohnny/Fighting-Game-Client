using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fighting_Game_Client
{
    public class HitBox
    {
        public string Id { get; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public HitBox(string name, int x, int y, int width, int height)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public virtual bool CollidesWith(HitBox other)
        {
            return X < other.X + other.Width &&
                   X + Width > other.X &&
                   Y < other.Y + other.Height &&
                   Y + Height > other.Y;
        }
        public virtual string toCSV()
        {
            return $"{Id};{Name};{X};{Y};{Width};{Height}";
        }
        public override string ToString()
        {
            return $"{Name}(X: {X}, Y: {Y}, Width: {Width}, Height: {Height})";
        }

    }
}
