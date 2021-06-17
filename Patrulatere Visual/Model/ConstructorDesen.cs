using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrulatere_Visual
{
    class ConstructorDesen
    {
        public Desen CreareDesen()
        {
            return new Desen();
        }

        public Desen CreareDesen(List<ElementGeometric> elemente)
        {
            Desen d = new Desen()
            {
                Elemente = elemente
            };
            return d;
        }

        //Metode cand adaug elemenete
    }
}
