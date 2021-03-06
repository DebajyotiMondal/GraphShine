﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    public class ConnectedComponents
    {
        public static List<Graph> getAllComponents(Graph G)
        {
            //initially everybody is black
            foreach (Vertex vertex in G.VertexList())
                vertex.Color = 0;
            
            List<Graph> components = new List<Graph>();
            
            foreach (var vertex in G.VertexList())
            {
                if (vertex.Color == 0)
                {
                    var comp = BfsOrdering(G,vertex);
                    components.Add(comp);
                }
            }
            return components;
        }

        internal static Graph BfsOrdering(Graph graph, Vertex startVertex)
        {
            int[] BfsOrder = new int[graph.VertexCount];
            int index = 0;


            Graph component = new Graph();

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
                foreach (var neighbor in graph.NeighborList(currentVertex))
                    if (neighbor.Color == 0)
                    {
                        //assign an intermediate color so that to avoid multiple enqueue 
                        neighbor.Color = 2;
                        Q.Enqueue(neighbor);
                    }

            }
            if (index < graph.VertexCount)
            {
                //Console.WriteLine("Input to BfsOrdering is a disconnected graph.");
                for (int i = index; i < graph.VertexCount; i++) BfsOrder[i] = -1;
            }

            //build the graph
            for (int i = 0; i < index; i++)
            {
                Vertex v = graph.GetVertex(BfsOrder[i]);
                component.InsertVertex(v);
            }

            //add the edges
            for (int i = 0; i < index; i++)
            {
                Vertex v = graph.GetVertex(BfsOrder[i]);
                foreach (var edgeId in graph.IdsOfIncidentEdges(v))
                    component.InsertEdge(graph.GetEdge(edgeId));                
            }
            return component;
        }
    }
}
