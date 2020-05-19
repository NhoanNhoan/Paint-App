using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Drawing_Project.CustomComponents
{
    class Hexagon : Polygon
    {
        public Hexagon(int nAngle = 6) : base(nAngle)
        {
        }

        public override void Draw(Graphics graphics)
        {
            SetAngles();
            graphics.DrawPolygon(_pen, _angles);
        }

        public override void FillColor(Graphics g)
        {
            g.FillPolygon(_brush, _angles);
        }

        public override bool CheckPointOnBound(PointF point)
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));
            PointF centerPoint = new PointF(topLeftCorner.X + (botRightCorner.X - topLeftCorner.X) / 2,
                topLeftCorner.Y + (botRightCorner.Y - topLeftCorner.Y) / 2);

            if (CalculateArea(centerPoint) == CalculateArea(point))
                return true;

            return false;
        }

        protected override void SetAngles()
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));
            float r = (botRightCorner.X - topLeftCorner.X) / 2;
            float h = (botRightCorner.Y - topLeftCorner.Y) / 2;

            _angles[0] = new PointF(topLeftCorner.X + r / 2, topLeftCorner.Y);
            _angles[1] = new PointF(_angles[0].X + r, _angles[0].Y);
            _angles[2] = new PointF(botRightCorner.X, topLeftCorner.Y + h);
            _angles[3] = new PointF(botRightCorner.X - r / 2, botRightCorner.Y);
            _angles[4] = new PointF(topLeftCorner.X + r / 2, botRightCorner.Y);
            _angles[5] = new PointF(topLeftCorner.X, topLeftCorner.Y + h);
        }

        private double CalculateTriangleArea(PointF firstAngle, PointF secondAngle, PointF thirdAngle)
        {
            return Math.Abs((firstAngle.X * (secondAngle.Y - thirdAngle.Y) +
                secondAngle.X * (thirdAngle.Y - firstAngle.Y) + thirdAngle.X * (firstAngle.Y - secondAngle.Y)) / 2.0);
        }

        protected override double CalculateArea(PointF point)
        {
            return CalculateTriangleArea(point, _angles[0], _angles[1]) +
                CalculateTriangleArea(point, _angles[1], _angles[2]) +
                CalculateTriangleArea(point, _angles[2], _angles[3]) +
                CalculateTriangleArea(point, _angles[3], _angles[4]) +
                CalculateTriangleArea(point, _angles[4], _angles[5]) +
                CalculateTriangleArea(point, _angles[5], _angles[0]);
        }
    }
}
