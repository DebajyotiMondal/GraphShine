using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GraphShine.Tests;
using iTextSharp.text.pdf;

namespace GraphShine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public void runTests()
        {
            TestFunctions allTests = new TestFunctions(Paper);
            allTests.Run();
        }
        public MainWindow()
        {
            Console.WriteLine("Welcome to GraphShine!");
            InitializeComponent();
            runTests();
            //this.Hide();                        
        }


    }
}
