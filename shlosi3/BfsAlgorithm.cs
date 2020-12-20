using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace shlosi3
{
    public class BfsAlgorithm
    {
        readonly int infinity = int.MaxValue - 1;

        public Dictionary<Vertex<T>, Vertex<T>> Bfs<T>(Vertex<T> sourceVertex, Graph<T> graph)
        {
            var queue = new Queue<Vertex<T>>();

            // init dictionaries
            var colors = graph.Vertices.ToDictionary(x => x, x => BfsColors.White);
            var d = graph.Vertices.ToDictionary(x => x, x=> infinity);
            var parents = new Dictionary<Vertex<T>, Vertex<T>>();

            // init source vertex
            colors[sourceVertex] = BfsColors.Gray;
            d[sourceVertex] = 0;
            parents[sourceVertex] = null;
            queue.Enqueue(sourceVertex);

            while (queue.Any())
            {
                var currentVertex = queue.Dequeue();
                var neighbors = graph.Edges
                    .Where(x => x.SourceVertex.Value.Equals(currentVertex.Value))
                    .Select(x => x.DestinationVertex);

                foreach (var neighbor in neighbors)
                {
                    if (colors[neighbor] == BfsColors.White)
                    {
                        colors[neighbor] = BfsColors.Gray;
                        d[neighbor] = d[currentVertex] + 1;
                        parents[neighbor] = currentVertex;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return parents;
        }
    }
}