using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.DataStructures;
using GraphShine.GraphPrimitives;
using Org.BouncyCastle.Crypto;

namespace GraphShine.GraphAlgorithms
{
    public class SchnyderWood
    {

        //      0
        //     1 2
        public static Tree[] BuildSchnyderTrees(PlanarGraph g, Vertex root_l, Vertex root_m, Vertex root_r)
        {
            Tree[] treelist = new Tree[3];
            double priority = double.MaxValue;
            //linked list of candidate vertices
            Dictionary<Vertex, Vertex> outerverticesLinkedRight = new Dictionary<Vertex, Vertex>();
            Dictionary<Vertex, Vertex> outerverticesLinkedLeft = new Dictionary<Vertex, Vertex>();
            //for each vertex initialize the number of chords it is incident to
            Dictionary<Vertex, int> canonicalLael = new Dictionary<Vertex, int>();            
            //for each vertex initialize the number of chords it is incident to
            Dictionary<Vertex,int> chordCount = new Dictionary<Vertex, int>();
            //list of vertices with no incident chord
            BST<Vertex> candidateVertices = new BST<Vertex>();
            //vertices in canonical order
            List<Vertex> canonicalOrder = new List<Vertex>();
                
            InitializeChrodCountAndOuterChain(g, root_l, root_m, root_r, canonicalLael, chordCount, outerverticesLinkedRight, outerverticesLinkedLeft);

            
            var currentLebel = g.VertexCount;            
            candidateVertices.Insert(root_m);

            while (outerverticesLinkedRight.Count > 2)
            {                
                //choose the leftmost candidate vertex
                var w = candidateVertices.ExtractMin();
                canonicalLael.Add(w,currentLebel);
                currentLebel--;
                canonicalOrder.Add(w);

                //scan the neighbors of w from currentleft
                var currentLeft = outerverticesLinkedLeft[w];
                var currentRight = outerverticesLinkedRight[w];

                var neighborsCCW = g.NeighborListOrderedCCW(w,currentLeft);
                var newvertices = neighborsCCW.ToArray();
                //new vertices on the outer face         
                //max index corresponds to the last vertex on the outer chain
                int max = newvertices.Length-1;
                for(int i  = 0 ; i < newvertices.Length -1; i++)
                {
                    var a = newvertices[i];
                    var b = newvertices[i + 1];

                    if (canonicalLael.ContainsKey(b))
                    {
                        max = i;
                        break;
                    }
                    //otherwise it is a in Gk, and put them in the outer chain
                    if (outerverticesLinkedRight.ContainsKey(a))
                    {
                        outerverticesLinkedRight[a] = b;                        
                    }
                    else outerverticesLinkedRight.Add(a,b);
                    
                    if (outerverticesLinkedLeft.ContainsKey(b))
                    {
                        outerverticesLinkedLeft[b] = a;
                    }
                    else outerverticesLinkedLeft.Add(b, a);
                      
                }

                //remove w from the outer chain
                outerverticesLinkedLeft.Remove(w);
                outerverticesLinkedRight.Remove(w);


                //get all children in Tm and color the edges
                HashSet<Vertex> mChildren = new HashSet<Vertex>();
                Dictionary<int, Char> edgeColor = new Dictionary<int, Char>();
                AssignEdgeColor(g, max, newvertices, mChildren, w, edgeColor);
                priority = UpdateChordCountAndCandidateList(g, max, newvertices, chordCount, priority, candidateVertices, canonicalLael, outerverticesLinkedLeft, outerverticesLinkedRight, mChildren);

                              

                //PrintOuterChainAndCandidateVertices(root_l, outerverticesLinkedRight, allGoodtoPick);
            
            }
            canonicalOrder.Add(root_r);
            canonicalOrder.Add(root_l);  
            canonicalLael[root_l] = 1;
            canonicalLael[root_r] = 2;
            PrintCanonicalOrder(canonicalOrder);
            
            return treelist;
        }

        private static void PrintCanonicalOrder(List<Vertex> canonicalOrder)
        {
            Console.WriteLine("Canonical Order: ");
            foreach (var q in canonicalOrder)
            {
                Console.Write(" " + q);
            }
            Console.WriteLine();
        }

