using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Drawing_Project
{
    public partial class Form1 : Form
    {
        internal static bool _isPen;
        internal static bool _isFill;
        internal static Pen _drawingPen;
        internal static Brush _brush;

        #region Socket

        const int PORT = 9999;
        TcpClient client_socket = new TcpClient();
        StreamReader receiver;

        void Connect()
        {
            client_socket.Connect(IPAddress.Parse("127.0.0.1"), 9999);
            receiver = new StreamReader(client_socket.GetStream());
            lbStatus.Text = "Connected";
            lbStatus.ForeColor = Color.Green;
            int nShape = int.Parse(receiver.ReadLine());
            CustomComponents.Shape shape = null;

            for (int i = 0; i < nShape; ++i)
            {
                string type = receiver.ReadLine();

                if (type == "Rectangle")
                    shape = new CustomComponents.RectangleShape();
                else if (type == "Line")
                    shape = new CustomComponents.Line();
                else if (type == "Ellipse")
                    shape = new CustomComponents.Ellipse();
                else if (type == "Triangle")
                    shape = new CustomComponents.Triangle();
                else if (type == "Hexagon")
                    shape = new CustomComponents.Hexagon();
                else if (type == "Circle")
                    shape = new CustomComponents.Circle();

                shape.nControlPoint = int.Parse(receiver.ReadLine());
                string startPosX = receiver.ReadLine();
                string startPosY = receiver.ReadLine();
                string endPosX = receiver.ReadLine();
                string endPosY = receiver.ReadLine();

                if (type == "Circle")
                {
                    shape.StartPosition = new PointF(float.Parse(startPosX), float.Parse(startPosY));
                    shape.EndPosition = new PointF(float.Parse(endPosX), float.Parse(endPosY));
                }
                else
                {
                    shape.StartPosition = new PointF(int.Parse(startPosX), int.Parse(startPosY));
                    shape.EndPosition = new PointF(int.Parse(endPosX), int.Parse(endPosY));
                }

                shape.ShapePen.Width = int.Parse(receiver.ReadLine());
                shape.IsDrawing = false;
                DoubleBufferedPanel._shapes.Add(shape);
            }

            pnSurface.Refresh();
        }

        #endregion

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _drawingPen = new Pen(Color.Black, 2);
            _brush = new SolidBrush(Color.Transparent);
            _isPen = true;

            btnItems.BackColor = Color.Silver;
            pnShapes.BringToFront();
        }

        #region Shapes

        private void btnLine_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.Line();
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.RectangleShape();
        }

        private void btnEllipse_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.Ellipse();
        }

        private void BtnIsoscelesTriangle_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.Triangle();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.Hexagon();
        }


        private void Button7_Click(object sender, EventArgs e)
        {
            _isPen = true;
            pnSurface._shape = new CustomComponents.Circle();
        }

        #endregion

        #region Menu Buttons

        private void ChangeColorButtonClick(Button btn)
        {
            if (btn != btnItems && btnItems.BackColor == Color.Silver)
            {
                btnItems.BackColor = Color.Transparent;
                return;
            }

            if (btn != btnPen && btnPen.BackColor == Color.Silver)
            {
                btnPen.BackColor = Color.Transparent;
                return;
            }

            if (btn != btnConnect && btnConnect.BackColor == Color.Silver)
            {
                btnConnect.BackColor = Color.Transparent;
                return;
            }

            if (btn != btnPen && btnPen.BackColor == Color.Silver)
            {
                btnPen.BackColor = Color.Transparent;
                return;
            }
        }

        private void BtnItems_Click(object sender, EventArgs e)
        {
            pnShapes.BringToFront();
            btnItems.BackColor = Color.Silver;
            ChangeColorButtonClick(btnItems);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            _isFill = false;
            pnColors.BringToFront();
            btnPen.BackColor = Color.Silver;
            ChangeColorButtonClick(btnPen);
        }

        private void BtnShare_Click(object sender, EventArgs e)
        {
            btnConnect.BackColor = Color.Silver;
            ChangeColorButtonClick(btnConnect);
            Connect();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Color Buttons

        private void BtnWhite_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.White;
            else
                _brush = new SolidBrush(Color.White);
        }

        private void BtnBlack_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Black;
            else
                _brush = new SolidBrush(Color.Black);
        }

        private void BtnRed_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Red;
            else
                _brush = new SolidBrush(Color.Red);
        }

        private void BtnYellow_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Yellow;
            else
                _brush = new SolidBrush(Color.Yellow);
        }

        private void BtnGreen_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Green;
            else
                _brush = new SolidBrush(Color.Green);
        }

        private void BtnBlue_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Blue;
            else
                _brush = new SolidBrush(Color.Blue);
        }

        private void BtnPink_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Pink;
            else
                _brush = new SolidBrush(Color.Pink);
        }

        private void BtnDarkTurquoise_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.DarkTurquoise;
            else
                _brush = new SolidBrush(Color.DarkTurquoise);
        }

        private void BtnPurple_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Purple;
            else
                _brush = new SolidBrush(Color.Purple);
        }

        private void BtnBrown_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Brown;
            else
                _brush = new SolidBrush(Color.Brown);
        }

        private void BtnOrange_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Orange;
            else
                _brush = new SolidBrush(Color.Orange);
        }

        private void BtnLime_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Lime;
            else
                _brush = new SolidBrush(Color.Lime);
        }

        private void BtnLightSteelBlue_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.LightSteelBlue;
            else
                _brush = new SolidBrush(Color.LightSteelBlue);
        }

        private void BtnSpringGreen_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.SpringGreen;
            else
                _brush = new SolidBrush(Color.SpringGreen);
        }

        private void BtnOrangeRed_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.OrangeRed;
            else
                _brush = new SolidBrush(Color.OrangeRed);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (_isPen)
                _drawingPen.Color = Color.Transparent;
            else
                _brush = new SolidBrush(Color.Transparent);
        }

        #endregion

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string pw = txtPenWidth.Text;
            float w;
            if (pw == String.Empty)
                w = 1;
            else
                w = int.Parse(pw);

            _drawingPen.Width = w;
        }

        private void TxtPenWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
