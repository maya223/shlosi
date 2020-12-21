using System;

namespace prim
{
    public class Edge<T>
    {
        public Vertex<T> SourceVertex { get; set; }
        public Vertex<T> DestinationVertex { get; set; }
        public int Weight { get; set; }

        public Edge(Vertex<T> sourceVertex, Vertex<T> destinationVertex,int weight=0)
        {
            SourceVertex = sourceVertex;
            DestinationVertex = destinationVertex;
            Weight = weight;
        }
    }
}