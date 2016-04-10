using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GraphPrimitives;
using GraphShine.Utilities;

namespace GraphShine.GraphPrimitives
{
    class PlanarGraph: Graph
    {
        internal Dictionary<Vertex, Dictionary<Vertex, KeyValuePair<Vertex, Vertex>>> PlanarAdjList;

        private Dictionary<Vertex, int> CanonicalOrder; 

        public PlanarGraph() : base()
        {            
            PlanarAdjList = new Dictionary<Vertex, Dictionary<Vertex, KeyValuePair<Vertex, Vertex>>>();
        }

        public PlanarGraph(Graph2D G): base()
        {
            Graph.CopyAttributes(G,this);
            PlanarAdjList = RotationSystem.getRotationSystem(G);
        }


        public Dictionary<Vertex, KeyValuePair<Vertex,Vertex>>.KeyCollection NeighborOrderedList(Vertex v)
        {
            var list = new List<Vertex>();
            return PlanarAdjList[v].Keys;            
        }

        /*
         * input: a planar graph and an edge a<---b
         * output: c, the neighbor next to b around a in anticlockwise order
         */
        
        public Vertex getAntiClockNeighbor(Vertex current, Vertex neighbor)
        {
            var list = PlanarAdjList[current];
            return list[neighbor].Value;
        }


        public Vertex getClockNeighbor(Vertex current, Vertex neighbor)
        {
            var list = PlanarAdjList[current];
            return list[neighbor].Key;
        }


        /*
         *input: a planar graph and an edge
         *output: a face that is incident to the right of this edge
         */
        /*
        public static List<Vertex> GetRightIncidentFace(Tiling gPlanar, Vertex givenTailVertex, Vertex givenHeadVertex, out bool degenerate)
        {
            degenerate = false;
            List<Vertex> face = new List<Vertex>();
            Vertex currentHead = givenHeadVertex, currentTail = givenTailVertex;

            do
            {
                int subsequentVertxId = GetAnticlockwiseNextNeighbor(gPlanar, currentHead.Id, currentTail.Id);
                currentTail = currentHead;
                currentHead = gPlanar.VList[subsequentVertxId];
                if (face.Contains(currentHead))
                {
                    degenerate = true;
                    break;
                }
                face.Add(currentHead);
            } while (!(givenTailVertex.Id == currentTail.Id && givenHeadVertex.Id == currentHead.Id));
            return face;
        }        
        */




        public void InsertVertex(Vertex v)
        {
            base.InsertVertex(v);
            PlanarAdjList.Add(v, new Dictionary<Vertex, KeyValuePair<Vertex,Vertex>>());
        }

        public void DeleteVertex(Vertex v)
        {
            base.DeleteVertex(v);
            //delete associated edges            
            Console.WriteLine("Not Supported Yet");
            //then remove v
            PlanarAdjList.Remove(v);
        }

        public void DeleteEdge(Vertex v, Vertex w)
        {
            base.DeleteEdge(v,w);
            updateRotationSystem(v, w);
        }

        public void updateRotationSystem(Vertex v, Vertex w)
        {
            Console.WriteLine("Need To be tested");
            var listv = PlanarAdjList[v];

            if (listv.Keys.Count == 1)
            {
                listv.Remove(w);
                return;
            }

            var wPrev = listv[w].Key;
            var wNext = listv[w].Value;


            var wPrevPrev = listv[wPrev].Key;
            if (wPrevPrev.Id == w.Id) wPrevPrev = wPrev;
            listv[wPrev] = new KeyValuePair<Vertex, Vertex>(wPrevPrev, wNext);

            var wNextNext = listv[wNext].Value;
            if (wNextNext.Id == w.Id) wNextNext = wNext;
            listv[wNext] = new KeyValuePair<Vertex, Vertex>(wPrev, wNextNext);

            listv.Remove(w);
        }

        public bool InsertEdge(Vertex a, Vertex b)
        {
            // posInRotationSystem should come as an input
            Console.WriteLine("Not Supported Yet");
            return true;
        }

        public bool InsertEdge(Edge e)
        {
            // posInRotationSystem should come as an input
            Console.WriteLine("Not Supported Yet");
            return true;
        }

    }
}
