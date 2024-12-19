using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace corretto
{
    internal class Player
    {
        private String id;
        private String personaggio;
        private float x;
        private float y;
        public Player(String id, float x, float y)
        {
            this.personaggio = null;
            this.id = id;
            this.x = x;
            this.y = y;
        }
        public String getId()
        {
            return this.id;
        }
        public String toCSV()
        {
            return getId() + ";" + x + ";" + y + ";" + personaggio;
        }
        public String toString()
        {
            return personaggio;
        }
        

        public void setX(float x)
        {
            this.x = x;
        }

        public void setY(float y)
        {
            this.y = y;
        }

        public float getX()
        {
            return x;
        }

        public float getY()
        {
            return y;
        }
        public String getPersonaggio()
        {
            return personaggio;
        }
        public void setPersonaggio(String personaggio)
        {
            this.personaggio = personaggio;
        }
    }
}