        private static void InitializeChrodCountAndOuterChain(PlanarGraph g, Vertex root_l, Vertex root_m, Vertex root_r,
            Dictionary<Vertex, int> canonicalLael, Dictionary<Vertex, int> chordCount, Dictionary<Vertex, Vertex> outerverticesLinkedRight,
            Dictionary<Vertex, Vertex> outerverticesLinkedLeft)
        {
            
            foreach (var w in g.VertexList())
            {
                chordCount.Add(w, 0);
                if (w.Id == root_l.Id) chordCount[w] = int.MaxValue;
                if (w.Id == root_r.Id) chordCount[w] = int.MaxValue;
            }

            outerverticesLinkedRight.Add(root_l, root_m);
            outerverticesLinkedRight.Add(root_m, root_r);
            outerverticesLinkedRight.Add(root_r, null);

            outerverticesLinkedLeft.Add(root_l, null);
            outerverticesLinkedLeft.Add(root_m, root_l);
            outerverticesLinkedLeft.Add(root_r, root_m);
        }

        private static void PrintOuterChainAndCandidateVertices(Vertex root_l, Dictionary<Vertex, Vertex> outerverticesLinkedRight,
            BST<Vertex> allGoodtoPick)
        {
            //print current outer chain                          
            Console.Write("Current Outer Face:");
            var current = root_l;
            while (current != null)
            {
                Console.Write(" " + current);
                current = outerverticesLinkedRight[current];
            }
            Console.WriteLine();

            //chord count
            Console.Write("Candidates:");
            allGoodtoPick.printTree();
            Console.WriteLine();
        }

        private static double UpdateChordCountAndCandidateList(PlanarGraph g, int max, Vertex[] newvertices,
            Dictionary<Vertex, int> chordCount, double priority, BST<Vertex> allGoodtoPick, Dictionary<Vertex, int> canonicalLael,
            Dictionary<Vertex, Vertex> outerverticesLinkedLeft, Dictionary<Vertex, Vertex> outerverticesLinkedRight, HashSet<Vertex> mChildren)
        {
            //update the chord count
            if (max == 1)
            {
                var a = newvertices[0];
                var b = newvertices[1];

                if (chordCount[b] == 1)
                {
                    b.Weight = --priority;
                    allGoodtoPick.Insert(b);
                }
                if (chordCount[a] == 1)
                {
                    a.Weight = --priority;
                    allGoodtoPick.Insert(a);
                }

                chordCount[a] = chordCount[a] - 1;
                chordCount[b] = chordCount[b] - 1;

                //Console.WriteLine("Decreasing ChordCount of " + a +" and " + b);
            }

            //for all of them check whether there is any chord
            for (int i = 1; i < max; i++)
            {
                var p = newvertices[i];
                foreach (var q in g.NeighborList(p))
                {
                    if (canonicalLael.ContainsKey(q)) continue;
                    if (outerverticesLinkedLeft[p].Id == q.Id) continue;
                    if (outerverticesLinkedRight[p].Id == q.Id) continue;
                    //else it may be a chord; 
                    if (outerverticesLinkedLeft.ContainsKey(q))
                    {
                        //do not update in both direction
                        if (mChildren.Contains(q) && p.Id > q.Id) continue;

                        chordCount[p] = chordCount[p] + 1;
                        chordCount[q] = chordCount[q] + 1;

                        allGoodtoPick.Delete(p);
                        allGoodtoPick.Delete(q);

                        //Console.WriteLine("Increasing ChordCount of " + p + " and " + q);
                    }
                }
            }
            //insert the candidate outer vertices into BST with 
            for (int i = max - 1; i > 0; i--)
            {
                var a = newvertices[i];
                if (chordCount[a] == 0)
                {
                    a.Weight = --priority;
                    allGoodtoPick.Insert(a);
                }
            }
            return priority;
        }

        private static void AssignEdgeColor(PlanarGraph g, int max, Vertex[] newvertices, HashSet<Vertex> mChildren, Vertex w,
            Dictionary<int, char> edgeColor)
        {
            for (int i = 1; i < max; i++)
            {
                var mChild = newvertices[i];
                mChildren.Add(mChild);
                var e = g.getEdge(w, mChild);
                edgeColor.Add(e.Id, 'm');
            }
            var lChild = newvertices[0];
            var rChild = newvertices[max];
            var e_l = g.getEdge(w, lChild);
            edgeColor.Add(e_l.Id, 'l');
            var e_r = g.getEdge(w, rChild);
            edgeColor.Add(e_r.Id, 'r');
        }
    }
}
