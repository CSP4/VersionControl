using PresentFactory.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentFactory.Entities
{
    public class PresentFactoryCS : IToyFactory
    {
        public Color PresentMainColor { get; set; }
        public Color PresentLineColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(PresentMainColor, PresentLineColor);
        }
    }
}
