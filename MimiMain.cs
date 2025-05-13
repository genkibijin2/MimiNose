using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MimiNose
{
    public partial class MimiNoseMain : Form
    {
        public MimiNoseMain()
        {
            InitializeComponent();
        }







        //---Code that manages clicking and dragging the window despite not having actual OS borders---\\
        private bool mouseDown;
        private Point lastLocation;
        private void os9_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void os9_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void os9_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //---End of mouse clicking code section---\\
    }
}
