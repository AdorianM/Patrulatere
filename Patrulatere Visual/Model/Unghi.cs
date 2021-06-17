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
    class Unghi : ElementGeometric
    {
        private Latura latura1;
        private Latura latura2;
        private double degrees;
        private const double t = 1 / 4;

        public Unghi(Latura latura1, Latura latura2)
        {
            this.latura1 = latura1;
            this.latura2 = latura2;
            if(latura2.Panta() != Double.PositiveInfinity && latura1.Panta() != Double.PositiveInfinity)
            {
                double numarator = latura2.Panta() - latura1.Panta();
                double numitor = (1 + latura2.Panta() * latura1.Panta());
                if (numitor != 0)
                {
                    double tg_angle = numarator / numitor;
                    if (tg_angle != Math.PI / 2)
                        degrees = getDegrees(Math.Atan(tg_angle));
                    if (degrees < 0)
                    {
                        //Practic 180 - degrees care e negativ deci 180 + degrees
                        degrees = 180 + degrees;
                    }
                }
                else
                {
                    degrees = 90;
                }
            }
            else
            {
                degrees = 90; ////////////////////////////////////ATENTIUNE
            }
            
        }

        public Unghi()
        {

        }

        public double Degrees { get => degrees; }

        public double Radians()
        {
            return (Math.PI / 180) * degrees;
        }

        public double getDegrees(double rad)
        {
            return (180 / Math.PI) * rad;
        }
        public override void Desenare(PaintEventArgs e)
        {
            Culoare = Color.Green;
            Style = new Pen(Culoare, 2);
            //Obtin punctul 1 pentru desenarea unghiului
            Punct p1 = new Punct();
            p1.X = (float)(latura1.Begin.X + t * (latura1.End.X - latura1.Begin.X));
            p1.Y = (float)(latura1.Begin.Y + t * (latura1.End.Y - latura1.Begin.Y));

            //Obtin punctul 2 pentru desenarea unghiului
            Punct p2 = new Punct();
            p2.X = (float)(latura2.Begin.X + t * (latura2.End.X - latura2.Begin.X));
            p2.Y = (float)(latura2.Begin.Y + t * (latura2.End.Y - latura2.Begin.Y));

            //Unghiurile nu trebuiesc desenate
            //e.Graphics.DrawLine(this.Style, p1.X, p1.Y, p2.X, p2.Y);
            //e.Graphics.DrawArc(this.Style, p1.X, p1.Y, Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y), (float)getDegrees(Math.Atan(latura1.Panta())), (float)getDegrees(Math.Atan(latura2.Panta())));
            //e.Graphics.DrawArc(this.Style, new Rectangle((int)p1.X, (int)p1.Y, (int)Math.Abs(p2.X - p1.X), (int)Math.Abs(p2.Y - p1.Y)), (float)getDegrees(Math.Atan(latura1.Panta())), (float)getDegrees(Math.Atan(latura2.Panta())));
        }

        public override string ToString()
        {
            return "Radiani = " + Radians().ToString("#.##") + ", Grade= " + Degrees.ToString("#.##");
        }
    }
}
