using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patrulatere;

namespace Patrulatere_Visual
{
    public partial class ViewForm : Form
    {
        public event EventHandler<MouseEventArgs> DrawPanel_MouseClick_event;
        public event EventHandler<PaintEventArgs> DrawPanel_Paint_event;
        public event EventHandler btnResetSize_Click_event;
        public event EventHandler btnReset_Click_event;
        public event EventHandler ButtonDraw_Click_event;
        public event EventHandler<MouseEventArgs> btnSave_Click_event;
        public event EventHandler<MouseEventArgs> btnIncarcare_Click_event;


        public ViewForm()
        {
             
            InitializeComponent();
        }

        private void DrawPanel_MouseClick(object sender, MouseEventArgs e)
        {
           if(DrawPanel_MouseClick_event != null)
                DrawPanel_MouseClick_event(this, e);
        }

        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < DrawPanel.Width; i += Controller.Controller.squareSize)
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray), i, 0, i, DrawPanel.Height);
            }
            for(int j = 0; j < DrawPanel.Height; j += Controller.Controller.squareSize)
            {
                e.Graphics.DrawLine(new Pen(Color.DarkGray), 0, j, DrawPanel.Width, j);
            }
            if (DrawPanel_Paint_event != null)
                DrawPanel_Paint_event(this, e);
        }

        private void ButtonDraw_Click(object sender, EventArgs e)
        {
            if(ButtonDraw_Click_event != null)
                ButtonDraw_Click_event(this, e);
            
        }

        private void btnResetSize_Click(object sender, EventArgs e)
        {
            if (btnResetSize_Click_event != null)
                btnResetSize_Click_event(this, e);
            Refresh();
        }

        private void butReset_Click(object sender, EventArgs e)
        {
            if (btnReset_Click_event != null)
                btnReset_Click_event(this, e);
            Refresh();
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            List<CheckBox> cbList = gboxData.Controls.OfType<CheckBox>().ToList();
            if (cbSelectAll.Checked)
            {
                foreach (CheckBox c in cbList)
                {
                    c.Checked = true;
                }
            }
            else
            {
                foreach (CheckBox c in cbList)
                {
                    c.Checked = false;
                }
            }
                
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave_Click_event != null)
                btnSave_Click_event(this, null);
        }

        private void btnIncarcare_Click(object sender, EventArgs e)
        {
            if (btnIncarcare_Click_event != null)
                btnIncarcare_Click_event(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String text = "Negru - Cerc circumscris\nNegru - Laturi\n" +
                "Mov - Punct Newton\nVerde - Diagonale\nLime - Punct Mathot(intersectia mediatoarelor)\n" +
                "Visiniu - Bimediane\nRosu - Mediatoare\nRosu - colturi\nAqua - Dreapta Newton\n" +
                "Portocaliu - Bisectoare\nUnele elemente nu sunt vizibile. Se poate decomenta afisarea lor in Patrulater.cs\n" +
                "Cercul circumscris este vizibil doar cand acesta exista!!!";
            MessageBox.Show(text, "Legenda", MessageBoxButtons.OK);
        }
    }
}
