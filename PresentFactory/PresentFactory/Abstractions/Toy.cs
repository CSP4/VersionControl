using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentFactory.Abstractions
{
    public abstract class Toy : Label
    {
        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = Width;
            Top = 150;
            Paint += Ball_Paint;
            Click += Toy_Click;
        }

        protected abstract void Toy_Click(object sender, EventArgs e);

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected abstract void DrawImage(Graphics g);

        public void MoveToy()
        {
            Left += 1;
        }
    }
}
