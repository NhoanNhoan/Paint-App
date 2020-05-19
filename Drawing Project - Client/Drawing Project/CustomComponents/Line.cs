using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Drawing_Project.CustomComponents
{
    internal class Line : Shape
    {
        public Line():base()
        {
            _nControlPoints = 2;
            _type = ShapeType.Line;
            _controlPoints = new List<PointF>();
            _controlPoints.Add(_startPos);
            _controlPoints.Add(_endPos);
            _controlPoints = new List<PointF>();

            for(int i =0; i  < _nControlPoints;++i)
            {
                _controlPoints.Add(new PointF());
            }

            _brush = new SolidBrush(_pen.Color);
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawLine(_pen, _startPos, _endPos);
            _controlPoints[0] = _startPos;
            _controlPoints[1] = _endPos;
        }

        //public override void FillColor(Graphics g)
        //{
        //    _pen.Color = (_brush as SolidBrush).Color;
        //    Draw(g);
        //}

        public override bool CheckPointOnBound(PointF point)
        {
            float b = (_startPos.X * _endPos.Y - _endPos.X * _startPos.Y) / (_startPos.X - _endPos.X);
            float a = (_startPos.Y - b) / _startPos.X;
            float firstDistance = (float)Math.Abs(Math.Pow(_endPos.X - _startPos.X, 2) +
                Math.Pow(_endPos.Y - _startPos.Y, 2));
            float secondDistance = (float)Math.Abs(Math.Pow(_endPos.X - point.X, 2) +
                Math.Pow(_endPos.Y - point.Y, 2));
            float thirdeDistance = (float)Math.Abs(Math.Pow(_startPos.X - point.X, 2) +
                Math.Pow(_startPos.Y - point.Y, 2));

            if (Math.Abs(a * point.X + b - point.Y) <= 10)
            {
                if (firstDistance >= secondDistance + thirdeDistance)
                    return true;
            }

            return false;
        }

        protected override void SetControlPoints()
        {
            _controlPoints[0] = _startPos;
            _controlPoints[1] = _endPos;
        }

        public override void MoveControlPoint(int controlNum, PointF point)
        {
            float x = point.X - _oldPos.X;
            float y = point.Y - _oldPos.Y;

            if (0 == controlNum)
            {
                _startPos = new PointF(_startPos.X + x, _startPos.Y + y);
            }
            else
            {
                _endPos = new PointF(_endPos.X + x, _endPos.Y + y);
            }
        }
    }
}
