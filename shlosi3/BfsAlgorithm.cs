using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace prim
{
    public class BfsAlgorithm
    {
        readonly int infinity = int.MaxValue - 1;

        public Dictionary<T, Vertex<T>> Bfs<T>(Vertex<T> sourceVertex, Graph<T> graph)
        {
            var queue = new Queue<Vertex<T>>();

            // init dictionaries
            var colors = graph.Vertices.ToDictionary(x => x.Value, x => BfsColors.White);
            var d = graph.Vertices.ToDictionary(x => x.Value, x => infinity);
            var parents = new Dictionary<T, Vertex<T>>();

            // init source vertex
            colors[sourceVertex.Value] = BfsColors.Gray;
            d[sourceVertex.Value] = 0;
            parents[sourceVertex.Value] = null;
            queue.Enqueue(sourceVertex);

            while (queue.Any())
            {
                var currentVertex = queue.Dequeue();
                var neighbors = graph.Edges
                    .Where(x => x.SourceVertex.Value.Equals(currentVertex.Value) ||
                                       x.DestinationVertex.Value.Equals(currentVertex.Value))
                    .Select(x => x.DestinationVertex.Value.Equals(currentVertex.Value) ? x.SourceVertex : x.DestinationVertex);

                foreach (var neighbor in neighbors)
                {
                    if (colors[neighbor.Value] == BfsColors.White)
                    {
                        colors[neighbor.Value] = BfsColors.Gray;
                        d[neighbor.Value] = d[currentVertex.Value] + 1;
                        parents[neighbor.Value] = currentVertex;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return parents;
        }
    }
}