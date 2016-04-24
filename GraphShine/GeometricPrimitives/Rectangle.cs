using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphShine.GeometricPrimitives
{
    public class Rectangle
    {
        public Point2D BottomLeftCorner;
        public Point2D TopRightCorner;

        public Rectangle(double a, double b, double x, double y)
        {
            BottomLeftCorner = new Point2D(a, b, 0);
            TopRightCorner = new Point2D(x, y, 1);
        }
    }
}
