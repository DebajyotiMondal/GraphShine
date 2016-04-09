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

            Vertex v0 = G.CreateVertex();
            Vertex v1 = G.CreateVertex();
            Vertex v2 = G.CreateVertex();
            Vertex v3 = G.CreateVertex();
            Vertex v4 = G.CreateVertex();
            Vertex v5 = G.CreateVertex();
            Vertex v6 = G.CreateVertex();
            Vertex v7 = G.CreateVertex();
            Vertex v8 = G.CreateVertex();
            Vertex v9 = G.CreateVertex();



            G.InsertEdge(v0, v1);
            G.InsertEdge(v0, v3);
            G.InsertEdge(v3, v4);
            G.InsertEdge(v1, v2);
            G.InsertEdge(v1, v3);
            G.InsertEdge(v2, v3);
            G.InsertEdge(v7, v8);
            G.InsertEdge(v8, v9);

            List<Graph> components = ConnectedComponents.getAllComponents(G);

            foreach (var comp in components)
            {
                foreach (var v in comp.VertexList())
                {
                    Console.Write(v.Id + " ");
                }
                Console.WriteLine("");
            }

        }
    }
}
