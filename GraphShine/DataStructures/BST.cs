// AuthorName = Debajyoti Mondal
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GraphShine.GeometricPrimitives;
using GraphShine.GraphPrimitives;


namespace GraphShine.DataStructures
{
    /// <summary>
    /// Maintains a set of unique values
    /// </summary>
    class BST<DJ>:Tree where DJ : IComparable
    {

        public BSTnode<DJ> CreateBSTNode()
        {

            
            var vId = NodeCount;
            if (ReusableNodeId.Count > 0) vId = ReusableNodeId.Pop();
            BSTnode<DJ> v = new BSTnode<DJ>(vId);

            Nodes.Add(v.Id, v);
            NodeCount = Nodes.Count;

            Dictionary<int, int> neighborsList = new Dictionary<int, int>();
            AdjList.Add(v.Id, neighborsList);

            return v;
        }


        //*******************
        //Still writing
        //*******************
        public bool Delete(DJ key)
        {
            if (RootNode == null)
                return false;
            

            //search for the current key
            BSTnode<DJ> currentNode = (BSTnode<DJ>) RootNode;



            while (currentNode != null)
            {
                if (key.CompareTo(currentNode.value) == 0)
                {
                    break;
                }


                //if key is greater than currenNode.value then go to the left
                if (key.CompareTo(currentNode.value) > 0)
                {
                    currentNode = (BSTnode<DJ>) currentNode.rightKid();
                }
                else
                {
                    currentNode = (BSTnode<DJ>) currentNode.leftKid();
                }
            }
            if (currentNode == null) return false;

            //if the currentnode has at most one kid, then assign currentnode to its kid
            if (currentNode.totalKids() < 2)
            {
                var mykid = currentNode.leftKid();
                if (mykid == null) mykid = currentNode.rightKid();
                var myParent = currentNode.Parent;
                if (myParent == null) //change the rootnode                   
                    RootNode = mykid;

                //current node is a leaf
                if (mykid == null)
                {
                    DeleteDirectedEdge(myParent, currentNode);
                    DeleteNode(currentNode);
                    if (myParent != null)
                        adjustTree((BSTnode<DJ>)myParent);
                    return true;
                }

                int kidNo = currentNode.PosAsKid;
                DeleteDirectedEdge(myParent, currentNode);
                DeleteDirectedEdge(currentNode, mykid);
                if (kidNo >= 0)
                    InsertDirectedEdge(myParent, mykid, kidNo);

                DeleteNode(currentNode);
                if (myParent != null)
                    adjustTree((BSTnode<DJ>)myParent);
                
            }
            else
            {
                //find the in-order predecessor of current node
                BSTnode<DJ> Y = (BSTnode<DJ>) inorderPredecessor(currentNode);
                //update the value
                currentNode.value = Y.value;
                //delete Y, Y does not have any right kid.
                var mykid = Y.leftKid();
                var myParent = Y.Parent;
                int kidNo = Y.PosAsKid;
                if (mykid == null)
                {
                    DeleteDirectedEdge(myParent, Y);
                    DeleteNode(Y);
                    if (myParent != null)
                        adjustTree((BSTnode<DJ>)myParent);
                    return true;
                }
                                
                DeleteDirectedEdge(myParent,Y);
                DeleteDirectedEdge(Y, mykid);
                if (kidNo >= 0)
                    InsertDirectedEdge(myParent, mykid, kidNo);

                DeleteNode(Y);
                //adjust from Y to the parent
                if (myParent!= null)
                    adjustTree((BSTnode<DJ>)myParent);
            }

            
            return true;
        }

        public static Node  inorderPredecessor(BSTnode<DJ> currentNode)
        {
            Node x = currentNode.leftKid();

            if (x == null)
            {
                Console.WriteLine("BST ERROR");
                return x;
            }
            while (x.rightKid() != null)            
                x = x.rightKid();
            
            return x;
        }

