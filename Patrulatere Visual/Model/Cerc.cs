using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patrulatere;

namespace Patrulatere_Visual.Model
{
    class Cerc : ElementGeometric
    {
        Punct centru;
        double raza;

        public Punct Centru { get => centru; set => centru = value; }
        public double Raza { get => raza; set => raza = value; }

        public Cerc(Punct c, double r)
        {
            Centru = c;
            Raza = r;
        }

        public override void Desenare(PaintEventArgs e)
        {
            List<Punct> puncte = new List<Punct>();
            //Initial salvez mijloacele dreptunghiului de desenare (punctele tangente cercului)
            puncte.Add(new Punct((float)(centru.X - raza), (float)(centru.Y - raza)));
            puncte.Add(new Punct((float)(centru.X + raza), (float)(centru.Y + raza)));
            //Obtin colturile

            e.Graphics.DrawEllipse(Style, puncte[0].X, puncte[0].Y, puncte[1].X - puncte[0].X, puncte[1].Y - puncte[0].Y);
        }
    }
}
