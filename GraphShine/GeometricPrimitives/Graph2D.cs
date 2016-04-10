using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace GraphShine.GraphPrimitives
{
    public class Graph2D : Graph
    {
       
        public Graph2D():base()
        {
        }

    }

    public class Vertex2D : Vertex,IComparable
    {
        public double X, Y;
        public Vertex2D(int a, double p, double q):base(a)
        {
            X = p;
            Y = q;
        }

        int IComparable.CompareTo(object obj)
        {
            Vertex c = (Vertex)obj;

            if (this.Weight < c.Weight) return -1;
            if (this.Weight > c.Weight) return 1;

            //if weights are the same
            if (this.Id < c.Id) return -1;
            if (this.Id > c.Id) return 1;
            
            return 0;
        }

        public override string ToString()
        {
            return "" + Id ;
        }
    }
    
}
