using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphShine.GeometricPrimitives;

namespace GraphShine.DataStructures
{
    public class BSTTest
    {
        public static void BinarySearchTreeTest()
        {
            BST<float> tree = new BST<float>();
            tree.Insert(5);
            tree.printTree();
            tree.Insert(7);
            tree.printTree();
            tree.Insert(9);
            tree.printTree();
            //Right Left Adjustment
            //Right Left Adjustment
            //this insertion requires two adjastment :) 2 adjastment always suffice for every insertion
            tree.Insert(8);
            tree.printTree();
            tree.Insert(6);
            tree.printTree();
            tree.Insert(2);
            tree.printTree();
            //This BST is only for unique values.
            tree.Insert(2);//duplicate insertion not allowed
            tree.printTree();
            tree.Insert(1);
            tree.printTree();
            //Left Left Adjustment
            tree.Insert(-1);
            tree.printTree();
            //Left Right Adjustment
            tree.Insert(1.5f);
            tree.printTree();
            tree.Insert(10);
            tree.printTree();
            tree.Insert(11);
            tree.printTree();
            //Right Right Adjustment
            tree.Insert(12);
            tree.printTree();

            tree.Delete(2);
            tree.printTree();
            tree.Delete(2);
            tree.printTree();
            tree.Delete(7);
            tree.printTree();
            tree.Delete(6);
            tree.printTree();
            tree.Delete(1.5f);
            tree.printTree();
            tree.Delete(11);
            tree.printTree();
            tree.Delete(8);
            tree.printTree();

            tree.Delete(5);
            tree.printTree();
            tree.Delete(-1);
            tree.printTree();
            tree.Delete(9);
            tree.printTree();
            tree.Delete(10);
            tree.printTree();
            tree.Delete(12);
            tree.printTree();
            tree.Delete(1);
            tree.printTree();
            tree.Delete(12);
            tree.printTree();
        }
    }
}
