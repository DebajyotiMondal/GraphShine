using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GeometricPrimitives;

namespace GraphShine.Utilities
{
    public class Distance
    {
        public static double EuclideanDist(Point p, Point q)
        {
            return Math.Sqrt(Math.Pow(p.x - q.x, 2) + Math.Pow(p.y - q.y, 2));
        }
        public static double ManhattanDist(Point p, Point q)
        {
            return Math.Abs(p.x - q.x) + Math.Abs(p.y - q.y);
        }

        public static double LpDist(Point p, Point q, double metric)
        {
            double deltax = p.x - q.x;
            double deltay = p.y - q.y;
            double xMetric = Math.Pow(deltax, metric);
            double yMetric = Math.Pow(deltay, metric);
            return Math.Pow( xMetric + yMetric, 1/metric );
        }
    }
}
