using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphShine.Utilities
{
    public class ArraySearch
    {
        /// <summary>
        /// Searches query value inside an array and if found, then the indexFound is 
        /// the index in the Array that holds the query value. Returns true if found, false otherwise.
        /// </summary>
        /// <param name="querypoint"></param>
        /// <param name="Array"></param>
        /// <param name="indexFound"></param>
        /// <returns></returns>
        public static bool BinarySearch(double querypoint, double[] Array, out int indexFound)
        {
            indexFound = -1;
            int low = 0;
            int high = Array.Length - 1;

            while (low <= high)
            {
                int mid = (low + high)/2;
                if (Array[mid] == querypoint)
                {
                    indexFound = mid;
                    return true;
                }
                if (Array[mid] < querypoint)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return false;
        }

        /// <summary>
        /// Searches query value inside an array and if found, then the leftIndex and rightIndex both contain 
        /// the Array Index that holds the query value. If not found then the leftIndex and rightIndex forms
        /// the smallest interval containing the query value. Returns true if found, false otherwise.
        /// If the query value is smaller (larger) than all then leftIndex (rightIndex) is -1.
        /// </summary>
        /// <param name="querypoint"></param>
        /// <param name="Array"></param>
        /// <param name="leftIndex"></param>
        /// <param name="rightIndex"></param>
        /// <returns></returns>
        public static bool BinarySearchClosest(double querypoint, double[] Array, out int leftIndex,
            out int rightIndex)
        {
            leftIndex = rightIndex = -1;
            int low = 0;
            int high = Array.Length - 1;

            while (low <= high)
            {
                int mid = (low + high)/2;
                if (Array[mid] == querypoint)
                {
                    leftIndex = rightIndex = mid;
                    return true;
                }
                if (Array[mid] < querypoint)
                {
                    low = mid + 1;
                    leftIndex = mid;
                }
                else
                {
                    high = mid - 1;
                    rightIndex = mid;
                }
            }
            return false;
        }
    }
}
