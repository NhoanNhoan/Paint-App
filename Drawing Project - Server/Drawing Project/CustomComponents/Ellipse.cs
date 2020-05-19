using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing_Project.CustomComponents
{
    class Ellipse : Drawing_Project.CustomComponents.RectangleShape
    {
        public Ellipse() : base()
        {
        }

        public override void Draw(Graphics graphics)
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));

            graphics.DrawEllipse(_pen, new RectangleF(topLeftCorner.X, topLeftCorner.Y,
                botRightCorner.X - topLeftCorner.X,
                botRightCorner.Y - topLeftCorner.Y));
        }

        public override void FillColor(Graphics g)
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));
            g.FillEllipse(_brush, new RectangleF(topLeftCorner.X, topLeftCorner.Y,
                botRightCorner.X - topLeftCorner.X,
                botRightCorner.Y - topLeftCorner.Y));
        }
    }
}
