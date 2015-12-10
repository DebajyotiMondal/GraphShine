using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphShine.Utilities
{
    /*
     * ArraySearchTest.BinarySearchTest();
    */  
    public class ArraySearchTest
    {
        public static void BinarySearchTest()
        {
            double[] A = {.251, .5468, .84245, .84245, .84245, 1.2458, 1.9475, 2.564, 7.1254, 9.1542};            
            int indexFound,l,r;

            

            if(ArraySearch.BinarySearch(.251, A, out indexFound)) Console.WriteLine(".251 Exists at "+ indexFound);
            if (ArraySearch.BinarySearch(.84245, A, out indexFound)) Console.WriteLine(".84245 Exists at " + indexFound);
            if (ArraySearch.BinarySearch(2.564, A, out indexFound)) Console.WriteLine("2.564 Exists at " + indexFound);
            if (ArraySearch.BinarySearch(9.1542, A, out indexFound)) Console.WriteLine("9.1542 Exists at " + indexFound);

            if (ArraySearch.BinarySearch(1.2, A, out indexFound)) Console.WriteLine("1.2 Exists at " + indexFound);
            if (ArraySearch.BinarySearch(2.8, A, out indexFound)) Console.WriteLine("2.8 Exists at " + indexFound);
            if (ArraySearch.BinarySearch(5.6, A, out indexFound)) Console.WriteLine("5.6 Exists at " + indexFound);
            if (ArraySearch.BinarySearch(0.1, A, out indexFound)) Console.WriteLine("0.1 Exists at " + indexFound);


            if (ArraySearch.BinarySearchClosest(.251, A, out l,out r)) Console.WriteLine(".251 Exists at " + l+" "+r);
            if (ArraySearch.BinarySearchClosest(.84245, A, out l, out r)) Console.WriteLine(".84245 Exists at " + l + " " + r);
            if (ArraySearch.BinarySearchClosest(2.564, A, out l, out r)) Console.WriteLine("2.564 Exists at " + l + " " + r);
            if (ArraySearch.BinarySearchClosest(9.1542, A, out l, out r)) Console.WriteLine("9.1542 Exists at " + l + " " + r);

            ArraySearch.BinarySearchClosest(1.2, A, out l, out r); Console.WriteLine("1.2 Exists at " + l + " " + r);
            ArraySearch.BinarySearchClosest(2.8, A, out l, out r); Console.WriteLine("2.8 Exists at " + l + " " + r);
            ArraySearch.BinarySearchClosest(5.6, A, out l, out r); Console.WriteLine("5.6 Exists at " + l + " " + r);
            ArraySearch.BinarySearchClosest(0.1, A, out l, out r); Console.WriteLine("0.1 Exists at " + l + " " + r);
            ArraySearch.BinarySearchClosest(10, A, out l, out r); Console.WriteLine("10 Exists at " + l + " " + r);

        }
    }
}
