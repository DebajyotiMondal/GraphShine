using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;

namespace GraphShine.GraphAlgorithms
{
    class MinimumSpanningTree
    {
        public List<Tree> MSTPrim(Graph G)
        {
            List<Tree> treeList = new List<Tree>();
            
            //get connected components
            /*List<Graph> componentList = ConnectedComponents(G);
            foreach (var component in componentList)
            {
                treeList.Add(getPrimTree());
            } 
             * */
            return treeList;
        }
    }
}
