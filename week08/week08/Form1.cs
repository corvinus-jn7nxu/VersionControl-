using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week08.Entities;

namespace week08
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;

        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var b = Factory.CreateNew();
            b.Left = b.Width * -1;
            _balls.Add(b);
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var Position = 0;
            foreach (var item in _balls)
            {
                item.MoveToy();
                if (Position < item.Left)
                {
                    Position = item.Left;
                }
            }
            
            if (Position >= 1000)
            {
                //Ball Saveb = new Ball();
                var Saveb = _balls[0];
                mainPanel.Controls.Remove(Saveb);
                _balls.Remove(Saveb);
                //_balls.RemoveAt(0);
                //mainPanel.Controls.RemoveAt(0);

            }

        }

    }
}
