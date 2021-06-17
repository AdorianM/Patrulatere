using Patrulatere;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patrulatere;
using Patrulatere_Visual;

namespace Patrulatere_Visual
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model.Model model = new Model.Model();
            ViewForm view = new ViewForm();
            Controller.Controller controller = new Patrulatere_Visual.Controller.Controller(model, view);
            
        }
    }
}
