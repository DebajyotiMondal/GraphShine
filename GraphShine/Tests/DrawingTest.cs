using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GraphShine.GraphPrimitives;
using GraphShine.IO;
using GraphShine.GeometricPrimitives;
using GraphShine.Viewer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Path = System.Windows.Shapes.Path;

namespace GraphShine.Tests
{
    class DrawingTest
    {
        public static void DrawingStraightLineTest(Canvas MyCanvas)
        {
            String filename = "../../data/testgeometricgraph.txt";
            Graph2D graph = GraphReader.ReadGeometricGraph(filename);


            GraphViewer.InsertGraph2D(MyCanvas, graph);
            GraphViewer.UpdateViewer(MyCanvas);

            GraphViewer.SavePdf(MyCanvas);

        }


    
    }
}
