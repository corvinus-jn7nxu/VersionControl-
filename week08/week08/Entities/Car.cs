﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;

namespace week08.Entities
{
    class Car : Abstractions.Toy
    {
        protected override void DrawIamge(Graphics g)
        {
            Image img = Image.FromFile("Images/car.png");
            g.DrawImage(img, new Rectangle(0, 0, Width, Height));
        }
    }
}
