using Patrulatere_Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Patrulatere
{
    [Serializable]
    abstract class Figura : ElementGeometric
    {
        protected List<Punct> puncte;
        protected List<Latura> laturi;
        protected List<Unghi> unghiuri;
        public abstract bool Convexitate();
        public abstract double Perimetru();
        public abstract double Aria();
        public abstract bool Inscriptibilitate();
        public abstract bool Circumscriptibilitate();
        public abstract double RazaCerculuiInscris();
        public abstract List<Latura> Diagonale();
        public abstract List<Latura> Bimediane();
        public abstract List<Latura> Bisectoare();
        public abstract List<Unghi> Unghiuri();
    }
}
