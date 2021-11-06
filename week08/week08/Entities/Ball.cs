using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week08.Abstractions;

namespace week08.Entities
{
    public class Ball : Toy
    {
        //public event System.Windows.Forms.MouseEventHandler MouseClick;
        private Random rnd = new Random();

        private void Ball_Click(object sender, EventArgs e)
        {
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            BallColor = new SolidBrush(randomColor);
            Invalidate();
        }

        public SolidBrush BallColor { get; private set; }
        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
            Click += Ball_Click;
        }

        protected override void DrawIamge(Graphics g)
        {
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }



    }
}
