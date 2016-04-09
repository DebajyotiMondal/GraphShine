﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    class MinimumSpanningTreeTest
    {
        public static void MSTtest(){

            Graph G = new Graph();

            Vertex v0 = G.CreateVertex();
            Vertex v1 = G.CreateVertex();
            Vertex v2 = G.CreateVertex();
            Vertex v3 = G.CreateVertex();
            Vertex v4 = G.CreateVertex();

            G.InsertEdge(v0, v1);
            G.InsertEdge(v0, v3);
            G.InsertEdge(v3, v4);
            G.InsertEdge(v1, v2);
            G.InsertEdge(v1, v3);
            G.InsertEdge(v2, v3);

            int [] bfsOrder = VertrexOrdering.BfsOrdering(G, G.GetVertex(0));
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
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            Console.WriteLine("-------");
            G.DeleteEdge(5,6);
            G.DeleteEdge(6,5);
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            Console.WriteLine("-------");
            G.InsertEdge(v4, v5);
            G.InsertEdge(v5, v6);
            G.InsertEdge(v6, v7);
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            Console.WriteLine("-------");
            G.DeleteEdge(5, 6);
            G.DeleteEdge(6, 5);
            bfsOrder = VertrexOrdering.BfsOrdering(G, G.GetVertex(4));
            for (int i = 0; i < bfsOrder.Length; i++)
                Console.WriteLine(bfsOrder[i] + " ");

            int k = 0;
            foreach (var e in G.EdgeList())
            {                
                e.Weight = k++;
                Console.WriteLine(e.StartNodeId + " " + e.EndNodeId + " " + e.Weight);
            }
            
            var TreeList = MinimumSpanningTree.MSTPrim(G);

            Console.WriteLine("Printing Trees:");
            foreach (var T in TreeList.Keys)
            {
                Console.WriteLine("Weight = " + TreeList[T]);
                T.printTree();                                                
            }
            

        }
    }
}
