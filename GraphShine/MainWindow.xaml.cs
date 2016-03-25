using System;
using System.Collections.Generic;
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

namespace GraphShine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public void runTests()
        {
            Tests allTests = new Tests();
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
