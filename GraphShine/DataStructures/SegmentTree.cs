using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GeometricPrimitives;
using GraphShine.GraphPrimitives;
using GraphShine.Utilities;

namespace GraphShine.DataStructures
{
    /*
     * SegmentTreeTest.SegmentTreeDataStructureTest();
    */

    public class SegmentTree
    {
        static Dictionary<int, List<HInterval>> IdToIntervals = new Dictionary<int, List<HInterval>>();
        public static Tree tree;
        public static void Build(HInterval[] horizontalHIntervals)
        {
            tree = new Tree(0, 0);
            //collect all the endpoints
            int index = 0;
            double [] endpoints = new double[2*horizontalHIntervals.Length];
            foreach (HInterval interval in horizontalHIntervals)
            {
                endpoints[index] = interval.A.x; index++;
                endpoints[index] = interval.B.x; index++;
            }

            ConstructBalancedBinaryTreeOnElementaryIntervals(endpoints);

            //sort the intervals according to y coordinate
            HInterval[] tempHIntervals = new HInterval[horizontalHIntervals.Length];
            double[] Y = new double[horizontalHIntervals.Length];
            for (int i = 0; i < horizontalHIntervals.Length; i++)
            {
                tempHIntervals[i] = new HInterval(horizontalHIntervals[i]);
                Y[i] = tempHIntervals[i].A.y;
            }
            Array.Sort(Y,tempHIntervals);

            //for each BaseHInterval insert it into tree nodes.            
            foreach (var interval in tempHIntervals)            
                InsertInterval(tree.RootNode, interval);                         

            
            //for each node copy the horizontalHIntervals 
            foreach (var nodeId in IdToIntervals.Keys)
            {
                int totalSemgnets = IdToIntervals[nodeId].Count;
                SegmentNode w = (SegmentNode) tree.Nodes[nodeId];
                //double[] Y = new double[totalSemgnets];
                w.ListOfHIntervals = new HInterval[totalSemgnets];
                index = 0;
                foreach (HInterval interval in IdToIntervals[nodeId])
                {
                    w.ListOfHIntervals[index] = interval;
                    //Y[index] = HInterval.A.y;
                    index++;
                }
                //Array.Sort(Y,w.ListOfHIntervals);
            }
                     
        }

        private static void InsertInterval(Node currentNode, HInterval givenHInterval)
        {
            SegmentNode w = (SegmentNode) currentNode;
            //baseinterval contained in given HInterval
            if (givenHInterval.Contains(w.BaseHInterval))
            {
                if (!IdToIntervals.ContainsKey(w.Id)) IdToIntervals[w.Id] = new List<HInterval>();
                IdToIntervals[w.Id].Add(givenHInterval);                
                return;
            }
            SegmentNode leftkid = (SegmentNode) w.leftKid();
            if (leftkid!= null && givenHInterval.Intersects(leftkid.BaseHInterval))
            {
                InsertInterval(leftkid, givenHInterval);               
            }
            SegmentNode rightkid = (SegmentNode)w.rightKid();
            if (rightkid != null && givenHInterval.Intersects(rightkid.BaseHInterval))
            {
                InsertInterval(rightkid, givenHInterval);
            }
        }
        private static void ConstructBalancedBinaryTreeOnElementaryIntervals(double[] endpoints)
        {
            int index = 0;

            Array.Sort(endpoints);
             
            //build a tree on the elementary horizontalHIntervals
            HInterval[] elementaryHIntervals = new HInterval[2*endpoints.Length];
            int numberOfElementaryIntervals = 0;
            //build the leaves
            for (int i = 0; i < endpoints.Length;i++)
            {
                var leftend = endpoints[i];
                var rightend = endpoints[i];
                while (i + 1 < endpoints.Length && endpoints[i] == endpoints[i + 1])
                {
                    i++;
                    rightend = endpoints[i];
                }
                if(i + 1 < endpoints.Length) rightend = endpoints[i+1];

                //elementary closed BaseHInterval
                HInterval hInterval = new HInterval(new Point(leftend, 0), new Point(leftend, 0));
                elementaryHIntervals[numberOfElementaryIntervals++] = hInterval;
                //elementary open BaseHInterval
                if (leftend != rightend)
                {
                    hInterval = new HInterval(new Point(leftend + Fields.Epsilon, 0),
                        new Point(rightend - Fields.Epsilon, 0));
                    elementaryHIntervals[numberOfElementaryIntervals++] = hInterval;
                }
            }
            //build the internal nodes
            double level = Math.Ceiling(Math.Max(0,Math.Log(numberOfElementaryIntervals, 2)));
            int NodeUpperBound = (int) Math.Pow(2, level + 1) - 1;
            //add nodes
            for (int i = 0; i < NodeUpperBound; i++) tree.InsertNode(new SegmentNode(i, new HInterval()));
            tree.RootNode = tree.Nodes[0];
            //add edges
            int eId = 0;
            for (int i = 0; i < NodeUpperBound; i++)
            {
                int kid1Id = -1 + 2*(i + 1);
                int kid2Id = -1 + 2*(i + 1) + 1;
                if (kid1Id < NodeUpperBound)
                    tree.InsertDirectedEdge(new Edge(i, kid1Id, eId++), i, kid1Id, 0);
                if (kid2Id < NodeUpperBound)
                    tree.InsertDirectedEdge(new Edge(i, kid2Id, eId++), i, kid2Id, 1);
            }
            //assignment of leaves to elementary horizontalHIntervals
            int k = 0;
            for (int i = 0; i < NodeUpperBound; i++)
            {
                if (tree.Nodes[i].Kids.Count == 0)
                {
                    SegmentNode w = (SegmentNode) tree.Nodes[i];
                    if (k < numberOfElementaryIntervals)
                        w.BaseHInterval = elementaryHIntervals[k++];
                    else
                        w.BaseHInterval = new HInterval(new Point(Double.MaxValue, 0), new Point(Double.MaxValue, 0));
                }
            }
            //postorder traversal and update internal nodes
            UpdateInternalNodes((SegmentNode)tree.RootNode);
        }