        public void Insert(DJ key)
        {                                    
            if (RootNode == null)
            {                
                BSTnode<DJ> newNode =  CreateBSTNode();
                newNode.value = key;
                RootNode = newNode;
                return;
            }

            //search for the current key
            BSTnode<DJ> currentNode = (BSTnode<DJ>)RootNode;
            BSTnode<DJ> ParentOfurrentNode = null;


            while (currentNode != null)
            {                                
                if (key.CompareTo(currentNode.value) == 0)
                {
                    Console.WriteLine("This BST is only for unique values.");
                    return;
                }

                ParentOfurrentNode = currentNode;

                //if key is greater than currenNode.value then go to the left
                if (key.CompareTo(currentNode.value) > 0)
                {
                    currentNode =  (BSTnode<DJ>)currentNode.rightKid();
                }
                else{
                    currentNode = (BSTnode<DJ>) currentNode.leftKid();
                }      
           
            }

            //add the key to the tree
            //if theres is a list of ids that are reusable then use that.
            
            BSTnode<DJ> leaf =   CreateBSTNode();
            leaf.value = key; 
            
             
            //insert the leaf
            if (key.CompareTo(ParentOfurrentNode.value) > 0)
                this.InsertDirectedEdge(ParentOfurrentNode, leaf, 1);
            else this.InsertDirectedEdge(ParentOfurrentNode, leaf, 0);


            currentNode = leaf;
            adjustTree(currentNode);
        }

        public void adjustTree(BSTnode<DJ> currentNode)
        {
            
            
            //travel upwards and find the first unbalanced node
            BSTnode<DJ> UnbalancedNode = null;
            //Queue<Node> q = new Queue<Node>();
            while (currentNode!=null)
            {

                //if (q.Count == 3) q.Dequeue();
                //q.Enqueue(currentNode);

                //check whether this is an unbalanced node
                int leftHeight = 0, rightHeight = 0;
                
                var leftKid = (BSTnode<DJ>) currentNode.leftKid();
                var rightKid = (BSTnode<DJ>) currentNode.rightKid();

                if (leftKid != null) leftHeight = leftKid.height;
                if (rightKid != null) rightHeight = rightKid.height;
                
                //update height
                currentNode.height = Math.Max(leftHeight, rightHeight) + 1;
                
                if (Math.Abs(leftHeight - rightHeight) >= 2)
                {
                    Node X, Y, Z = null;
                    //X = current, Y = child, Z = grand child
                    if (leftHeight > rightHeight)
                    {
                        X = currentNode;
                        Y = X.leftKid();
                        var k1 = (BSTnode<DJ>)Y.leftKid();
                        var k2 = (BSTnode<DJ>)Y.rightKid();
                        if (k1 == null) Z = k2;
                        if (k2 == null) Z = k1;
                        if (Z == null)
                        {
                            if (k1.height > k2.height) Z = k1;
                            else Z = k2;

                        }
                    }
                    else
                    {
                        X = currentNode;
                        Y = X.rightKid();
                        var k1 = (BSTnode<DJ>)Y.leftKid();
                        var k2 = (BSTnode<DJ>)Y.rightKid();
          
                        if (k1 == null) Z = k2;
                        if (k2 == null) Z = k1;
                        if (Z == null)
                        {
                            if (k1.height > k2.height) Z = k1;
                            else Z = k2;

                        }
                    }
                    //var Z = q.Dequeue();
                    //var Y = q.Dequeue();
                    //var X = q.Dequeue();
                    //update height and perform balanding operation
                    var Xleft = X.leftKid();
                    var Xright = X.rightKid();
                    var Yleft = Y.leftKid();
                    var Yright = Y.rightKid();
                    var Zleft = Z.leftKid();
                    var Zright = Z.rightKid();
                    if (Xleft != null && Yright != null && Xleft.Id == Y.Id && Yright.Id == Z.Id)
                    {
                        //left right  case
                        //Console.WriteLine("Left Right Adjustment");
                        currentNode = MakeAdjustment(Z, Y, X, Yleft, Zleft, Zright, Xright, currentNode);
                        //q.Enqueue(Y); q.Enqueue(Z); 
                    }
                    else if (Xright != null && Yleft != null && Xright.Id == Y.Id && Yleft.Id == Z.Id)
                    {
                        //right left  case
                        //Console.WriteLine("Right Left Adjustment");
                        currentNode = MakeAdjustment(Z, X, Y, Xleft, Zleft, Zright, Yright, currentNode);
                        //q.Enqueue(X); q.Enqueue(Z);
                    }
                    else if (Xleft != null && Yleft != null && Xleft.Id == Y.Id && Yleft.Id == Z.Id)
                    {
                        //left left  case
                        //Console.WriteLine("Left Left Adjustment");
                        currentNode = MakeAdjustment(Y, Z, X, Zleft, Zright, Yright, Xright, currentNode);
                        //q.Enqueue(Z); q.Enqueue(Y);
                    }
                    else if (Xright != null && Yright != null && Xright.Id == Y.Id && Yright.Id == Z.Id)
                    {
                        //right right  case
                        //Console.WriteLine("Right Right Adjustment");
                        currentNode = MakeAdjustment(Y, X, Z, Xleft, Yleft, Zleft, Zright, currentNode);
                        //q.Enqueue(X); q.Enqueue(Y);
                    }
                }
                
                currentNode = (BSTnode<DJ>) currentNode.Parent;
            }

        }
        
