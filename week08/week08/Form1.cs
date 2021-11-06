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

        private Toy _nextToy;

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set {
                _factory = value; 
                DisplayNext(); }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory();
        }
        void DisplayNext()
        {
            if (_nextToy != null)
            {
                Controls.Remove(_nextToy);
            }
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var cd = new ColorDialog();
            cd.Color = button.BackColor;
            //cd.ShowDialog();
            if (cd.ShowDialog() != DialogResult.OK)
            {
                return;  
            }
            button.BackColor = cd.Color;
        }
    }
}
