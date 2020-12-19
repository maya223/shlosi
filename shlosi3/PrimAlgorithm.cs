using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class PrimAlgorithm<T> where T : IComparable
    {
        readonly int infinite = int.MaxValue - 1;

        public DirectedGraph<T> Prim(Graph<T> g, Vertex<T> sourceVertex)
        {
            var mst = new DirectedGraph<T>{Vertices = g.Vertices};
            var dictionary = GetVerticesDictionary(g.Vertices);
            dictionary[sourceVertex] = 0;

            var q = GetQueue(dictionary);

            while (q.Count > 0)
            {
                var currentVertex = q.Dequeue();

                foreach (var edge in g.Edges.Where(x=>x.SourceVertex.Value.Equals(currentVertex.Value)))
                {
                    var neighbor = edge.DestinationVertex;

                    if (IsVertexInQueue(q, neighbor) && edge.Weight < dictionary[neighbor])
                    {
                        dictionary[neighbor] = edge.Weight;

                        var qItem = GetVertexInQueue(q, neighbor);
                        qItem.Weight = edge.Weight;

                        q.UpdateItem(q.StoredValues.IndexOf(qItem));

                        mst.AddEdge(currentVertex, neighbor, edge.Weight);
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
            return queue.StoredValues.FirstOrDefault(x=> x.Value.Equals(vertex.Value));
        }

        private Dictionary<Vertex<T>, int> GetVerticesDictionary(List<Vertex<T>> vertices)
        {
            return vertices.ToDictionary(x=> x, x => infinite);
        }

        private PriorityQueue<WeightedVertex<T>> GetQueue(Dictionary<Vertex<T>, int> verticesDictionary)
        {
            var q = new PriorityQueue<WeightedVertex<T>>();
            var weightedVertices = verticesDictionary.Select(x => new WeightedVertex<T>(x.Key, x.Value)).ToList();

            q.EnqueueRange(weightedVertices);
            return q;
        }
    }
}