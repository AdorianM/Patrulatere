using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Patrulatere_Visual;
using Patrulatere_Visual.Model;
using System.Reflection;
using System.Runtime.Serialization;

namespace Patrulatere
{
    [Serializable]
    class Patrulater : Figura
    {
        List<Cerc> cercuri;
        public Patrulater(List<Punct> puncte)
        {
            this.puncte = puncte;
            laturi = Laturi();
            unghiuri = Unghiuri();
        }

        public Patrulater(Punct p1, Punct p2, Punct p3, Punct p4)
        {
            puncte = new List<Punct>();
            puncte.Add(p1);
            puncte.Add(p2);
            puncte.Add(p3);
            puncte.Add(p4);
            laturi = Laturi();
            unghiuri = Unghiuri();
        }

        public Patrulater()
        {
        }

        public List<Latura> Laturi()
        {
            List<Latura> laturi = new List<Latura>(4);
            laturi.Add(new Latura(puncte[0], puncte[1]));
            laturi.Add(new Latura(puncte[1], puncte[2]));
            laturi.Add(new Latura(puncte[2], puncte[3]));
            laturi.Add(new Latura(puncte[3], puncte[0]));
            return laturi;
        }

        public override List<Unghi> Unghiuri()
        {
            List<Unghi> unghiuri = new List<Unghi>(4);
            //ACEASTA ORDINE A LATURILOR CONTEAZA
            unghiuri.Add(new Unghi(laturi[3], laturi[0]));
            unghiuri.Add(new Unghi(laturi[0], laturi[1]));
            unghiuri.Add(new Unghi(laturi[1], laturi[2]));
            unghiuri.Add(new Unghi(laturi[2], laturi[3]));
            return unghiuri;
        }

        public List<Punct> Colturi()
        {
            return puncte;
        }


        public override List<Latura> Bimediane()
        {
            List<Latura> bimediane = new List<Latura>();
            
            bimediane.Add(new Latura(laturi[0].Mijloc(), laturi[2].Mijloc()));
            bimediane.Add(new Latura(laturi[1].Mijloc(), laturi[3].Mijloc()));
            return bimediane;
        }

        private Latura getBisectoare(int x)
        {
            
            Latura bisectoareCurenta = new Latura();
            if (x % 4 == 0)
            {
                //piciorul bisectoarei pe diagonala opusa
                Punct p1 = puncte[0];
                
                bisectoareCurenta.Begin = p1;
                Punct picior_p1 = new Punct();
                double a = 1;
                double b = laturi[3].Lungime()/ laturi[0].Lungime();

                if(b > a)
                {
                    double aux = a;
                    a = b;
                    b = aux;
                }

                Latura diagonala_opusa = new Latura(puncte[1], puncte[3]);
                picior_p1.X = (float)(diagonala_opusa.Begin.X + (a / (a + b) * (diagonala_opusa.End.X - diagonala_opusa.Begin.X)));
                picior_p1.Y = (float)(diagonala_opusa.Begin.Y + (a / (a + b) * (diagonala_opusa.End.Y - diagonala_opusa.Begin.Y)));
                bisectoareCurenta.End = picior_p1;
            }
            else
            {
                Punct p1 = puncte[x % 4];
                bisectoareCurenta.Begin = p1;
                Punct picior_p1 = new Punct();
                double a = 1;
                double b = laturi[x % 4].Lungime() / laturi[(x - 1) % 4].Lungime();

                if (b > a)
                {
                    double aux = a;
                    a = b;
                    b = aux;
                }

                Latura diagonala_opusa = new Latura(puncte[(x + 1) % 4], puncte[(x + 3) % 4]);
                picior_p1.X = (float)(diagonala_opusa.Begin.X + (a / (a + b) * (diagonala_opusa.End.X - diagonala_opusa.Begin.X)));
                picior_p1.Y = (float)(diagonala_opusa.Begin.Y + (a / (a + b) * (diagonala_opusa.End.Y - diagonala_opusa.Begin.Y)));
                bisectoareCurenta.End = picior_p1;
            }
            return bisectoareCurenta;
        }

        public override List<Latura> Bisectoare()
        {
            

            List<Latura> bisectoare = new List<Latura>();
            for(int i = 0; i < 4; i++)
            {
                bisectoare.Add(getBisectoare(i));
            }

            return bisectoare;
        }

        public override bool Circumscriptibilitate()
        {
            double sum1 = Math.Abs(laturi[0].Lungime()) + Math.Abs(laturi[2].Lungime());
            double sum2 = Math.Abs(laturi[1].Lungime()) + Math.Abs(laturi[3].Lungime());
            if (sum1 == sum2)
                return true;
            return false;
        }

        public override bool Convexitate()
        {
            foreach (Unghi u in unghiuri)
            {
                if(u.Degrees > 180)
                {
                    return false;
                }
            }
            return true;
        }

