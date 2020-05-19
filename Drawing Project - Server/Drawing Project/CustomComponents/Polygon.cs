using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Drawing_Project.CustomComponents
{
    class Polygon : Shape
    {
        protected int _nAngle;
        protected PointF _controlTopPoint;
        protected PointF[] _angles;

        public Polygon(int nAngle) : base()
        {
            _nControlPoints = 8;
            _controlPoints = new List<PointF>();

            for (int i = 0; i < _nControlPoints; ++i)
            {
                _controlPoints.Add(new PointF());
            }

            _nAngle = nAngle;
            _angles = new PointF[_nAngle];
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(_pen, _angles);
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

        protected virtual void SetAngles()
        {
            
        }

        protected virtual void SetControlTopPoint(PointF point)
        {
            _controlTopPoint = new PointF(point.X, point.Y);
        }

        protected virtual double CalculateArea(PointF point)
        {
            return 0;
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
    }
}
