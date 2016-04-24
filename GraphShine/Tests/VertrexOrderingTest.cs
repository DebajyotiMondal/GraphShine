using System;
using GraphShine.GraphAlgorithms;
using GraphShine.GraphPrimitives;
using GraphShine.IO;

namespace GraphShine.Tests
{
    /*
     * VertrexOrderingTest.BfsTest();
     */
    public class VertrexOrderingTest
    {
        public static  void BfsTest()
        {
            Graph G = new Graph();

            Vertex v0 = G.CreateVertex();
            Vertex v1 = G.CreateVertex();
            Vertex v2 = G.CreateVertex();
            Vertex v3 = G.CreateVertex();
            Vertex v4 = G.CreateVertex();

            G.InsertEdge(v0,v1);
            G.InsertEdge(v0,v3);
            G.InsertEdge(v3, v4);
            G.InsertEdge(v1, v2);
            G.InsertEdge(v1, v3);
            G.InsertEdge(v2, v3);

            int [] bfsOrder = VertexOrdering.BfsOrdering(G, G.GetVertex(0));
            for (int i = 0; i < bfsOrder.Length; i++) 
                Console.WriteLine(bfsOrder[i]+" ");

            Console.WriteLine("-------");
            G.DeleteVertex(0);
            Vertex v5 = G.CreateVertex();
            Vertex v6 = G.CreateVertex();
            Vertex v7 = G.CreateVertex();
            G.InsertEdge(v4, v5);
            G.InsertEdge(v5, v6);
            G.InsertEdge(v6, v7);
            bfsOrder = VertexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            Console.WriteLine("-------");
            G.DeleteEdge(5,6);
            bfsOrder = VertexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");


        }

        public static void CanonicalOrderingTest()
        {
            String filename = "../../data/testgeometricgraph.txt";
            Graph2D graph = GraphReader.ReadGeometricGraph(filename);
            PlanarGraph g = new PlanarGraph(graph);

            Vertex root_l = g.GetVertex(1);
            Vertex root_m = g.GetVertex(15);
            Vertex root_r = g.GetVertex(2); 
            var ordering = VertexOrdering.CanonicalOrdering(g,root_l,root_m,root_r);
        }
    }
}
