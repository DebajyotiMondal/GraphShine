using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GraphShine.DataStructures;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    class MinimumSpanningTree
    {
        
        public static Dictionary<Tree,double>  MSTPrim(Graph G)
        {
            Dictionary<Tree, double> treeList = new Dictionary<Tree, double>();
            
            //get connected components
            List<Graph> componentList = ConnectedComponents.getAllComponents(G);
            foreach (var component in componentList)
            {
                getPrimTree(component,treeList);
            } 
             
            return treeList;
        }

        static void getPrimTree(Graph G, Dictionary<Tree,double> treeList)
        {
            if (G.VertexCount == 0)
            {
                Console.WriteLine("Warning: G is Empty");
                return;
            }

            Tree T = new Tree();
            //initialize graph nodes

            
            BST<Vertex> bst = new BST<Vertex>();

            var initialNode = G.GetInitialVertex();
            initialNode.Color = 1;
            initialNode.Weight = 0;
            bst.Insert(initialNode);

            foreach (Vertex v in G.VertexList())
            {                               
                if(v.Id == initialNode.Id) continue;
                v.Color = 0;
                v.Weight = double.MaxValue;
                bst.Insert(v);
                
            }

            //bst.printTree();

            while (!bst.IsEmpty())
            {
                Vertex v = bst.ExtractMin();
                //bst.printTree();
                foreach (Vertex w in G.NeighborList(v))
                {                    
                    if (w.Color == 1) continue;

                    w.Color = 1;
                    Edge e = G.getEdge(v, w);

                    if (w.Weight >= e.Weight)
                    {
                        bst.Delete(w);                        
                        //bst.printTree();
                        w.Weight = e.Weight;
                        w.Parent = v;
                        bst.Insert(w);
                        //bst.printTree();
                    }

                }
            }

            var root = new Node(initialNode.Id);
            T.InsertNode(root);
            T.RootNode = root;

            double cost = 0;

            foreach (Vertex v in G.VertexList())
            {           
                if (v.Parent != null)
                {
                    var w = new Node(v.Id);
                    T.InsertNode(w);
                    T.InsertDirectedEdge(v.Parent.Id, w.Id);
                    cost += G.getEdge(v, v.Parent).Weight;
                }
            }
        
            treeList[T] = cost;
        }
    }
}
