using PresentFactory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentFactory.Entities
{
    public class Ball:Toy
    {
        public SolidBrush BallColor { get; private set; }
        Random rnd = new Random();
        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
            Click += Ball_Click;
        }

        private void Ball_Click(object sender, EventArgs e)
        {
            BallColor = new SolidBrush(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
            Invalidate();
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallColor,0,0,Width,Height);
        }

        protected override void Toy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ez egy Labda!");
        }
    }
}
