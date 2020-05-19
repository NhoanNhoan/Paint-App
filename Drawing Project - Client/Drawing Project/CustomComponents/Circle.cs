using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Drawing_Project.CustomComponents
{
    class Circle : Ellipse
    {
        private float _radius;
        private PointF _centerPoint;

        public Circle() : base()
        {
            _centerPoint = new PointF();
        }

        public override void MouseDown(MouseEventArgs e)
        {
            // Current position of mouse
            PointF mousePoint = new PointF(e.X, e.Y);

            // Not yet drawn
            if (_isDrawing)
            {
                _centerPoint = mousePoint;
                return;
            }

            // This shape was drawn

            // Initialize old position
            _oldPos = mousePoint;

            // Check whether mouse position on control point or not
            _curControlPoint = CheckPointOnControlPoints(mousePoint);

            // Mouse position on a control point
            if (_curControlPoint != _nControlPoints)
            {
                _isResizing = true;
            }
        }

        public override void MouseMove(MouseEventArgs e)
        {
            PointF mousePoint = new PointF(e.X, e.Y);

            if (_isResizing)
            {
                MoveControlPoint(_curControlPoint, mousePoint);
            }
            else if (_isDrawing)
            {
                _radius = (float)Math.Sqrt(Math.Pow(e.X - _centerPoint.X, 2) +
                    Math.Pow(e.Y - _centerPoint.Y, 2));
                _startPos = new PointF(_centerPoint.X - _radius, _centerPoint.Y - _radius);
                _endPos = new PointF(_centerPoint.X + _radius, _centerPoint.Y + _radius);
                return;
            }
            else
            {
                PointF distance = new PointF(e.X - _oldPos.X, e.Y - _oldPos.Y);
                Move(distance);
                _centerPoint.X += distance.X;
                _centerPoint.Y += distance.Y;
            }

            if (!_isDrawing) _oldPos = mousePoint;
            SetControlPoints();
        }

        public override void MoveControlPoint(int controlNum, PointF point)
        {
            _radius = (float)Math.Sqrt(Math.Pow(point.X - _centerPoint.X, 2) +
                    Math.Pow(point.Y - _centerPoint.Y, 2));
            _startPos = new PointF(_centerPoint.X - _radius, _centerPoint.Y - _radius);
            _endPos = new PointF(_centerPoint.X + _radius, _centerPoint.Y + _radius);
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