        static void UpdateInternalNodes(SegmentNode w)
        {
            if (w.isLeaf()) return;
            SegmentNode leftkid = (SegmentNode)w.leftKid();
            if (leftkid != null) UpdateInternalNodes(leftkid);
            SegmentNode rightkid = (SegmentNode)w.rightKid();
            if (rightkid != null) UpdateInternalNodes(rightkid);
            w.BaseHInterval = new HInterval(leftkid.BaseHInterval.A, rightkid.BaseHInterval.B);
        }

        public static List<HInterval> GetAllIntersectingIntervals(Point givenPoint)
        {
            List <HInterval> intersetList = new List<HInterval>();
            allIntersecting((SegmentNode)tree.RootNode, givenPoint, intersetList);
            return intersetList;
        }

        static void allIntersecting(SegmentNode w, Point givenPoint, List<HInterval> intersetList)
        {
            if (w.ListOfHIntervals != null)
            {
                foreach (var interval in w.ListOfHIntervals)
                    intersetList.Add(interval);
            }
            SegmentNode leftkid = (SegmentNode) w.leftKid();
            if(leftkid!= null && leftkid.BaseHInterval.Contains(givenPoint))
                allIntersecting(leftkid, givenPoint, intersetList);
            SegmentNode rightkid = (SegmentNode)w.rightKid();
            if (rightkid != null && rightkid.BaseHInterval.Contains(givenPoint))
                allIntersecting(rightkid, givenPoint, intersetList);
        }
        
        public static HInterval GetImmediatelyAbove(Point givenPoint)
        {
            HInterval intersectedSegment = new HInterval(new Point(0, double.MaxValue), new Point(0, double.MaxValue));
            GetImmediateIntersectingSegment((SegmentNode)tree.RootNode, givenPoint, ref intersectedSegment);
            return intersectedSegment;
        }

        static void GetImmediateIntersectingSegment(SegmentNode w, Point givenPoint, ref HInterval intersectedSegment)
        {
            if (w.ListOfHIntervals != null)
            {
                //binary search to find the interval that is immediately above
                int low = 0;
                int high = w.ListOfHIntervals.Length - 1;
                
                while (low <= high)
                {
                    int mid = (low + high) / 2;
                    if (w.ListOfHIntervals[mid].A.y == givenPoint.y)
                    {
                        intersectedSegment = w.ListOfHIntervals[mid];
                        break;
                    }
                    if (w.ListOfHIntervals[mid].A.y < givenPoint.y)
                        low = mid + 1;
                    else
                    {
                        if(intersectedSegment.A.y > w.ListOfHIntervals[mid].A.y )
                            intersectedSegment = w.ListOfHIntervals[mid];
                        high = mid - 1;
                        if (high < 0 || w.ListOfHIntervals[high].A.y < givenPoint.y) break;
                    }                    
                }
            }
            SegmentNode leftkid = (SegmentNode) w.leftKid();
            if(leftkid!= null && leftkid.BaseHInterval.Contains(givenPoint))
                GetImmediateIntersectingSegment(leftkid, givenPoint, ref intersectedSegment);
            SegmentNode rightkid = (SegmentNode)w.rightKid();
            if (rightkid != null && rightkid.BaseHInterval.Contains(givenPoint))
                GetImmediateIntersectingSegment(rightkid, givenPoint, ref intersectedSegment);
        }
    
    }



    class SegmentNode : Node
    {
        public HInterval BaseHInterval;
        public HInterval[] ListOfHIntervals;
        public SegmentNode(int a, HInterval h ):base(a)
        {
            BaseHInterval = h;
        }
    }
}
