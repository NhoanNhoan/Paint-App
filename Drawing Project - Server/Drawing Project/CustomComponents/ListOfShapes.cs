using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing_Project.CustomComponents
{
    class ListOfShapes
    {
        internal List<CustomComponents.Shape> Shapes
        {
            get
            {
                return Shapes;
            }
            set
            {
                Shapes = value;
            }
        }

        void AddANewShape(CustomComponents.Shape _shape)
        {
            Shapes.Add(_shape);
        }
    }
}
