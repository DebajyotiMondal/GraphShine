


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
            foreach (Vertex vertex in graph.VertexList())
                Console.Write(vertex.Id + " ");
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
        }

        public static void GeometricGraphReadingTest()
        {
            String filename = "../../data/testgeometricgraph.txt";
            Graph2D graph = GraphReader.ReadGeometricGraph(filename);

            Console.WriteLine("Number Of Vertices = " + graph.VertexCount);
            foreach (Vertex vertex in graph.VertexList())
            {
                var v = (Vertex2D) vertex;
                Console.Write("["+v.Id + " " + v.X+ " " + v.Y+"]");
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
        }
        
    }
}