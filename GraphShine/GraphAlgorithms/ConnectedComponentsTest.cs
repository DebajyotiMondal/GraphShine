using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    /* 
     * ConnectedComponentsTest.getAllComponentsTest();
     */

    public class ConnectedComponentsTest
    {
        public static void getAllComponentsTest()
        {
            
            Graph G = new Graph();

            G.InsertVertex(new Vertex(0));
            G.InsertVertex(new Vertex(1));
            G.InsertVertex(new Vertex(2));
            G.InsertVertex(new Vertex(3));
            G.InsertVertex(new Vertex(4));
            G.InsertVertex(new Vertex(5));
            G.InsertVertex(new Vertex(6));
            G.InsertVertex(new Vertex(7));
            G.InsertVertex(new Vertex(8));
            G.InsertVertex(new Vertex(9));



            G.InsertEdge(new Edge(0, 1, 0));
            G.InsertEdge(new Edge(0, 3, 2));
            G.InsertEdge(new Edge(3, 4, 3));
            G.InsertEdge(new Edge(1, 2, 4));
            G.InsertEdge(new Edge(1, 3, 5));
            G.InsertEdge(new Edge(2, 3, 1));
            G.InsertEdge(new Edge(7, 8, 6));
            G.InsertEdge(new Edge(8, 9, 7));

            List<Graph> components = ConnectedComponents.getAllComponents(G);

            foreach (var comp in components)
            {
                foreach (var vertexId in comp.Vertices.Keys)
                {
                    Console.Write(vertexId + " ");
                }
                Console.WriteLine("");
            }

        }
    }
}
