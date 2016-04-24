using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphShine.GeometricPrimitives
{
    public class HInterval
    {
        public int Id;
        public Point2D A;
        public Point2D B;

        public HInterval(HInterval givenHInterval)
        {
            A = new Point2D(givenHInterval.A.x, givenHInterval.A.y);
            B = new Point2D(givenHInterval.B.x, givenHInterval.B.y);
            Id = givenHInterval.Id;
        }

        public HInterval(Point2D s, Point2D t)
        {
            A = s;
            B = t;
        }

        public HInterval(Point2D s, Point2D t, int w)
        {
            Id = w;
            A = s;
            B = t;
        }

        public HInterval()
        {
            Id = -1;
        }

        public bool Contains(HInterval givenHInterval)
        {
            return (A.x<= givenHInterval.A.x  && B.x >= givenHInterval.B.x);
        }

        public bool Contains(Point2D p)
        {
            return (A.x <= p.x && B.x >= p.x);
        }
        public bool Intersects(HInterval givenHInterval)
        {
            return (givenHInterval.A.x <= A.x && A.x <= givenHInterval.B.x) ||
                (givenHInterval.A.x <= B.x && B.x <= givenHInterval.B.x)||
                givenHInterval.Contains(this)||
                Contains(givenHInterval);
        }
    }
}
