using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    class Program
    {
        static void Main(string[] args)
        {
            // question 1
            var mst = GetMST();

            mst.Edges.ForEach( x=>
            {
                Console.WriteLine($"{x.SourceVertex.Value} => {x.DestinationVertex.Value}");
            });

            // question 2
            var q2 = new Question2<char>();
            //q2.AddEdgeToMST(mst, new Edge<char>(mst.Vertices[0], mst.Vertices[4], 2));
        }

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
        
        // Question 1 
        private static DirectedGraph<char> GetMST()
        {
            var graph = GenerateGraph();
            var prim = new PrimAlgorithm<char>();

            return prim.Prim(graph, graph.Vertices[0]);
        }
    }
}
