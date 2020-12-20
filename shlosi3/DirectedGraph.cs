using System;

namespace shlosi3
{
    public class DirectedGraph<T> : Graph<T>
    {
        public override void AddEdge(Vertex<T> v1, Vertex<T> v2, int weight)
        {
            Edges.Add(new Edge<T>(v1,v2, weight));
        }

        public override void AddEdge(Edge<T> edge)
        {
            Edges.Add(edge);
        }
    }
}