using System;
using System.Collections.Generic;

namespace prim
{
    public class Graph<T>
    {
        public List<Vertex<T>> Vertices { get; set; }
        public List<Edge<T>> Edges { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex<T>>();
            Edges = new List<Edge<T>>();
        }

        public virtual void AddEdge(Vertex<T> v1, Vertex<T> v2, int weight)
        {
            Edges.Add(new Edge<T>(v1, v2, weight));
            Edges.Add(new Edge<T>(v2, v1, weight));
        }

        public virtual void AddEdge(Edge<T> edge)
        {
            Edges.Add(edge);
            Edges.Add(new Edge<T>(edge.DestinationVertex, edge.SourceVertex, edge.Weight));
        }
    }
}