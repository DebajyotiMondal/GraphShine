using System;
using System.IO;
using GraphShine.GraphPrimitives;

namespace GraphShine.IO
{
    class GraphReader
    {
        /* Input: A file with the first line VertexCount,EdgeCount and the subsequent lines as edges such as nodex,nodey
         * Output: A graph with vertexlist, edgelist, and adjacencylist each list corresponds to a dictionary of unique Ids
         */
        public static Graph Read(String filename)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                int a, b;
                String line;
                char[] delimiterChars = {' ', ',', '.', ':', '\t'};
                
                line = file.ReadLine();
                string[] words = line.Split(delimiterChars);
                if (words.Length != 2) throw new Exception();
                a = int.Parse(words[0]); //n
                b = int.Parse(words[1]); //m
                Graph graph = new Graph();

                int edgeIndex = 0;
                while ((line = file.ReadLine()) != null)
                {
                    words = line.Split(delimiterChars);
                    if (words.Length != 2) throw new Exception();
                    
                    a = int.Parse(words[0]);
                    b = int.Parse(words[1]);
                    //update vertexlist
                    if (graph.GetVertex(a) == null)
                    {
                        Vertex vertex = new Vertex(a);
                        graph.InsertVertex(vertex);
                    }
                    if (graph.GetVertex(b) == null)
                    {
                        Vertex vertex = new Vertex(b);
                        graph.InsertVertex(vertex);
                    }

                    Edge edge = new Edge(a, b, edgeIndex);
                    if (!graph.ContainsEdge(edge))
                    {
                        graph.InsertEdge(edge);
                        edgeIndex++;
                    }

                }
                return graph;
            }
            catch (Exception e)
            {
                Console.WriteLine("Input File Reading Error.");
            }
            return null;
        }

        public static Graph2D ReadGeometricGraph(String filename)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                int a, b;
                String line;
                char[] delimiterChars = {' ', ',', ':', '\t'};
                
                line = file.ReadLine();
                string[] words = line.Split(delimiterChars);
                if (words.Length != 2) throw new Exception();
                var n = int.Parse(words[0]); //n
                var m = int.Parse(words[1]); //m
                Graph2D graph = new Graph2D();

                int edgeIndex = 0;
                for (int i = 1; i <= n; i++)
                {
                    if ((line = file.ReadLine()) == null)throw new Exception();
                    words = line.Split(delimiterChars);
                    

                    a = int.Parse(words[0]);
                    var x = double.Parse(words[1]);
                    var y = double.Parse(words[2]);

                    if (graph.GetVertex(a) == null)
                    {
                        Vertex2D vertex = new Vertex2D(a,x,y);
                        graph.InsertVertex(vertex);
                    }
                }
                while ((line = file.ReadLine()) != null)
                {
                    words = line.Split(delimiterChars);                    
                    
                    a = int.Parse(words[0]);
                    b = int.Parse(words[1]);
                    //update vertexlist
                    if (graph.GetVertex(a) == null) throw new Exception();
                    if (graph.GetVertex(b) == null) throw new Exception();

                    Edge edge = new Edge(a, b, edgeIndex);
                    if (!graph.ContainsEdge(edge))
                    {
                        graph.InsertEdge(edge);
                        edgeIndex++;
                    }

                }
                return graph;
            }
            catch (Exception e)
            {
                Console.WriteLine("Input File Reading Error.");
            }
            return null;
        }
    
        
    }
}
