using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    public class VertrexOrdering
    {
        public static int[] BfsOrdering(Graph connectedGraph, Vertex startVertex)
        {
            int[] BfsOrder = new int[connectedGraph.VertexCount];
            int index = 0;

            //initially everybody is black
            foreach (Vertex vertex in connectedGraph.VertexList())
                vertex.Color = 0;
            
            //insert startVertex into Queue
            Queue<Vertex> Q = new Queue<Vertex>();
            Q.Enqueue(startVertex);
            //while Q is not empty
            while (Q.Count > 0)
            {
                //take a vertex from Q and make it white : visited
                Vertex currentVertex = Q.Dequeue();
                currentVertex.Color = 1;
                BfsOrder[index++] = currentVertex.Id;

                //find the unvisited neighbors and insert them into Q
                foreach (var neighbor in connectedGraph.NeighborList(currentVertex))
                    if (neighbor.Color == 0)
                    {
                        //assign an intermediate color so that to avoid multiple enqueue 
                        neighbor.Color = 2; 
                        Q.Enqueue(neighbor);
                    }

            }
            if (index < connectedGraph.VertexCount)
            {
                //Console.WriteLine("Input to BfsOrdering is a disconnected graph.");
                for (int i = index; i < connectedGraph.VertexCount; i++) BfsOrder[i] = -1;
            }

            return BfsOrder;
        }
    }
}