        public override List<Latura> Diagonale()
        {

            List<Latura> diagonale = new List<Latura>();
            diagonale.Add(new Latura(puncte[0], puncte[2]));
            diagonale.Add(new Latura(puncte[1], puncte[3]));
            return diagonale;
        }

        public override bool Inscriptibilitate()
        {
            //Verificare suplementaritate unghiuri opuse
            if(unghiuri[0].Degrees + unghiuri[2].Degrees != 180) //Nu verificam pt unghiurile 1 si 3 pentru ca daca primele n-au 180, nici celelalte nu vor avea
                return false;
            return true;
        }

        public override double Perimetru()
        {
            double perimetru = 0;
            foreach(Latura l in laturi)
            {
                perimetru += l.Lungime();
            }
            return perimetru;
        }

        public override double RazaCerculuiInscris()
        {
            //R = ARIA / SEMIPERIMETRU
            if(Circumscriptibilitate() == true)
            {
                double semiPerimetru = Perimetru() / 2;
                return Aria() / semiPerimetru;
            }
            return 0;
        }

        public Punct CentrulCerculuiCircumscris()
        {
            Punct centru = new Punct();
            Latura mediatoare1 = laturi[0].Mediatoare();
            Latura mediatoare2 = laturi[1].Mediatoare();
            Latura mediatoare3 = laturi[2].Mediatoare();
            Latura mediatoare4 = laturi[3].Mediatoare();
            centru = mediatoare1.Intersectie(mediatoare2);
            if(centru == null)
            {
                centru = mediatoare2.Intersectie(mediatoare3);
                if(centru == null)
                {
                    centru = mediatoare3.Intersectie(mediatoare4);
                    if(centru == null)
                    {
                        centru = mediatoare4.Intersectie(mediatoare1);
                    }
                }
                
            }
            return centru;
        }

        public double RazaCerculuiCircumscris()
        {
            if(Inscriptibilitate() == false)
                return 0;
            //Am nevoie de 3 laturi. Daca un patrulater e inscriptibil si triunghiul e inscriptibil (oricare din ele) si au acelasi cerc circumsrcis.
            Punct p = CentrulCerculuiCircumscris();
            Latura laturaRazei = new Latura(p, puncte[0]);
            return laturaRazei.Lungime();
        }

        public Cerc CerculCircumscris()
        {
            if (Inscriptibilitate())
            {
                if(CentrulCerculuiCircumscris() != null)
                {
                    Cerc c = new Cerc(CentrulCerculuiCircumscris(), RazaCerculuiCircumscris());
                    return c;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Desenare(PaintEventArgs e)
        {
            Random rand = new Random();
            Color culoareDiagonale = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            Color culoareBimediane = Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
            foreach (Latura l in Laturi())
            {
                l.Desenare(e);
                
            }

            foreach (Latura l in Diagonale())
            {
                //120, 210, 111
                l.Desenare(e, Color.FromArgb(120, 210, 111));
            }
            
            /*foreach(Latura l in Laturi())
            {
                l.Mediatoare().Desenare(e, Color.Red);
            }*/

            foreach (Latura l in Bimediane()) //SE POT ACTIVA PENTRU DESENARE
            {
                l.Desenare(e, Color.Brown);
            }
            

            foreach (Punct p in puncte)
            {
                p.Desenare(e);
            }

            //DreaptaNewton().Desenare(e, Color.Aqua); //SE POATE ACTIVA PENTRU DESENARE

            Punct newt = PunctNewton();
            if(newt != null)
            {
                newt.Desenare(e, Color.BlueViolet);
            }

            if(Inscriptibilitate())
            {
                if(CentrulCerculuiCircumscris() != null)
                    CentrulCerculuiCircumscris().Desenare(e, Color.Lime);
            }
            Cerc c = CerculCircumscris();
            if (c != null)
            {
                c.Desenare(e);
            }

            

            foreach (Latura b in Bisectoare())
            {
                b.Desenare(e, Color.DarkOrange);
            }
        }

        public double SumaUnghiurilor()
        {
            double sum = 0;
            foreach(Unghi u in unghiuri)
            {
                sum += u.Degrees;
            }
            return sum;
        }

        public Punct PunctNewton()
        {
            if (Inscriptibilitate())
            {
                List<Latura> diagonale = Diagonale();
                Punct newton = diagonale[0].Intersectie(diagonale[1]);
                return newton;
            }
            return null;
        }

        public Latura DreaptaNewton() 
        {
            List<Latura> diagonale = Diagonale();
            return new Latura(diagonale[0].Mijloc(), diagonale[1].Mijloc());
        }

        public override double Aria()
        {
            double aria = 0;
            Unghi L0L3 = new Unghi(laturi[3], laturi[0]);//Unghiul dintre latura 0 si 3
            Unghi l1L2 = new Unghi(laturi[1], laturi[2]);
            aria = (laturi[0].Lungime() * laturi[3].Lungime() * Math.Sin(L0L3.Radians()) + laturi[1].Lungime() * laturi[2].Lungime() * Math.Sin(l1L2.Radians())) / 2;
            return aria;
        }
    }
}
