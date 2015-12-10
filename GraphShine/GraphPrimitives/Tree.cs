using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphShine.GraphPrimitives
{

    
    public class Tree
    {
        public Node RootNode;

        public int NodeCount;
        public int EdgeCount;
        public Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        public Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
        public Dictionary<int, List<int>> AdjList = new Dictionary<int, List<int>>();

        public Tree(int n, int m)
        {
            NodeCount = n;
            EdgeCount = m;
        }

        public void InsertNode(Node v)
        {
            Nodes.Add(v.Id, v);
            NodeCount = Nodes.Count;
        }


        public int InsertEdge(Edge e)
        {
            bool alreadyExists = ContainsEdge(e);
            if (!alreadyExists)
            {
                //update edgelist                        
                Edges.Add(e.Id, e);
                EdgeCount = Edges.Count;

                int a = e.StartNodeId;
                int b = e.EndNodeId;

                //update adjacency list
                if (!AdjList.ContainsKey(a))
                {
                    List<int> neighborsList = new List<int>();
                    neighborsList.Add(b);
                    AdjList.Add(a, neighborsList);
                }
                else AdjList[a].Add(b);
                if (!AdjList.ContainsKey(b))
                {
                    List<int> neighborsList = new List<int>();
                    neighborsList.Add(a);
                    AdjList.Add(b, neighborsList);
                }
                else AdjList[b].Add(a);
                 
                Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].Contains(b) ^
                AdjList.ContainsKey(b) && AdjList[b].Contains(a)));
            }
            return EdgeCount;
        }


        public int InsertDirectedEdge(Edge e, int parentId, int kidId, int kidPosition)
        {
            bool alreadyExists = ContainsEdge(e);
            if (!alreadyExists)
            {
                //update edgelist                        
                Edges.Add(e.Id, e);
                EdgeCount = Edges.Count;

                int a = e.StartNodeId;
                int b = e.EndNodeId;

                //update adjacency list
                if (!AdjList.ContainsKey(a))
                {
                    List<int> neighborsList = new List<int>();
                    neighborsList.Add(b);
                    AdjList.Add(a, neighborsList);
                }
                else AdjList[a].Add(b);
                if (!AdjList.ContainsKey(b))
                {
                    List<int> neighborsList = new List<int>();
                    neighborsList.Add(a);
                    AdjList.Add(b, neighborsList);
                }
                else AdjList[b].Add(a);

                Nodes[parentId].Kids.Add(kidPosition,Nodes[kidId] );
                Nodes[kidId].Parent = Nodes[parentId];

                Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].Contains(b) ^
                AdjList.ContainsKey(b) && AdjList[b].Contains(a)));
            }
            return EdgeCount;
        }



        public bool ContainsEdge(Edge e)
        {
            int a = e.StartNodeId;
            int b = e.EndNodeId;
            Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].Contains(b) ^
                AdjList.ContainsKey(b) && AdjList[b].Contains(a)));
            return AdjList.ContainsKey(a) && AdjList[a].Contains(b);
        }

        public bool AreAdjacent(Node v, Node w)
        {
            int a = v.Id;
            int b = w.Id;
            Debug.Assert(!(AdjList.ContainsKey(a) && AdjList[a].Contains(b) ^
                AdjList.ContainsKey(b) && AdjList[b].Contains(a)));
            return AdjList.ContainsKey(a) && AdjList[a].Contains(b);
        }

        public void isConsistent()
        {
            Debug.Assert(NodeCount == Nodes.Count);
            Debug.Assert(EdgeCount == Edges.Count);
        }


    }

    public class Node: Vertex
    {        
        public Node Parent;
        public Dictionary<int,Node> Kids;

        public Node(int a) : base(a) {  
            Parent = null;
            Kids = new Dictionary<int, Node>();
        }
        public Node leftKid()
        {
            if (Kids.Count == 0) return null;
            return Kids[0];
        }
        public Node rightKid()
        {
            if (Kids.Count == 0) return null;
            return Kids[Kids.Count - 1];
        }

        public bool isLeaf()
        {
            return Kids.Count == 0;
        }
    }   
}
