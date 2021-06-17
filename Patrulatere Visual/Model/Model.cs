using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patrulatere;
using Patrulatere_Visual;

namespace Patrulatere_Visual.Model
{
    class Model
    {
        ConstructorDesen constructorDesen = new ConstructorDesen();
        Desen desen;
        FabricaElementGeometric fabrica;

        public Model()
        {
            fabrica = new FabricaElementGeometric();
            desen = constructorDesen.CreareDesen(new List<ElementGeometric>());
        }

        public Desen Desen { get => desen; set => desen = value; }
        public FabricaElementGeometric Fabrica { get => fabrica; set => fabrica = value; }

        public void addPoint(int x, int y)
        {
            Punct p2 = new Punct(x, y);
            if (desen.Elemente.Count >= 1)
            {
                Punct p1 = null;
                for (int i = desen.Elemente.Count - 1; i >= 0; i--)
                {
                    Type t = p2.GetType();
                    if (t.Equals(desen.Elemente[i].GetType()))
                    {
                        p1 = new Punct(((Punct)desen.Elemente[i]).X, ((Punct)desen.Elemente[i]).Y);
                        break;
                    }
                }
                if (p1 != null)
                {
                    desen.Elemente.Add(fabrica.ObtineElementGeometric("latura", new List<Object>() { p1, p2 }));
                }
            }
            desen.Elemente.Add(fabrica.ObtineElementGeometric("Punct", new List<Object>() { x, y }));
        }

        public void addPatrulater()
        {
            List<Object> puncte = new List<Object>();
            Type t = new Punct().GetType();
            for (int i = 0; i < desen.Elemente.Count; i++)
            {
                if (t.Equals(desen.Elemente[i].GetType()))
                {
                    puncte.Add((desen.Elemente[i]));
                }
            }
            desen.Elemente.Add(fabrica.ObtineElementGeometric("Patrulater", puncte));
        }

        public void addPatrulater(Patrulater p)
        {
            desen.Elemente.Clear();
            desen.Elemente.Add(p);
        }
    }
}
