using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing_Project.CustomComponents
{
    enum ShapeType
    {
        Line,
        Rectangle,
        Ellipse,
        Hexagon,
        Triangle,
        Circle
    }

    class ShapeFactory
    {
        public Shape shape;

        public ShapeFactory(ShapeType type)
        {
            switch(type)
            {
                case ShapeType.Line:
                    {
                        shape = new Line();
                        break;
                    }
                case ShapeType.Rectangle:
                    {
                        shape = new RectangleShape();
                        break;
                    }
                case ShapeType.Ellipse:
                    {
                        shape = new Ellipse();
                        break;
                    }
            }
        }

        public Shape GetShape()
        {
            return shape;
        }
    }
}
