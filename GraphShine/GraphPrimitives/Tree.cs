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

        public static Stack<int> ReusableNodeId;
        public static Stack<int> ReusableEdgeId;

        public Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        public Dictionary<int, Edge> Edges = new Dictionary<int, Edge>();
        public Dictionary<int, Dictionary<int, int>> AdjList = new Dictionary<int, Dictionary<int, int>>();

        public Tree() 
        {
            NodeCount = 0;
            EdgeCount = 0;

            ReusableNodeId = new Stack<int>();
            ReusableEdgeId = new Stack<int>();
        }
        
        public void InsertNode(Node v)
        {
            Nodes.Add(v.Id, v);
            NodeCount = Nodes.Count;
        }
        /// <summary>
        /// Create a node and assign a unique id for the node; returns the node.
        /// </summary>
        /// <returns></returns>
        public Node CreateNode()
        {            

            var vId = NodeCount + 1;
            if (ReusableNodeId.Count > 0) vId = ReusableNodeId.Pop();
            Node v = new Node(vId);

            Nodes.Add(v.Id, v);
            NodeCount = Nodes.Count;

            return v;
        }
        
        /// <summary>
        /// Delete the given node.
        /// </summary>
        /// <param name="v"></param>
        public void DeleteNode(Node v)
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
                if (Nodes[neighborId].Parent != null && Nodes[neighborId].Parent.Id == v.Id)
                    Nodes[neighborId].Parent = null;
                else if (v.Parent != null && v.Parent.Id == neighborId)
                    Nodes[neighborId].Kids.Remove(v.PosAsKid);
            }
            EdgeCount = Edges.Count;
            AdjList.Remove(v.Id);
            
            //delete the vertex itself
            ReusableNodeId.Push(v.Id);
            Nodes.Remove(v.Id);
            NodeCount = Nodes.Count;

        }
        /// <summary>
        /// Delete the node of given node Id.
        /// </summary>
        /// <param name="vId"></param>
        public void DeleteNode(int vId)
        {
            DeleteNode(Nodes[vId]);
        }

        /// <summary>
        /// Delete the given edge.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="kid"></param>
        /// <returns></returns>
        public bool DeleteDirectedEdge(Node parent, Node kid)
        {
            if (parent == null || kid == null) return false;

            var a = parent.Id;
            var b = kid.Id;            
            var NeighborToEdgeId = AdjList[a];

            if (NeighborToEdgeId.ContainsKey(b))
            {

                var edgeId = NeighborToEdgeId[b];

                Edges.Remove(edgeId);
                ReusableEdgeId.Push(edgeId);
                EdgeCount = Edges.Count;

                NeighborToEdgeId.Remove(b);
            }
             
            parent.Kids.Remove(kid.PosAsKid);
            kid.Parent = null;
            kid.PosAsKid = -1;

            return false;
        }
        /// <summary>
        /// Create a new edge.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="Kid"></param>
        /// <param name="kidPosition"></param>
        /// <returns></returns>
        public bool InsertDirectedEdge(Node parent, Node Kid, int kidPosition)
        {
            if (parent == null || Kid == null) return false;

            var eId = EdgeCount + 1;
            var reusing = false;

            if (ReusableEdgeId.Count > 0)
            {
                reusing = true;
                eId = ReusableEdgeId.Pop();
            }
            var e = new Edge(parent.Id,Kid.Id,eId);
            if (!InsertDirectedEdge(e, kidPosition))
            {
                if(reusing) ReusableEdgeId.Push(eId);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Insert the given edge.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="kidPosition"></param>
        /// <returns></returns>
        private bool InsertDirectedEdge(Edge e, int kidPosition)
        {
            var parentId = e.StartNodeId;
            var kidId = e.EndNodeId;
            bool alreadyExists = ContainsEdge(e);
            if (!alreadyExists)
            {
                Nodes[kidId].PosAsKid = kidPosition;

                //update edgelist                        
                Edges.Add(e.Id, e);
                EdgeCount = Edges.Count;

                int a = e.StartNodeId;
                int b = e.EndNodeId;


                //update adjacency list
                if (!AdjList.ContainsKey(a))
                {
                    Dictionary<int, int> neighborsList = new Dictionary<int, int>();
                    neighborsList.Add(b, e.Id);
                    AdjList.Add(a, neighborsList);
                }
                else AdjList[a].Add(b, e.Id);
                
                Nodes[parentId].Kids.Add(kidPosition,Nodes[kidId] );
                Nodes[kidId].Parent = Nodes[parentId];
                return true;
            }
            return false;
        }



        public bool ContainsEdge(Edge e)
        {
            int a = e.StartNodeId;
            int b = e.EndNodeId;
            
            return AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b);
        }

        public bool AreAdjacent(Node v, Node w)
        {
            int a = v.Id;
            int b = w.Id;
            return AdjList.ContainsKey(a) && AdjList[a].ContainsKey(b);
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
        public int PosAsKid;

        public Node(int a) : base(a) {  
            Parent = null;
            Kids = new Dictionary<int, Node>();
            PosAsKid = -1;
        }

        /// <summary>
        /// return the kid in the k-th position
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public Node getKid(int k)
        {
            if (Kids.ContainsKey(k)) return Kids[k];
            return null;
        }

        public int totalKids()
        {        
            return Kids.Keys.Count();
        }

        public Node leftKid()
        {        
            if (Kids.ContainsKey(0)) return Kids[0];
            return null;
        }
        public Node rightKid()
        {
            if (Kids.ContainsKey(1)) return Kids[1];
            return null;
        }

        public bool isLeaf()
        {
            return Kids.Count == 0;
        }
    }   
}
