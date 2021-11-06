using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week08.Abstractions;
using week08.Entities;

namespace week08
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            toy.Left = toy.Width * -1;
            _toys.Add(toy);
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var Position = 0;
            foreach (var item in _toys)
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
                var oldestToy = _toys[0];
                mainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
                //_balls.RemoveAt(0);
                //mainPanel.Controls.RemoveAt(0);

            }

        }

    }
}
