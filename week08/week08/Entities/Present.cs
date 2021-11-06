using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;

namespace week08.Entities
{
    class Present : Toy
    {
        public SolidBrush PresentColor { get; private set; }
        public SolidBrush PresentColor2 { get; private set; }

        public Present(Color color, Color color2)
        {
            PresentColor = new SolidBrush(color);

            PresentColor2 = new SolidBrush(color2);

        }
        protected override void DrawIamge(Graphics g)
        {
            g.FillRectangle(PresentColor, 0, 0, Width, Height);
            g.FillRectangle(PresentColor2, 20, 0, 10, 50);
            g.FillRectangle(PresentColor2, 0, 20, 50 ,10);
        }
    }
}
