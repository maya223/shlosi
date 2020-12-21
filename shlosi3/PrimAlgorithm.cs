using System;
using System.Collections.Generic;
using System.Linq;

namespace prim
{
    public class PrimAlgorithm<T> where T : IComparable
    {
        readonly int infinite = int.MaxValue - 1;

        public DirectedGraph<T> GetMst(Graph<T> g, Vertex<T> sourceVertex)
        {
            var mst = new DirectedGraph<T>{Vertices = g.Vertices};
            var parentsDictionary = new Dictionary<Vertex<T>, Vertex<T>>();
            var dictionary = GetVerticesDictionary(g.Vertices);
            dictionary[sourceVertex] = 0;

            var q = GetVerticesQueue(dictionary);

            while (q.Count > 0)
            {
                var currentVertex = q.Dequeue();
                var edges = g.Edges.Where(x => x.SourceVertex.Value.Equals(currentVertex.Value));

                foreach (var edge in edges)
                {
                    var neighbor = edge.DestinationVertex;

                    if (IsVertexInQueue(q, neighbor) && edge.Weight < dictionary[neighbor])
                    {
                        dictionary[neighbor] = edge.Weight;

                        var qItem = GetVertexInQueue(q, neighbor);
                        
                        qItem.Weight = edge.Weight;
                        q.UpdateItem(qItem);

                        var newEdge = new Edge<T>(currentVertex, neighbor, edge.Weight);
                        ReplaceVertexParent(mst, neighbor, newEdge);
                    }
                }
            }

            return mst;
        }

        private bool IsVertexInQueue(PriorityQueue<WeightedVertex<T>> queue, Vertex<T> vertex)
        {
            return GetVertexInQueue(queue, vertex) != null;
        }

        private WeightedVertex<T> GetVertexInQueue(PriorityQueue<WeightedVertex<T>> queue, Vertex<T> vertex)
        {
            return queue.GetItem(x=> x.Value.Equals(vertex.Value));
        }

        private void ReplaceVertexParent(DirectedGraph<T> tree, Vertex<T> destVertex, Edge<T> newEdge)
        {
            tree.Edges.RemoveAll(x => x.DestinationVertex.Value.Equals(destVertex.Value));
            tree.AddEdge(newEdge);
        }

        private Dictionary<Vertex<T>, int> GetVerticesDictionary(List<Vertex<T>> vertices)
        {
            return vertices.ToDictionary(x=> x, x => infinite);
        }

        private PriorityQueue<WeightedVertex<T>> GetVerticesQueue(Dictionary<Vertex<T>, int> verticesDictionary)
        {
            var q = new PriorityQueue<WeightedVertex<T>>();
            var weightedVertices = verticesDictionary.Select(x => new WeightedVertex<T>(x.Key, x.Value)).ToList();

            q.EnqueueRange(weightedVertices);
            return q;
        }
    }
}