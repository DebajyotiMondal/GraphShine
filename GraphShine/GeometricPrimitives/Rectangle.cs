using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphShine.GeometricPrimitives
{
    public class Rectangle
    {
        public Point BottomLeftCorner;
        public Point TopRightCorner;

        public Rectangle(double a, double b, double x, double y)
        {
            BottomLeftCorner = new Point(a, b, 0);
            TopRightCorner = new Point(x, y, 1);
        }
    }
}
