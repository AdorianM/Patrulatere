using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patrulatere_Visual.Model;
using Patrulatere_Visual;
using System.Windows.Forms;
using Patrulatere;
using System.Reflection;
using System.Collections;
using System.IO;

namespace Patrulatere_Visual.Controller
{
    class Controller
    {
        Model.Model model = null;
        ViewForm view = null;
        int clickCount = 0;
        public static int squareSize = 20;
        public Controller(Model.Model model, ViewForm view)
        {
            this.model = model;
            this.view = view;

            this.view.DrawPanel_MouseClick_event += functie_desenare;
            this.view.DrawPanel_Paint_event += refacere_desen;
            this.view.btnResetSize_Click_event += View_btnResetSize_Click_event;
            this.view.btnReset_Click_event += View_btnReset_Click_event;
            this.view.ButtonDraw_Click_event += View_ButtonDraw_Click_event;
            this.view.btnSave_Click_event += View_btnSave_Click_event;
            this.view.btnIncarcare_Click_event += View_btnIncarcare_Click_event;

            Application.Run(this.view);
        }

        private void View_btnIncarcare_Click_event(object sender, MouseEventArgs e)
        {
            String fileName = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {            
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                }
            }
            Patrulater p = LoadPatrulater(fileName);
            if(p != null)
            {
                model.addPatrulater(p);
                clickCount = 0;
                view.Refresh();
            }
        }

        private void View_btnSave_Click_event(object sender, MouseEventArgs e)
        {
            String fileName = "";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                }
            }

            SavePatrulater(fileName);
        }

        private void View_ButtonDraw_Click_event(object sender, EventArgs e)
        {
            Patrulater patrulater = model.Desen.GetPatrulater();
            if(patrulater != null)
            {
                view.richTextBox1.Text = "";
                List<CheckBox> requestedData = view.gboxData.Controls.OfType<CheckBox>().ToList();
                foreach (CheckBox c in requestedData)
                {
                    if (c.Checked)
                    {
                        string functionName = c.Name;
                        functionName = functionName.Substring(2, Math.Max(0, c.Name.Length - 2));
                        //Reflection
                        Type patrulaterType = (patrulater.GetType());
                        MethodInfo metoda = patrulaterType.GetMethod(functionName);
                        if (metoda != null)
                        {
                            view.richTextBox1.Text += functionName + ": ";
                            Object result = metoda.Invoke(patrulater, null);
                            String toWrite = "";
                            if (result is ICollection)
                            {
                                foreach (Object o in (ICollection)result)
                                {
                                    toWrite += o.ToString();
                                }
                            }
                            else
                            {
                                toWrite = result.ToString();
                            }
                            view.richTextBox1.Text += toWrite + '\n';
                        }
                    }
                }
            }
        }

        private void View_btnReset_Click_event(object sender, EventArgs e)
        {
            model.Desen.StergeDesen();
            clickCount = 0;
        }

        private void View_btnResetSize_Click_event(object sender, EventArgs e)
        {
            if (!view.tBSquareSize.Text.Equals(""))
                squareSize = Int32.Parse(view.tBSquareSize.Text);
        }

        public int SnapValueToGrid(int x)
        {
            return (int)Math.Round(x / (float)squareSize) * squareSize;
        }

        public void refacere_desen(object sender, PaintEventArgs e)
        {
            model.Desen.Desenare(e);
        }

        public Patrulater LoadPatrulater(String fileName)
        {
            if(fileName != "")
            {
                string allData = "";
                using(BinaryReader reader = new BinaryReader(File.Open(fileName,FileMode.Open)))
                {
                    allData = reader.ReadString();
                }
                if(allData != "")
                {
                    Patrulater patrulater = (Patrulater)Model.Convertor.StringToObject(allData);
                    MessageBox.Show("Patrulater incarcat cu succes.", "Mesaj informativ");
                    return patrulater;
                }
                return null;
            }
            return null;
        }

        public void SavePatrulater (String fileName)
        {
            if(fileName != "")
            {
                Patrulater patrulater = model.Desen.GetPatrulater();
                if (patrulater != null)
                {
                    String patrulaterBinar = Model.Convertor.ObjectToString(patrulater);
                    using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                    {
                        writer.Write(patrulaterBinar);
                    }
                    MessageBox.Show("Patrulaterul s-a salvat cu succes.", "Mesaj informativ");
                    return;
                }
                MessageBox.Show("Patrulaterul nu s-a salvat cu succes.", "Mesaj informativ");
            }
        }
        public void functie_desenare(object sender, MouseEventArgs e)
        {
            if(model.Desen.GetPatrulater() != null)
            {
                View_btnReset_Click_event(sender, e);
            }

            model.addPoint(SnapValueToGrid(e.Location.X), SnapValueToGrid(e.Location.Y));
            clickCount++;
            if (clickCount == 4)
            {
                model.addPatrulater();
            }
            view.Refresh();
        }


    }
}
