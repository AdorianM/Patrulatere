using Patrulatere_Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Patrulatere
{
    [Serializable]
    class Punct : ElementGeometric
    {
        private float x;
        private float y;

        public Punct()
        {
            X = 0; //Scriind X mare, se va asocia valoarea la x prin intermediul setter-ului
            Y = 0;
        }
        public Punct(float x,float y)
        {
            this.X = x;
            this.Y = y;
        }

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public override void Desenare(PaintEventArgs e)
        {
            Culoare = Color.Red;
            Style = new Pen(Culoare, 5);
            Rectangle dotBuilder = new Rectangle((int)(X - 1), (int)(Y - 1), 1, 1);
            e.Graphics.DrawEllipse(this.Style, dotBuilder);
        }

        public void Desenare(PaintEventArgs e, Color c)
        {
            Culoare = c;
            Style = new Pen(Culoare, 5);
            Rectangle dotBuilder = new Rectangle((int)(X - 1), (int)(Y - 1), 1, 1);
            e.Graphics.DrawEllipse(this.Style, dotBuilder);
        }

        public override string ToString()
        {
            return "(" + X + " " + Y + ") ";
        }
    }
}
