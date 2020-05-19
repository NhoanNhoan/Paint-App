using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Drawing_Project.Tools
{
    internal class DrawingPen
    {
        internal Pen _pen;
        private Color Color
        {
            get
            {
                return Color;
            }
            set
            {
                Color = value;
            }
        }
        private Int32 Width
        {
            get
            {
                return Width;
            }
            set
            {
               Width = value;
            }
        }

        public DrawingPen()
        {
            Color = Color.Black;
            Width = 2;
            _pen = new Pen(Color, Width);
        }
    }
}
