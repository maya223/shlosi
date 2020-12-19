using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class Question2<T>
    {
        public void AddEdgeToMST(DirectedGraph<T> mst, Edge<T> edge)
        {
            var path = BFS(edge.SourceVertex, mst);

            if (edge.Weight < MaxEdgeWeight(path))
            {
                mst.AddEdge(edge);
                //MST.Add(edge.Vertex, sourceVertex);

                // remove maxEdge
            }
        }

        private DirectedGraph<T> TreeToGraph(Dictionary<Vertex<T>, Vertex<T>> tree)
        {
            var graph = new DirectedGraph<T>();
        

            return graph;
        }

        private Queue<Vertex<T>> BFS(Vertex<T> sourceVertex, Graph<T> graph)
        {
            var visited = graph.Vertices.ToDictionary(x=> x, x=> false);
            var queue = new Queue<Vertex<T>>();

            visited[sourceVertex] = true;
            queue.Enqueue(sourceVertex);

            while (queue.Any())
            {
                var currentVertex = queue.Dequeue();
                var neighbors = graph.Edges.
                    Where(x=>x.SourceVertex.Equals(currentVertex)).Select(x => x.DestinationVertex);

                foreach (var neighbor in neighbors)
                {
                    if (!visited[neighbor])
                    {
                        visited[neighbor] = true;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return queue;
        }

        private int MaxEdgeWeight(object path)
        {
            throw new System.NotImplementedException();
        }
    }
}