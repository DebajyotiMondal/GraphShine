﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.IO;

namespace GraphShine.GraphPrimitives
{
    class PlanarGraphTest
    {
        public static void PlanarGraphFunctionsTest()
        {
            String filename = "../../data/testgeometricgraph.txt";
            Graph2D graph = GraphReader.ReadGeometricGraph(filename);

            Console.WriteLine("Number Of Vertices = " + graph.VertexCount);
            foreach (Vertex vertex in graph.VertexList())
            {
                var v = (Vertex2D)vertex;
                Console.Write("[" + v.Id + " " + v.X + " " + v.Y + "]");
            }
            Console.WriteLine("");
            Console.WriteLine("Number Of Edges = " + graph.EdgeCount);
            foreach (Edge edge in graph.EdgeList())
                Console.Write("(" + edge.StartNodeId + "," + edge.EndNodeId + ")");
            Console.WriteLine("");
            Console.WriteLine("Traverse Adjacency List");

            foreach (var v in graph.VertexList())
            {
                Console.Write(v.Id + "-> ");
                foreach (var neighbor in graph.NeighborList(v))
                {
                    Console.Write(neighbor.Id + " ");
                }
                Console.WriteLine();
            }

            //check consistency of the data structure
            graph.isConsistent();






            PlanarGraph pg = new PlanarGraph(graph);
            Console.WriteLine("Printing Planar Graph Adjacency List");
            foreach (Vertex v in pg.VertexList())
            {
                Console.Write(v + " -> " );
                foreach (var w in pg.NeighborOrderedList(v))
                {
                    var acw = pg.getAntiClockNeighbor(v,w);
                    var cw = pg.getClockNeighbor(v, w);
                    Console.Write(w + "-["+acw+","+cw+"]"+ "  ");
                }
                Console.WriteLine();
            }

        }
        
    }
}
