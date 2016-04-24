using System;
using System.Collections.Generic;
using GraphShine.DataStructures;
using GraphShine.GeometricPrimitives;

namespace GraphShine.Tests
{
    public class SegmentTreeTest
    {
        public static void SegmentTreeDataStructureTest()
        {
            HInterval[] h = new HInterval[9];
            h[0] = new HInterval(new Point2D(19, 3), new Point2D(24, 3)) { Id = 0};
            h[1] = new HInterval(new Point2D(0, 3), new Point2D(4, 3)) { Id = 1 };
            h[2] = new HInterval(new Point2D(2, 1), new Point2D(6, 1)) { Id = 2 };
            h[3] = new HInterval(new Point2D(4, 2), new Point2D(9, 2)) { Id = 3 };
            h[4] = new HInterval(new Point2D(6, 0), new Point2D(14, 0)) { Id = 4 };
            h[5] = new HInterval(new Point2D(9, 3), new Point2D(16, 3)) { Id = 5 };
            h[6] = new HInterval(new Point2D(13, 2), new Point2D(19, 2)) { Id = 6 };
            h[7] = new HInterval(new Point2D(16, 1), new Point2D(19, 1)) { Id = 7 };
            h[8] = new HInterval(new Point2D(16, 0), new Point2D(24, 0)) { Id = 8 }; 

            Console.WriteLine("Buiding Segment Tree");
            SegmentTree.Build(h);
            Console.WriteLine("Construction Completed");

            Console.WriteLine("All segments intersection (17,0):");
            List<HInterval> intervals = SegmentTree.GetAllIntersectingIntervals(new Point2D(17, 0));
            foreach (var interval in intervals) Console.Write(interval.Id+" ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (27,0):");
            intervals = SegmentTree.GetAllIntersectingIntervals(new Point2D(27, 0));
            foreach (var interval in intervals) Console.Write(interval.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (10,0):");
            intervals = SegmentTree.GetAllIntersectingIntervals(new Point2D(10, 0));
            foreach (var interval in intervals) Console.Write(interval.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (1,0):");
            intervals = SegmentTree.GetAllIntersectingIntervals(new Point2D(1, 0));
            foreach (var interval in intervals) Console.Write(interval.Id + " ");
            Console.WriteLine();

            /////////////////////////////////////////////////////////////////////
            Console.WriteLine("All segments intersection (17,0):");
            HInterval intv = SegmentTree.GetImmediatelyAbove(new Point2D(17, 0));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (27,0):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(27, 0));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (10,0):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(10, 0));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (1,0):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(1, 0));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (4,1.5):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(4, 1.5));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (17,2.5):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(17, 2.5));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (16,0.5):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(16, 0.5));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();

            Console.WriteLine("All segments intersection (8,3):");
            intv = SegmentTree.GetImmediatelyAbove(new Point2D(8, 3));
            if (intv.Id > 0) Console.Write(intv.Id + " ");
            Console.WriteLine();
        }
    }
}
