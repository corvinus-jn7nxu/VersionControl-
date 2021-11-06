using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;

namespace week08.Entities
{
    public class PresentFactory : IToyFactory
    {
        public Color PresentColor { get;  set; }
        public Color PresentColor2 { get;  set; }
        public Toy CreateNew()
        {
            return new Present(PresentColor, PresentColor2);
        }
    }
}
