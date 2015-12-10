using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    /*
     * VertrexOrderingTest.BfsTest();
     */
    public class VertrexOrderingTest
    {
        public static  void BfsTest()
        {
            Graph G = new Graph();

            G.InsertVertex(new Vertex(0));
            G.InsertVertex(new Vertex(1));
            G.InsertVertex(new Vertex(2));
            G.InsertVertex(new Vertex(3));
            G.InsertVertex(new Vertex(4));

            G.InsertEdge(new Edge(0, 1, 0));
            G.InsertEdge(new Edge(0, 3, 2));
            G.InsertEdge(new Edge(3, 4, 3));
            G.InsertEdge(new Edge(1, 2, 4));
            G.InsertEdge(new Edge(1, 3, 5));
            G.InsertEdge(new Edge(2, 3, 1));

            int [] bfsOrder = VertrexOrdering.BfsOrdering(G, G.Vertices[0]);
            for (int i = 0; i < bfsOrder.Length; i++) 
                Console.WriteLine(bfsOrder[i]+" ");

            Console.WriteLine("-------");
            G.DeleteVertex(0);
            G.InsertVertex(new Vertex(5));
            G.InsertVertex(new Vertex(6));
            G.InsertVertex(new Vertex(7));
            G.InsertEdge(new Edge(4, 5, 6));
            G.InsertEdge(new Edge(5, 6, 7));
            G.InsertEdge(new Edge(6, 7, 8));
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.Vertices[4]);
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            Console.WriteLine("-------");
            G.DeleteEdge(5,6);
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.Vertices[4]);
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");


        }

    }
}
