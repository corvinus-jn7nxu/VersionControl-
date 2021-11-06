using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week08.Abstractions
{
    public abstract class Toy : Label
    {

        public Toy()
        {
            AutoSize = false;
            Height = 50;
            Width = 50;
            Paint += Toy_paint;
        }

        private void Toy_paint(object sender, PaintEventArgs e)
        {
            DrawIamge(e.Graphics);
        }
        protected abstract void DrawIamge(Graphics g);

        
        public virtual void MoveToy()
        {
            Left++;
        }
    }
}
