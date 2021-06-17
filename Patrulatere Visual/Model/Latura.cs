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
    class Latura : ElementGeometric
    {
        private Punct begin;
        private Punct end;

        public Latura(Punct begin, Punct end)
        {
            this.Begin = begin;
            this.End = end;
        }

        public Latura()
        {
        }

        public Punct Begin
        {
            get => begin;
            set => begin = value;
        }
        public Punct End
        {
            get => end;
            set => end = value;
        }

        public double Lungime()
        {
            return Math.Sqrt(Math.Pow(End.X - Begin.X, 2) + Math.Pow(End.Y - Begin.Y, 2));
        }

        public double Panta()
        {
            if(End.X - Begin.X != 0)
                return (End.Y - Begin.Y) / (End.X - Begin.X);
            return Double.PositiveInfinity;
        }

        public Punct Mijloc()
        {
            return new Punct((End.X + Begin.X) / 2, (End.Y + Begin.Y) / 2);
        }

        public List<float> ParametriEcuatie()
        {
            //aX + bY + c = 0
            //KX + Y + M = 0 (K = a/b, M = c/b)
            List<float> parametri = new List<float>(3);
            float a = End.Y - Begin.Y;
            float b = Begin.X - End.X;
            float c = -(a * Begin.X + b * Begin.Y);
            parametri.Add(a); 
            parametri.Add(b); 
            parametri.Add(c);
            return parametri;
        }

        public Punct Intersectie(Latura l)
        {
            Punct p = new Punct();
            float a1, b1, c1;
            float a2, b2, c2;

            List<float> parametri1 = ParametriEcuatie();
            List<float> parametri2 = l.ParametriEcuatie();
            a1 = parametri1[0];
            b1 = parametri1[1];
            c1 = parametri1[2];
            a2 = parametri2[0];
            b2 = parametri2[1];
            c2 = parametri2[2];

            if(a1 * b2 - a2 * b1 != 0 && b1 != 0)
            {
                p.X = (c2 * b1 - c1 * b2) / (a1 * b2 - a2 * b1);
                p.Y = (-c1 - a1 * p.X) / b1;
                return p;
            }
            return null;
        }

        public Latura Mediatoare()
        {
            return Perpendiculara(Mijloc());
        }

        public Latura Perpendiculara(Punct p)
        {
            Latura perpendiculara = new Latura();
            float xNew;
            float yNew;
            //obtine panta
            //y = kx + m
            if (!Double.IsInfinity(Panta()))
            {
                double pantaPerpendicularei = -1 / Panta();
                if (!Double.IsInfinity(pantaPerpendicularei))
                {
                    float mPerpendiculara = (p.Y - (float)pantaPerpendicularei * p.X);
                    Punct capatPerpendiculara;

                    xNew = p.X - 20 > 0 ? p.X - 20 : p.X + 20;
                    yNew = ((float)pantaPerpendicularei * xNew) + mPerpendiculara;
                    capatPerpendiculara = new Punct(xNew, yNew);

                    perpendiculara.Begin = p;
                    perpendiculara.End = capatPerpendiculara;

                    //obtine o latura ce trece prin p.
                    //y in functie de panta. (cunoscand x)

                    return perpendiculara;
                }
                else
                {
                    xNew = p.X;
                    yNew = p.Y - 20 > 0 ? p.Y - 20 : p.Y + 20;
                    perpendiculara.Begin = new Punct(xNew, yNew);
                    perpendiculara.End = p;
                    return perpendiculara;
                }
            }
            else
            {
                xNew = p.X - 20 > 0 ? p.X - 20 : p.X + 20;
                yNew = p.Y;
                perpendiculara.Begin = new Punct(xNew, yNew);
                perpendiculara.End = p;
                return perpendiculara;
            }
            
           
        }
        
        public override string ToString()
        {
            return " (" + begin + "-" + end + "). Panta : " + Panta().ToString("#.##") + " ";
        }

        public override void Desenare(PaintEventArgs e)
        {
            Culoare = Color.Black;
            Style = new Pen(Culoare, 3);
            e.Graphics.DrawLine(this.style, Begin.X, Begin.Y, End.X, End.Y);
        }

        public void Desenare(PaintEventArgs e, Color c)
        {
            Culoare = c;
            Style = new Pen(Culoare, 3);
            e.Graphics.DrawLine(this.style, Begin.X, Begin.Y, End.X, End.Y);
        }
    }
}
