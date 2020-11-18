using PresentFactory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentFactory.Entities
{
    public class Present : Toy
    {
        public SolidBrush PresentMainColor { get; private set; }
        public SolidBrush PresentLineColor { get; private set; }
        public Present(Color color1, Color color2)
        {
            PresentMainColor = new SolidBrush(color1);
            PresentLineColor = new SolidBrush(color2);
        }
        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(PresentMainColor, 0, 0, Width, Height);
            g.FillRectangle(PresentLineColor, 0, Height / 5 * 2, Width , Height / 5 );
            g.FillRectangle(PresentLineColor, Width / 5 * 2, 0, Width /5, Height);
        }
    }
}
