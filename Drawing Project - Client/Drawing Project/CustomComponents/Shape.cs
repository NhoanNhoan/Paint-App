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
    internal class Shape : Panel
    {
        #region Attributes

        protected bool _isDrawing;
        protected bool _isResizing;
        private bool _isSelected;
        protected int _nControlPoints;
        protected int _curControlPoint;
        protected float _r;
        protected ShapeType _type;
        protected PointF _startPos;
        protected PointF _endPos;
        protected PointF _oldPos;
        protected Pen _pen;
        protected Brush _brush;
        protected List<PointF> _controlPoints;

        protected static Pen _controlPen = new Pen(Color.Gray, 2);
        protected static Brush _controlBrush = new SolidBrush(Color.White);

        #endregion

        #region Properties

        public bool IsDrawing
        {
            get
            {
                return _isDrawing;
            }
            set
            {
                _isDrawing = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
            }
        }

        public int nControlPoint
        {
            get
            {
                return _nControlPoints;
            }
            set
            {
                _nControlPoints = value;
            }
        }

        public PointF StartPosition
        {
            get
            {
                return _startPos;
            }
            set
            {
                _startPos = value;
            }
        }

        public PointF EndPosition
        {
            get
            {
                return _endPos;
            }
            set
            {
                _endPos = value;
            }
        }

        public Pen ShapePen
        {
            get
            {
                return _pen;
            }
            set
            {
                _pen = value;
            }
        }

        public Brush ShapeBrush
        {
            set
            {
                _brush = value;
            }
        }

        #endregion

        #region Contructor

        public Shape():base()
        {
            _isDrawing = true;
            _r = 10;
            _pen = new Pen(Form1._drawingPen.Color, Form1._drawingPen.Width);
            _brush = new SolidBrush(Color.Transparent);
        }

        #endregion

        #region Methods

        public ShapeType GetShapeType()
        {
            return _type;
        }

        public virtual void Draw(Graphics graphics)
        {
        }

        public virtual bool CheckPointOnBound(PointF point)
        {
            return true;
        }

        public virtual int CheckPointOnControlPoints(PointF point)
        {
            for (int i = 0; i < _nControlPoints;++i)
            {
                RectangleF rec = new RectangleF(_controlPoints[i].X - _r / 2, _controlPoints[i].Y - _r / 2, _r, _r);
                if (rec.Contains(point))
                {
                    return i;
                }
            }

            return _nControlPoints;
        }

        public virtual void Move(PointF point)
        {
            _startPos = new PointF(_startPos.X + point.X, _startPos.Y + point.Y);
            _endPos = new PointF(_endPos.X + point.X, _endPos.Y + point.Y);
        }

        public virtual void MoveControlPoint(int controlNum, PointF point)
        {

        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            // Current position of mouse
            PointF mousePoint = new PointF(e.X, e.Y);

            // Not yet drawn
            if (_isDrawing)
            {
                _startPos = mousePoint;
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

        public virtual void MouseMove(MouseEventArgs e)
        {
            PointF mousePoint = new PointF(e.X, e.Y);

            if (_isResizing)
            {
                MoveControlPoint(_curControlPoint, mousePoint);
            }
            else if (_isDrawing)
            {
                _endPos = new PointF(e.X, e.Y);
                return;
            }
            else
            {
                PointF distance = new PointF(e.X - _oldPos.X, e.Y - _oldPos.Y);
                Move(distance);
            }

            if (!_isDrawing) _oldPos = mousePoint;
            SetControlPoints();
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                SetControlPoints();
                return;
            }

            if (_isResizing)
            {
                _isResizing = false;
            }
        }

        public virtual void Paint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            FillColor(e.Graphics);
        }

        public virtual void SetColorBrush(Color color)
        {
            _brush = new SolidBrush(color);
        }

        public virtual void FillColor(Graphics g)
        {
        }

        protected virtual void SetControlPoints()
        {

        }

        public virtual void DrawControlPoints(Graphics g)
        {
            for (int i = 0; i < _nControlPoints; ++i)
            {
                g.DrawEllipse(_controlPen, _controlPoints[i].X - _r / 2, _controlPoints[i].Y - _r / 2, _r, _r);
                g.FillEllipse(_controlBrush, _controlPoints[i].X - _r / 2, _controlPoints[i].Y - _r / 2, _r, _r);
            }
        }

        #endregion
    }

    public static class GraphicsExtensions
    {
        public static void DrawRectangle(this Graphics g, Pen pen, RectangleF rect) =>
            g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
    }
}
