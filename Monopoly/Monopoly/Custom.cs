﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monopoly
{
    class DrawPanel : Panel
    {
        public DrawPanel()
        {
            this.DoubleBuffered = true;
        }
    }

    class DrawListBox : ListBox
    {
        public DrawListBox()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }
    }
}
