using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Drawing_Project.CustomComponents
{
    class Triangle : Polygon
    {
        public Triangle(int nAngles = 3) : base(nAngles)
        {
            _nControlPoints = 8;
            _type = ShapeType.Rectangle;
            _controlPoints = new List<PointF>();

            for (int i = 0; i < _nControlPoints; ++i)
            {
                _controlPoints.Add(new PointF());
            }
        }

        public override void Draw(Graphics graphics)
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));

            _angles[0] = new PointF(_startPos.X, _endPos.Y);
            _angles[1] = new PointF(_startPos.X + Math.Abs((_endPos.X - _startPos.X) / 2), _startPos.Y);
            _angles[2] = _endPos;
            graphics.DrawPolygon(_pen, _angles);
        }

        public override void FillColor(Graphics g)
        {
            g.FillPolygon(_brush, _angles);
        }

        public override bool CheckPointOnBound(PointF point)
        {
            double A = Area(_angles[0].X, _angles[0].Y, _angles[1].X, _angles[1].Y, _angles[2].X, _angles[2].Y);

            /* Calculate area of triangle PBC */
            double A1 = Area(point.X, point.Y, _angles[1].X, _angles[1].Y, _angles[2].X, _angles[2].Y);

            /* Calculate area of triangle PAC */
            double A2 = Area(_angles[0].X, _angles[0].Y, point.X, point.Y, _angles[2].X, _angles[2].Y);

            /* Calculate area of triangle PAB */
            double A3 = Area(_angles[0].X, _angles[0].Y, _angles[1].X, _angles[1].Y, point.X, point.Y);

            /* Check if sum of A1, A2 and A3 is same as A */
            return (A == A1 + A2 + A3);
        }

        protected override void SetControlPoints()
        {
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));

            float xMidWidth = (botRightCorner.X - topLeftCorner.X) / 2;
            float xMidHeight = (botRightCorner.Y - topLeftCorner.Y) / 2;

            _controlPoints[0] = new PointF(topLeftCorner.X, topLeftCorner.Y);
            _controlPoints[1] = new PointF(topLeftCorner.X + xMidWidth, topLeftCorner.Y);
            _controlPoints[2] = new PointF(topLeftCorner.X + 2 * xMidWidth, topLeftCorner.Y);
            _controlPoints[3] = new PointF(topLeftCorner.X + 2 * xMidWidth, topLeftCorner.Y + xMidHeight);
            _controlPoints[4] = new PointF(botRightCorner.X, botRightCorner.Y);
            _controlPoints[5] = new PointF(topLeftCorner.X + xMidWidth, botRightCorner.Y);
            _controlPoints[6] = new PointF(topLeftCorner.X, botRightCorner.Y);
            _controlPoints[7] = new PointF(topLeftCorner.X, topLeftCorner.Y + xMidHeight);
        }

        public override void MoveControlPoint(int controlNum, PointF point)
        {
            float x = point.X - _oldPos.X;
            float y = point.Y - _oldPos.Y;
            PointF topLeftCorner = new PointF(Math.Min(_startPos.X, _endPos.X), Math.Min(_startPos.Y, _endPos.Y));
            PointF botRightCorner = new PointF(Math.Max(_startPos.X, _endPos.X), Math.Max(_startPos.Y, _endPos.Y));

            ChangeCurControlPoint(topLeftCorner, botRightCorner);

            if (0 == controlNum)
            {
                topLeftCorner = new PointF(topLeftCorner.X + x, topLeftCorner.Y + y);
            }
            else if (1 == controlNum)
            {
                topLeftCorner.Y += y;
            }
            else if (2 == controlNum)
            {
                topLeftCorner.Y += y;
                botRightCorner.X += x;
            }
            else if (3 == controlNum)
            {
                botRightCorner.X += x;
            }
            else if (4 == controlNum)
            {
                botRightCorner = new PointF(botRightCorner.X + x, botRightCorner.Y + y);
            }
            else if (5 == controlNum)
            {
                botRightCorner.Y += y;
            }
            else if (6 == controlNum)
            {
                topLeftCorner.X += x;
                botRightCorner.Y += y;
            }
            else if (7 == controlNum)
            {
                topLeftCorner.X += x;
            }
            _startPos = new PointF(Math.Min(topLeftCorner.X, botRightCorner.X), Math.Min(topLeftCorner.Y, botRightCorner.Y));
            _endPos = new PointF(Math.Max(topLeftCorner.X, botRightCorner.X), Math.Max(topLeftCorner.Y, botRightCorner.Y));
        }

        private void ChangeCurControlPoint(PointF topLeftCor, PointF botRightCor)
        {
            if (botRightCor.X - topLeftCor.X == 0)
            {
                if (botRightCor.Y - topLeftCor.Y == 0)
                {
                    if (0 == _curControlPoint)
                        _curControlPoint = 4;
                    else if (4 == _curControlPoint)
                        _curControlPoint = 0;
                    else if (2 == _curControlPoint)
                        _curControlPoint = 6;
                    else
                        _curControlPoint = 2;
                }
                else
                {
                    if (0 == _curControlPoint)
                        _curControlPoint = 2;
                    else if (2 == _curControlPoint)
                        _curControlPoint = 0;
                    else if (7 == _curControlPoint)
                        _curControlPoint = 3;
                    else if (3 == _curControlPoint)
                        _curControlPoint = 7;
                    else if (6 == _curControlPoint)
                        _curControlPoint = 4;
                    else
                        _curControlPoint = 6;
                }

                return;
            }

            if (botRightCor.Y - topLeftCor.Y < 1.0)
            {
                if (0 == _curControlPoint)
                    _curControlPoint = 6;
                else if (6 == _curControlPoint)
                    _curControlPoint = 0;
                else if (1 == _curControlPoint)
                    _curControlPoint = 5;
                else if (5 == _curControlPoint)
                    _curControlPoint = 1;
                else if (2 == _curControlPoint)
                    _curControlPoint = 4;
                else
                    _curControlPoint = 2;
            }
        }

        private double Area(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
        }
    }
}
