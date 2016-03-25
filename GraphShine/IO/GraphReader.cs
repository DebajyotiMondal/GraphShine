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
                a = int.Parse(words[0]);
                b = int.Parse(words[1]);
                Graph graph = new Graph(a, b);

                int edgeIndex = 0;
                while ((line = file.ReadLine()) != null)
                {
                    words = line.Split(delimiterChars);
                    if (words.Length != 2) throw new Exception();
                    
                    a = int.Parse(words[0]);
                    b = int.Parse(words[1]);
                    //update vertexlist
                    if (!graph.Vertices.ContainsKey(a))
                    {
                        Vertex vertex = new Vertex(a);
                        graph.InsertVertex(vertex);
                    }
                    if (!graph.Vertices.ContainsKey(b))
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
    }
}
