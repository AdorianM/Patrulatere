using Patrulatere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Patrulatere_Visual
{
    class FabricaElementGeometric
    {
        public ElementGeometric ObtineElementGeometric(String tip, List<Object> argumente)
        {
            //protected Color culoare = Color.Black;
            //protected Pen style = new Pen(Color.Black, 2);
            //protected String tip;

            if(tip.ToLower().Equals("punct"))
            {
                
                if(argumente.Count == 2)
                {
                    Punct p = new Punct((int)argumente[0], (int)argumente[1]);
                    p.Culoare = Color.Red;
                    p.Style = new Pen(p.Culoare, 4);
                    return p;
                }
                throw new InvalidOperationException();
            }
            if(tip.ToLower().Equals("latura"))
            {
                if(argumente.Count == 2)
                {
                    Latura l = new Latura((Punct)argumente[0], (Punct)argumente[1]);
                    l.Culoare = Color.Black;
                    l.Style = new Pen(l.Culoare, 2);
                    return l;
                }
                throw new InvalidOperationException();
            }
            if(tip.ToLower().Equals("unghi"))
            {
                if(argumente.Count == 2)
                {
                    Unghi u = new Unghi((Latura)argumente[0], (Latura)argumente[1]);
                    u.Culoare = Color.Green;
                    u.Style = new Pen(u.Culoare, 1);
                    return u;
                }
                throw new InvalidOperationException();
            }
            if(tip.ToLower().Equals("patrulater"))
            {
                if(argumente.Count == 1)
                {
                    List<Punct> puncte = new List<Punct>();
                    foreach(Object o in argumente)
                    {
                        puncte.Add((Punct)o);
                    }

                    Patrulater p = new Patrulater(puncte);
                    p.Culoare = Color.Red;
                    p.Style = new Pen(p.Culoare, 3);
                    return p;
                }
                if(argumente.Count == 4)
                {
                    Patrulater p = new Patrulater((Punct)argumente[0], (Punct)argumente[1], (Punct)argumente[2], (Punct)argumente[3]);
                    p.Culoare = Color.Red;
                    p.Style = new Pen(p.Culoare, 3);
                    return p;
                }
                return new Patrulater(); //Aici apar alte cazuri daca mai e nevoie
            }
            throw new InvalidOperationException();
        }

        public ElementGeometric ObtineElementGeometric(String tip)
        {
            if (tip.ToLower().Equals("punct"))
            {   
                return new Punct();    
            }
            if (tip.ToLower().Equals("latura"))
            {
                return new Latura();
            }
            if (tip.ToLower().Equals("unghi"))
            {
                return new Unghi();
            }
            if (tip.ToLower().Equals("patrulater"))
            {
                return new Patrulater(); //Aici apar alte cazuri daca mai e nevoie
            }
            throw new InvalidOperationException();
        }
    }
}
