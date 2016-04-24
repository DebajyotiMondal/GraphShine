using System;

namespace GraphShine.GeometricPrimitives
{
    public class Point2D
    {
        public int Id;
        public double x;
        public double y;
        
        public Point2D(double a, double b, int c)
        {
            Id = c;
            x = a;
            y = b;
        }
        public Point2D(double a, double b)
        {
            x = a;
            y = b;
        }
        public Point2D(Point2D q)
        {
            Id = q.Id;
            x = q.x;
            y = q.y;
        }
        public Point2D() 
        {
            Id = -1;
            x = 0;
            y = 0;
        }
         
        public static bool operator ==(Point2D a, Point2D b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Point2D a, Point2D b)
        {
            return !(a == b);
        }


        public static Rectangle GetBoundary(Point2D[] P)
        {
            double minX = double.MaxValue;
            double minY = double.MaxValue;
            double maxX = double.MinValue;
            double maxY = double.MinValue;

            for (int i = 0; i < P.Length; i++)
            {
                if (minX > P[i].x) minX = P[i].x;
                if (minY > P[i].y) minY = P[i].y;
                if (maxX < P[i].x) maxX = P[i].x;
                if (maxY < P[i].y) maxY = P[i].y;                
            }
            return new Rectangle(minX, minY, maxX, maxY);
        }

    }
}
