using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace Drawing_Project
{
    public class DoubleBufferedPanel : Panel
    {
        #region Attributes

        private bool _isPressDown;
        internal CustomComponents.Shape _shape;
        internal static List<CustomComponents.Shape> _shapes;

        #endregion

        #region Contructor

        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            _shapes = new List<CustomComponents.Shape>();
        }

        #endregion

        #region Methods

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (null == _shape)
            {
                return;
            }

            PointF mousePos = new PointF(e.X, e.Y);
            CustomComponents.Shape curShape = null;

            //if (Form1._isFill)
            //{
            //    curShape = FindShapeInCursor(mousePos);
            //    if (curShape != null)
            //    {
            //        curShape.ShapeBrush = Form1._brush.Clone() as SolidBrush;
            //        Invalidate();
            //    }
            //}

            curShape = FindControlPointInMousePoint(mousePos);

            if (null != curShape)
            {
                _isPressDown = true;
                _shape = curShape;
                _shape.IsSelected = true;
                Invalidate();
                _shape.MouseDown(e);
                return;
            }

            curShape = FindShapeInCursor(mousePos);

            if (null == curShape)
            {
                if (!_shape.IsDrawing)
                {
                    if (_shape.IsSelected)
                    {
                        _shape.IsSelected = false;
                        Invalidate();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    _isPressDown = true;
                    _shape.MouseDown(e);
                    return;
                }
            }
            else
            {
                _isPressDown = true;
                _shape = curShape;
                _shape.IsSelected = true;
                _shape.ShapePen = new Pen(Form1._drawingPen.Color, Form1._drawingPen.Width);
                _shape.MouseDown(e);
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (null == _shape) return;
            if (_isPressDown)
            {
                _isPressDown = false;
                if (_shape.IsDrawing) { _shapes.Add(_shape); }
                _shape.MouseUp(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (null == _shape) return;
            PointF mousePos = new PointF(e.X, e.Y);

            if (FindShapeInCursor(mousePos) != null)
                Cursor = Cursors.SizeAll;
            else if (_shape.nControlPoint != _shape.CheckPointOnControlPoints(mousePos))
                Cursor = Cursors.SizeNESW;
            else
                Cursor = Cursors.Default;

            if (_isPressDown)
            {
                _shape.MouseMove(e);
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            bool hasExist = false;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            foreach(CustomComponents.Shape shape in _shapes)
            {
                if (_shape == shape)
                {
                    hasExist = true;
                }

                shape.Paint(e);
                if (null != _shape && _shape.IsSelected)
                {
                    _shape.DrawControlPoints(e.Graphics);
                }
            }

            if (null != _shape && !hasExist) _shape.Paint(e);
        }

        private CustomComponents.Shape FindShapeInCursor(PointF position)
        {
            foreach(CustomComponents.Shape shape in _shapes)
            {
                if (shape.CheckPointOnBound(position))
                    return shape;
            }

            return null;
        }

        private CustomComponents.Shape FindControlPointInMousePoint(PointF mousePoint)
        {
            foreach(CustomComponents.Shape shape in _shapes)
            {
                if (shape.nControlPoint != shape.CheckPointOnControlPoints(mousePoint))
                    return shape;
            }

            return null;
        }

        public void Redrawing(PaintEventArgs e)
        {
            OnPaint(e);
        }

        #endregion
    }
}
