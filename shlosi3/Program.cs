using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class Program
    {
        public static Random Rand = new Random();
        public static int EDGES_COUNT = 50;
        public static int VERTICES_COUNT = 21;
        public static int MIN_WEIGHT = 4;
        public static int MAX_WEIGHT = 52;

        public static void Main()
        {
            var mstHelper = new MstHelper();
            var prim = new PrimAlgorithm<char>();

            // question 1
            Console.WriteLine("----------------- Question 1 ---------------");

            var graph = GenerateGraph();
            var mst = prim.GetMst(graph, graph.Vertices[0]);
            //PrintDirectedGraph(graph);

            Console.WriteLine("-----------------------------------------");

            // question 2
            Console.WriteLine("----------------- Question 2 ---------------");
            //mstHelper.AddEdgeToMst(mst, new Edge<char>(mst.Vertices[0], mst.Vertices[4], 2));
            //PrintDirectedGraph(graph);

            Console.WriteLine("-----------------------------------------");

            // question 3
            graph = GenerateRandomGraph();

            Console.WriteLine("---------------- GRAPH ----------------\r\n");
            PrintGraph(graph);

            mst = prim.GetMst(graph, graph.Vertices[0]);

            Console.WriteLine("------------------ MST - BEFORE --------------------");
            PrintDirectedGraph(mst);

            mstHelper.AddEdgeToMst(mst, GenerateNewEdge(graph));
            Console.WriteLine("------------------ MST - AFTER --------------------");
            PrintDirectedGraph(mst);
        }

        private static Edge<T> GenerateNewEdge<T>(Graph<T> graph)
        {
            var sourceIndex = Rand.Next(VERTICES_COUNT);
            var destinantionVertex = GenerateDestinationIndex(sourceIndex, graph);
            
            return new Edge<T>(graph.Vertices[sourceIndex], 
                graph.Vertices[destinantionVertex], MIN_WEIGHT - 1);
        }

        /*************** Question 1 ****************/

        private static Graph<char> GenerateGraph()
        {
            var graph = new Graph<char>();

            var a = new Vertex<char>('a');
            var b = new Vertex<char>('b');
            var c = new Vertex<char>('c');
            var d = new Vertex<char>('d');
            var e = new Vertex<char>('e');
            var f = new Vertex<char>('f');
            var g = new Vertex<char>('g');
            var h = new Vertex<char>('h');
            var i = new Vertex<char>('i');
            var j = new Vertex<char>('j');
            var k = new Vertex<char>('k');
            var l = new Vertex<char>('l');

            graph.Vertices.AddRange(new List<Vertex<char>>
            {
                a, b, c, d, e, f, g, h, i, j, k, l
            });

            graph.AddEdge(a, b, 12);
            graph.AddEdge(a, d, 5);
            graph.AddEdge(a, c, 23);
            graph.AddEdge(c, d, 18);
            graph.AddEdge(c, e, 17);
            graph.AddEdge(b, f, 7);
            graph.AddEdge(e, i, 16);
            graph.AddEdge(e, j, 14);
            graph.AddEdge(d, g, 9);
            graph.AddEdge(d, f, 10);
            graph.AddEdge(f, l, 20);
            graph.AddEdge(i, k, 7);
            graph.AddEdge(g, h, 4);
            graph.AddEdge(g, j, 3);
            graph.AddEdge(h, l, 8);
            graph.AddEdge(k, l, 12);

            return graph;
        }

        private static Graph<char> GenerateRandomGraph()
        {
            var vertexValue = 'a';

            var graph = new Graph<char>();

            for (var index = 0; index < VERTICES_COUNT; index++)
            {
                graph.Vertices.Add(new Vertex<char>(vertexValue));
                vertexValue++;
            }

            int weight, sourceIndex, destinationIndex;

            for (var index = 0; index < EDGES_COUNT; index++)
            {
                weight = Rand.Next(MIN_WEIGHT, MAX_WEIGHT);
                sourceIndex = Rand.Next(VERTICES_COUNT);
                
                destinationIndex = GenerateDestinationIndex(sourceIndex, graph);

                graph.AddEdge(graph.Vertices[sourceIndex],
                                graph.Vertices[destinationIndex], weight);
                
            }

            return graph;
        }

        private static int GenerateDestinationIndex<T>(int sourceIndex, Graph<T> graph)
        {
            var destinationIndex = Rand.Next(VERTICES_COUNT);

            while (destinationIndex == sourceIndex || 
                   graph.Edges.Any(edge => DoEdgesMatch(edge, new Edge<T>(graph.Vertices[sourceIndex], graph.Vertices[destinationIndex]))))
            {
                destinationIndex = Rand.Next(VERTICES_COUNT);
            };

            return destinationIndex;
        }

        private static bool DoEdgesMatch<T>(Edge<T> edge1, Edge<T> edge2)
        {
            return (edge1.SourceVertex.Value.Equals(edge2.SourceVertex.Value) &&
                    edge1.DestinationVertex.Value.Equals(edge2.DestinationVertex.Value)) ||
                   (edge1.SourceVertex.Value.Equals(edge2.DestinationVertex.Value) &&
                    edge1.DestinationVertex.Value.Equals(edge2.SourceVertex.Value));
        }

        public static void PrintGraph<T>(Graph<T> graph)
        {
            var edgesToPrint = new List<Edge<T>>();
            
            foreach (var edge in graph.Edges)
            {
                if (!edgesToPrint.Any(x => DoEdgesMatch(x, edge)))
                {
                    edgesToPrint.Add(edge);
                }
            }

            edgesToPrint.ForEach(x => { Console.WriteLine($"{x.SourceVertex.Value} == {x.Weight} == {x.DestinationVertex.Value}"); });
        }

        public static void PrintDirectedGraph<T>(DirectedGraph<T> graph)
        {
            graph.Edges.ForEach(x => { Console.WriteLine($"{x.SourceVertex.Value} == {x.Weight} ==> {x.DestinationVertex.Value}"); });
        }
    }
}
