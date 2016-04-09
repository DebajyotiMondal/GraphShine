using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace GraphShine.GraphPrimitives
{
    public class Graph
    {
        public int VertexCount;
        public int EdgeCount;

        private static Stack<int> ReusableVertexId;
        private static Stack<int> ReusableEdgeId;

        private Dictionary<int,Vertex> Vertices = new Dictionary<int,Vertex>();
        private Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
        private Dictionary<int, Dictionary<int, int>> AdjList = new Dictionary<int, Dictionary<int,int>>();

        public Graph()
        {
            VertexCount = 0;
            EdgeCount = 0;

            ReusableVertexId = new Stack<int>();
            ReusableEdgeId = new Stack<int>();
        }

        public Vertex GetInitialVertex()
        {
            return Vertices.Values.First();            
        }
        


         
         
        public Vertex CreateVertex()
        {

            var vId = VertexCount;
            if (ReusableVertexId.Count > 0) vId = ReusableVertexId.Pop();
            Vertex v = new Vertex(vId);

            InsertVertex(v);

            return v;
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
                ReusableEdgeId.Push(edgeId);
                Edges.Remove(edgeId);                
                //update neighbors neighbor list
                Dictionary<int, int> NeighborsNeighborList = AdjList[neighborId];
                NeighborsNeighborList.Remove(v.Id);
            }
            EdgeCount = Edges.Count;
            AdjList.Remove(v.Id);


            //delete the vertex itself
            ReusableVertexId.Push(v.Id);
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
            if (!AreAdjacent(v, w)) return;

            Dictionary<int, int> NeighborToEdgeId = AdjList[v.Id];
            int edgeId = NeighborToEdgeId[w.Id];
            
            ReusableEdgeId.Push(edgeId);
            Edges.Remove(edgeId);
            EdgeCount = Edges.Count;

            NeighborToEdgeId.Remove(w.Id);
 
            NeighborToEdgeId = AdjList[w.Id];
            NeighborToEdgeId.Remove(v.Id);
        }

        public Dictionary<int,int>.ValueCollection IdsOfIncidentEdges(Vertex v)
        {            
            return AdjList[v.Id].Values;
        }
        
        public Dictionary<int, Vertex>.ValueCollection VertexList()
        {
            return Vertices.Values;
        }
        
        public Dictionary<int, Edge>.ValueCollection EdgeList()
        {
            return Edges.Values;
        }

        public Vertex GetVertex(int id)
        {
            if (!Vertices.ContainsKey(id))
            {
                return null;
            }
            return Vertices[id];
        }
        public Edge GetEdge(int id)
        {
            if (!Edges.ContainsKey(id))
            {
                return null;
            }
            return Edges[id];
        }
        public Edge getEdge(Vertex v, Vertex w)
        {
            var neighborIds = AdjList[v.Id];
            var edgeID =  neighborIds[w.Id];
            return Edges[edgeID];
        }

        public Dictionary<int, int>.KeyCollection NeighborIDs(Vertex v)
        {
            return AdjList[v.Id].Keys;
        }

        public List<Vertex>  NeighborList(Vertex v)
        {
            var list = new List<Vertex>();
            foreach (int id in AdjList[v.Id].Keys)
            {
                list.Add(Vertices[id]);
            }
            return list;
        }

        public bool InsertEdge(Vertex a, Vertex b)
        {
            if (AreAdjacent(a, b)) return false;

            if (a == null || b == null) return false;

            var eId = EdgeCount + 1;
            if (ReusableEdgeId.Count > 0) eId = ReusableEdgeId.Pop();
            var e = new Edge(a.Id, b.Id, eId);
            if (!InsertEdge(e))
            {
                ReusableEdgeId.Push(eId);                
                return false;
            }
            return true;
        }

        public bool InsertEdge(Edge e)
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
                return true;
            }
            return false;
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

    public class Vertex : IComparable
    {
        public int Id;
        public double Weight;
        public int Color;
        public int degree;
        public Vertex Parent;
        public Vertex(int a)
        {
            Id = a;
        }

        int IComparable.CompareTo(object obj)
        {
            Vertex c = (Vertex)obj;

            if (this.Weight < c.Weight) return -1;
            if (this.Weight > c.Weight) return 1;

            //if weights are the same
            if (this.Id < c.Id) return -1;
            if (this.Id > c.Id) return 1;
            
            return 0;
        }

        public override string ToString()
        {
            return "" + Id ;
        }
    }
    public class Edge
    {
        public int Id;
        public int StartNodeId;
        public int EndNodeId;
        public double Weight;

        public Edge(int v1, int v2, int EdgeId)
        {
            Id = EdgeId;
            StartNodeId = v1;
            EndNodeId = v2;
        }
    }
}
