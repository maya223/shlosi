using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class MstHelper
    {
        public void AddEdgeToMst<T>(DirectedGraph<T> mst, Edge<T> edge)
        {
            var path = FindPath(mst, edge.SourceVertex, edge.DestinationVertex);
            var maxWeightEdge = GetMaxWeightEdgeInPath(mst, path);

            if (edge.Weight < maxWeightEdge.Weight)
            {
                mst.Edges.Remove(maxWeightEdge);
                mst.AddEdge(edge);
            }
        }

        private Dictionary<Vertex<T>, Vertex<T>> FindPath<T>(DirectedGraph<T> mst, Vertex<T> source, Vertex<T> destination)
        {
            var path = new Dictionary<Vertex<T>, Vertex<T>>();
            var parents = mst.Edges.ToDictionary(edge => edge.DestinationVertex.Value, edge => edge.SourceVertex);

            var currentVertex = destination;

            while (!currentVertex.Value.Equals(source.Value))
            {
                path.Add(currentVertex, parents[currentVertex.Value]);
                currentVertex = parents[currentVertex.Value];
            }

            return path;
        }
        
        private Edge<T> GetMaxWeightEdgeInPath<T>(Graph<T> graph, Dictionary<Vertex<T>, Vertex<T>> path)
        {
            var edgesInPath = path.SelectMany(x =>
                GetMatchingEdges(graph, x.Value, x.Key)).ToList();

            var maxWeight = edgesInPath.Max(x => x.Weight);
            
            return edgesInPath.FirstOrDefault(edge => edge.Weight == maxWeight);
        }

        private IEnumerable<Edge<T>> GetMatchingEdges<T>(Graph<T> graph, Vertex<T> sourceVertex, Vertex<T> destVertex)
        {
            return graph.Edges.Where(edge => edge.SourceVertex.Value.Equals(sourceVertex.Value) &&
                                             edge.DestinationVertex.Value.Equals(destVertex.Value));
        }
    }
}