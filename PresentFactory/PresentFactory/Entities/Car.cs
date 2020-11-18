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
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFile = Image.FromFile("Images/car.png");
            g.DrawImage(imageFile, new Rectangle(0,0,Width,Height));
        }

        protected override void Toy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ez egy Autó!");
        }
    }
}
