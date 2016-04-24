using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GeometricPrimitives;

namespace GraphShine.Utilities
{

    /*
     * NearestNeighborTest.L1NearestNeighborInConeTest(); 
     */

    public class NearestNeighborTest
    {
        public static void L1NearestNeighborInConeTest()
        {

            Point2D[] P = new Point2D[10];
            for(int i =0;i<P.Length;i++)P[i] = new Point2D(0,0,i);
            P[0].x = 5; P[0].y = 1;
            P[1].x = 1; P[1].y = 5;
            P[2].x = 2; P[2].y = 7;
            P[3].x = 3; P[3].y = 3;
            P[4].x = 5; P[4].y = 3;
            P[5].x = 6; P[5].y = 6;
            P[6].x = 3; P[6].y = 2;
            P[7].x = 9; P[7].y = 8;
            P[8].x = 8; P[8].y = 0;
            P[9].x = 0; P[9].y = 0;
            
            Dictionary<int, int> N1 = NearestNeighbor.L1NearestNeighborInCone(P, 1);
            Dictionary<int, int> N2 = NearestNeighbor.L1NearestNeighborInCone(P, 2);
            Dictionary<int, int> N3 = NearestNeighbor.L1NearestNeighborInCone(P, 3);
            Dictionary<int, int> N4 = NearestNeighbor.L1NearestNeighborInCone(P, 4);
            Dictionary<int, int> N5 = NearestNeighbor.L1NearestNeighborInCone(P, 5);
            Dictionary<int, int> N6 = NearestNeighbor.L1NearestNeighborInCone(P, 6);
            Dictionary<int, int> N7 = NearestNeighbor.L1NearestNeighborInCone(P, 7);
            Dictionary<int, int> N8 = NearestNeighbor.L1NearestNeighborInCone(P, 8);
            
        }

    }
}
