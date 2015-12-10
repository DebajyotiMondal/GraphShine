﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GraphShine.GraphPrimitives
{
    public class Graph
    {
        public int VertexCount;
        public int EdgeCount;
        public Dictionary<int,Vertex> Vertices = new Dictionary<int,Vertex>();
        public Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
        public Dictionary<int, Dictionary<int, int>> AdjList = new Dictionary<int, Dictionary<int,int>>();

        public Graph()
        {
            VertexCount = 0;
            EdgeCount = 0;
        }

        public Graph(int n, int m)
        {            
            VertexCount = n;
            EdgeCount = m;
        }

        public void InsertVertex(Vertex v)
        {
            Vertices.Add(v.Id, v);
            AdjList[v.Id] = new Dictionary<int, int>();
            VertexCount = Vertices.Count;
        }

        public void DeleteVertex(Vertex v)
        {
            //delete associated edges
            Dictionary<int, int> NeighborList = AdjList[v.Id];
            foreach (var neighborId in NeighborList.Keys)
            {
                int edgeId = NeighborList[neighborId];
                Edges.Remove(edgeId);                
                //update neighbors neighbor list
                Dictionary<int, int> NeighborsNeighborList = AdjList[neighborId];
                NeighborsNeighborList.Remove(v.Id);
            }
            EdgeCount = Edges.Count;
            AdjList.Remove(v.Id);


            //delete the vertex itself
            Vertices.Remove(v.Id);
            VertexCount = Vertices.Count;
        }

        public void DeleteVertex(int vId)
        {
            DeleteVertex(Vertices[vId]);
        }

        public void DeleteEdge(int vId, int wId)
        {
            DeleteEdge(Vertices[vId],Vertices[wId]);
        }

        public void DeleteEdge(Vertex v, Vertex w)
        {
            Dictionary<int, int> NeighborList = AdjList[v.Id];
            int edgeId = NeighborList[w.Id];
            
            Edges.Remove(edgeId);
            EdgeCount = Edges.Count;

            NeighborList.Remove(w.Id);
 
            NeighborList = AdjList[w.Id];
            NeighborList.Remove(v.Id);
        }

        public Dictionary<int,int>.ValueCollection IdsOfIncidentEdges(Vertex v)
        {            
            return AdjList[v.Id].Values;
        }


        public Dictionary<int, int>.KeyCollection IdsOfIncidentVertices(Vertex v)
        {
            return AdjList[v.Id].Keys;
        }

        public int InsertEdge(Edge e)
        {
            bool alreadyExists = ContainsEdge(e);
            if (!alreadyExists)
            {
                //update edgelist                        
                Edges.Add(e.Id,e);
                EdgeCount = Edges.Count;

                int a = e.StartNodeId;
                int b = e.EndNodeId;

                //update adjacency list
                if (!AdjList.ContainsKey(a))
                {
                    Dictionary<int, int> neighborsList = new Dictionary<int, int>();
                    neighborsList.Add(b,e.Id);
                    AdjList.Add(a, neighborsList);
                }
                else AdjList[a].Add(b,e.Id);
                if (!AdjList.ContainsKey(b))
                {
                    Dictionary<int, int> neighborsList = new Dictionary<int, int>();
                    neighborsList.Add(a, e.Id);
                    AdjList.Add(b, neighborsList);
                }
                else AdjList[b].Add(a,e.Id);
                /*
                Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b) ^
                AdjList.ContainsKey(b) && AdjList[b].ContainsKey(a)));
                 */
            }
            return EdgeCount;
        }

        

        public bool ContainsEdge(Edge e)
        {
            int a = e.StartNodeId;
            int b = e.EndNodeId;
            /*
            Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b) ^
                AdjList.ContainsKey(b) && AdjList[b].ContainsKey(a)));
             */
            return AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b);
        }

        public bool AreAdjacent(Vertex v, Vertex w)
        {
            int a = v.Id;
            int b = w.Id;
            Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b) ^
                AdjList.ContainsKey(b) && AdjList[b].ContainsKey(a)));
            return AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b);
        }

        public void isConsistent()
        {
            Debug.Assert(VertexCount == Vertices.Count);
            Debug.Assert(EdgeCount == Edges.Count);
        }
    }

    public class Vertex{
        public int Id;
        public double Weight;
        public int Color;
        public int degree;
        public Vertex(int a)
        {
            Id = a;
        }
    }
    public class Edge
    {
        public int Id;
        public int StartNodeId;
        public int EndNodeId;
        public int Weight;

        public Edge(int v1, int v2, int EdgeId)
        {
            Id = EdgeId;
            StartNodeId = v1;
            EndNodeId = v2;
        }
    }
}