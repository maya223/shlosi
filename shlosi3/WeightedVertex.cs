using System;
using System.Collections.Generic;

namespace prim
{
    public class WeightedVertex<T> : Vertex<T>, IComparable
    {
        public int Weight { get; set; }
        public WeightedVertex(Vertex<T> vertex, int weight) : base(vertex.Value)
        {
            Weight = weight;
        }

        public int CompareTo(object obj)
        {
            return Weight.CompareTo(((WeightedVertex<T>)obj).Weight);
        }
    }
}