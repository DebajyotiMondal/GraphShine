


using System;
using GraphShine.GraphPrimitives;

/*
 * GraphReaderTest.GraphReadingTest();
 */

namespace GraphShine.IO
{
    public class GraphReaderTest
    {

        //Input Output Tests
        public static void GraphReadingTest()
        {
            String filename = "../../data/testgraph.txt";
            Graph graph = GraphReader.Read(filename);

            Console.WriteLine("Number Of Vertices = " + graph.VertexCount);
            foreach (Vertex vertex in graph.Vertices.Values)
                Console.Write(vertex.Id + " ");
            Console.WriteLine("");
            Console.WriteLine("Number Of Edges = " + graph.EdgeCount);
            foreach (Edge edge in graph.Edges.Values)
                Console.Write("(" + edge.StartNodeId + "," + edge.EndNodeId + ")");
            Console.WriteLine("");
            Console.WriteLine("Traverse Adjacency List");

            foreach (var VertexId in graph.AdjList.Keys)
            {
                Console.Write(VertexId + "-> ");
                foreach (var neighborId in graph.AdjList[VertexId])
                {
                    Console.Write(neighborId + " ");
                }
                Console.WriteLine();
            }

            //check consistency of the data structure
            graph.isConsistent();
        }
 
    }
}