        private BSTnode<DJ> MakeAdjustment(Node M, Node L, Node R, Node A, Node B, Node C, Node D, BSTnode<DJ> currentNode)
        {
            Node root = currentNode.Parent; 
            int kidNo = currentNode.PosAsKid;
            if (root != null)
            {
                if (kidNo == 0)
                    DeleteDirectedEdge(root, root.leftKid());
                else
                    DeleteDirectedEdge(root, root.rightKid());
            }
            
            DeleteDirectedEdge(M, M.leftKid());
            DeleteDirectedEdge(M, M.rightKid());
            DeleteDirectedEdge(L, L.leftKid());
            DeleteDirectedEdge(L, L.rightKid());
            DeleteDirectedEdge(R, R.leftKid());
            DeleteDirectedEdge(R, R.rightKid());
            
            InsertDirectedEdge(M,L, 0);
            InsertDirectedEdge(M,R, 1);            
            InsertDirectedEdge(L,A, 0);
            InsertDirectedEdge(L,B, 1);
            InsertDirectedEdge(R,C, 0);
            InsertDirectedEdge(R,D, 1);

            var bstM = (BSTnode<DJ>) M;
            var bstL = (BSTnode<DJ>)L;
            var bstR = (BSTnode<DJ>)R;
            var bstA = (BSTnode<DJ>)A;
            var bstB = (BSTnode<DJ>)B;
            var bstC = (BSTnode<DJ>)C;
            var bstD = (BSTnode<DJ>)D;

            adjustHeight(bstL,bstA,bstB);
            adjustHeight(bstR, bstC, bstD);
            adjustHeight(bstM, bstL, bstR);

                            
            bstM.Parent = root;
            if (root != null)
            {
                if (kidNo == 0)
                    InsertDirectedEdge(root, M, 0);
                else
                    InsertDirectedEdge(root, M, 1);
            }

            if (root == null) RootNode = M; //we are changing the root

            return bstM;
        }

        void adjustHeight(BSTnode<DJ> X, BSTnode<DJ> A, BSTnode<DJ> B)
        {
            int left, right;
            if (A == null) left = 0;
            else left = A.height;
            if (B == null) right = 0;
            else right = B.height;
            X.height = Math.Max(left, right) + 1;
        }
               
        public  void printTree()
        {
            if (RootNode == null) return;
            Console.WriteLine("Nodes = " + NodeCount + " Root = " + ((BSTnode<DJ>)RootNode).value);
            Queue<BSTnode<DJ>> q = new Queue<BSTnode<DJ>>();
            q.Enqueue((BSTnode<DJ>)RootNode);
            while (q.Count > 0)
            {
                var currentNode = q.Dequeue();
                var left = (BSTnode<DJ>)currentNode.leftKid();
                var right = (BSTnode<DJ>)currentNode.rightKid();

                Console.Write("(");
                Console.Write(currentNode.value+",");
                if (left != null) Console.Write(left.value + ",");
                else Console.Write("-,");
                if (right != null) Console.Write(right.value + ",");
                else Console.Write("-,");
                Console.Write(")");
                foreach (var kid in currentNode.Kids.Values)
                {
                    q.Enqueue((BSTnode<DJ>)kid);
                }
            }
            Console.WriteLine("");
        }

        public DJ  ExtractMin()
        {
            if (RootNode == null) 
                Console.Write("Error: BST is Empty");
            Node w =  RootNode;
            var currentNode = w;
            while (currentNode != null)
            {
                w = currentNode;
                currentNode = currentNode.leftKid();
            }

            var result = ((BSTnode<DJ>) w).value;

            if (RootNode != null) 
                Delete(result);
            
            return result;
        }
        
        public bool IsEmpty()
        {
            if (RootNode == null) return true;
            return false;
        }
    }

 
    public class BSTnode<DJ>: Node
    {
        public DJ value;
        public int height;
        public BSTnode(int a): base(a)
        {
            height = 1;
        }
    }
}
