using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Patrulatere_Visual
{
    [Serializable]
    abstract class ElementGeometric
    {
        protected Color culoare = Color.Black;
        [NonSerialized] protected Pen style = new Pen(Color.Black, 2);

        public Color Culoare { get => culoare; set => culoare = value; }
        public Pen Style { get => style; set => style = value; }

        public abstract void Desenare(PaintEventArgs e);
    }
}
