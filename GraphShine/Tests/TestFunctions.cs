using System.Windows.Controls;

namespace GraphShine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class TestFunctions
    { 
        public Canvas Paper;

         
        public TestFunctions(Canvas Paper)
        {
            // TODO: Complete member initialization
            this.Paper = Paper;
        }

        //Graph Drawing TestFunctions
        public void SchnyderRealizerTest() { }

        public void MinimumSchnyderRealizerTest() { }

        public void StraightLineDrawingTest() { }

        public void ConvexDrawingTest() { }

        public void PolylineDrawingTest() { }

        //Graph Algorithms Test       
        public void DfsTest() { }

        public void GraphSeparatorTest() { }

        public void ShortestPathTest() { }

        public void MinimumCostNetworkFlowTest() { }

        //Data Structures Test

        public void PlanarityTest() { }

        public void PQRTreeTest() { }

        public void PriorityQueueTest() { }


        public void Run()
        {
            //GraphReaderTest.GraphReadingTest();
            //NearestNeighborTest.L1NearestNeighborInConeTest(); 
            //SegmentTreeTest.SegmentTreeDataStructureTest();
            //ArraySearchTest.BinarySearchTest();
            //VertrexOrderingTest.BfsTest();
            //ConnectedComponentsTest.getAllComponentsTest();
            //BSTTest.BinarySearchTreeTest();
            //MinimumSpanningTreeTest.MSTtest();
            //GraphReaderTest.GeometricGraphReadingTest();
            //PlanarGraphTest.PlanarGraphFunctionsTest(); //incomplete
            //DrawingTest.DrawingStraightLineTest(Paper);
            VertrexOrderingTest.CanonicalOrderingTest();
        }
    }
}
