using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using GraphShine.GraphPrimitives;

namespace GraphShine.Utilities
{
    class RotationSystem
    {
        /*
 * input: a geometric planar graph
 * output: a rotation system around each verex 
 * (vertex, adjacency list(neighbor1->(2,neighbor3), neighbor3->(1,neighbor2), neighbor2->(3,neighbor1) )
 */

        public static Dictionary<Vertex, Dictionary<Vertex, KeyValuePair<Vertex,Vertex>>> getRotationSystem(Graph2D g)
        {

            var rotationSystem = new Dictionary<Vertex, Dictionary<Vertex, KeyValuePair<Vertex,Vertex>>>();

            foreach (Vertex w in g.VertexList())
            {
                var sortedNeighborList = new Vertex[g.Degree(w)];
                var clockwiseAngularDistance = new double[g.Degree(w)];

                //sort the neighbors according to the clockwise angular distance
                var apex = (Vertex2D) w;
                var referrence = (Vertex2D) g.GetInitialNeighbor(w);
                int neighborIndex = 0;
                foreach (Vertex2D z in g.NeighborList(w))
                {
                    sortedNeighborList[neighborIndex] = z;
                    var current = z;
                    clockwiseAngularDistance[neighborIndex] = Angle.getClockwiseAngle(apex, referrence, current);
                    neighborIndex++;
                }

                Array.Sort(clockwiseAngularDistance, sortedNeighborList);

                rotationSystem.Add(w,new Dictionary<Vertex, KeyValuePair<Vertex,Vertex>>());
                var list = rotationSystem[w];
                int degw = g.Degree(w);
                for ( neighborIndex = 0; neighborIndex < degw; neighborIndex++)
                {
                    var prev = neighborIndex - 1;
                    var next = neighborIndex + 1;
                    if (prev < 0) prev = degw - 1;
                    if (next == degw) next = 0;

                    var prevVertex = sortedNeighborList[prev];
                    var nextVertex = sortedNeighborList[next];

                    KeyValuePair<Vertex,Vertex> p = 
                        new KeyValuePair<Vertex, Vertex>(prevVertex,nextVertex);

                    list.Add(sortedNeighborList[neighborIndex], p);
                    
                }

            }
            return rotationSystem;
        }


    }
}
