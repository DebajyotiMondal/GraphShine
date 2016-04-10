using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.Utilities
{
        class Angle
        {
            //compute the angle at A if it is less than pi/2, otherwise return pi/2

            /*TESTS
                Console.WriteLine("0 0, 1 1, 0 1" +Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(1, 1), new Vertex(0, 1)));
                Console.WriteLine("0 0, -1 1, 0 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(-1, 1), new Vertex(0, 1)));
                Console.WriteLine("0 0, -1 -1, 0 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(-1, -1), new Vertex(0, 1)));
                Console.WriteLine("0 0, 1 -1, 0 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(1, -1), new Vertex(0, 1)));
                Console.WriteLine("0 0, -1 1, 1 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(-1, 1), new Vertex(1, 1)));
                Console.WriteLine("0 0, -1 -1, 1 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(-1, -1), new Vertex(1, 1)));
                Console.WriteLine("0 0, 1 -1, 1 1" + Angle.getAngleIfSmallerThanPIby2(new Vertex(0, 0), new Vertex(1, -1), new Vertex(1, 1)));
             */
            public static double getAngleIfSmallerThanPIby2(Vertex2D A, Vertex2D B, Vertex2D C)
            {

                double xDiff1 = A.X - B.X;
                double yDiff1 = A.Y - B.Y;

                double xDiff2 = A.X - C.X;
                double yDiff2 = A.Y - C.Y;

                if (B.X >= A.X && C.X >= A.X && B.Y >= A.Y && C.Y >= A.Y)
                    return Math.Abs(Math.Atan2(yDiff1, xDiff1) - Math.Atan2(yDiff2, xDiff2));
                if (B.X >= A.X && C.X <= A.X && B.Y >= A.Y && C.Y <= A.Y)
                    return Math.Abs(Math.Atan2(yDiff1, xDiff1) - Math.Atan2(yDiff2, xDiff2));
                if (B.X <= A.X && C.X >= A.X && B.Y <= A.Y && C.Y >= A.Y)
                    return Math.Abs(Math.Atan2(yDiff1, xDiff1) - Math.Atan2(yDiff2, xDiff2));
                if (B.X <= A.X && C.X <= A.X && B.Y <= A.Y && C.Y <= A.Y)
                    return Math.Abs(Math.Atan2(yDiff1, xDiff1) - Math.Atan2(yDiff2, xDiff2));


                return Math.PI / 2;//Abs(Math.Atan2(yDiff1, xDiff1) - Math.Atan2(yDiff2, xDiff2));
            }

            public static double getClockwiseAngle(Vertex2D A, Vertex2D B, Vertex2D C)
            {

                double xDiff1 = A.X - B.X;
                double yDiff1 = A.Y - B.Y;

                double xDiff2 = A.X - C.X;
                double yDiff2 = A.Y - C.Y;

                double dot = xDiff1 * xDiff2 + yDiff1 * yDiff2;     // dot product
                double det = xDiff1 * yDiff2 - yDiff1 * xDiff2;     // determinant
                double angle = Math.Atan2(det, dot);  // atan2(y, x) or atan2(sin, cos)

                if (angle < 0) angle = 2 * Math.PI + angle;
                return Math.Abs(angle); // in degree*180/Math.PI; 
            }
        }
    
}
