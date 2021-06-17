using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Patrulatere;

namespace Patrulatere_Visual
{
    class Desen
    {
        private List<ElementGeometric> elemente = new List<ElementGeometric>();

        public List<ElementGeometric> Elemente
        {
            get => elemente;
            set => elemente = value;
        }

        public bool AdaugaElement(ElementGeometric e)
        {
            if(e is Patrulater)
            {
                elemente.Clear();
                elemente.Add(e);
            }
            else
            {
                elemente.Add(e);
            }
            return true;
        }

        public bool StergeElement(ElementGeometric e)
        {
            foreach(ElementGeometric current in elemente)
            {
                if(current.Equals(e))
                {
                    elemente.Remove(current);
                    return true;
                }
            }
            return false;
        }

        public void StergeDesen()
        {
            elemente.RemoveRange(0, elemente.Count);
        }

        public void Desenare(PaintEventArgs e)
        {
            foreach(ElementGeometric el in elemente)
            {
                el.Desenare(e);
            }
        }

        public List<ElementGeometric> getElemente(Type t)
        {
            List<ElementGeometric> elementeCerute = new List<ElementGeometric>();
            foreach (ElementGeometric e in elemente)
            {
                if(t.Equals(e.GetType()))
                {
                    elementeCerute.Add(e);
                }
            }
            if (elementeCerute.Count == 0)
                return null;
            return elementeCerute;
        }

        public Patrulater GetPatrulater()
        {
            List<ElementGeometric> listaElemente = null;
            listaElemente = getElemente(new Patrulater().GetType());
            List<Patrulater> patrulatere = new List<Patrulater>();
            //Test patrate.
            if (listaElemente != null)
            {
                foreach (ElementGeometric el in listaElemente)
                {
                    patrulatere.Add((Patrulater)el); //Obtinere patrulater.
                }
            }
            if (patrulatere.Count != 0)
            {
                return patrulatere[0];
            }
            return null;
        }

        public override string ToString()
        {
            String toReturn = "";
            foreach(ElementGeometric el in elemente)
            {
                toReturn = toReturn + el.ToString();
            }
            return toReturn;
        }
    }
}
