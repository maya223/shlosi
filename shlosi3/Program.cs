using System;
using System.Collections.Generic;

namespace shlosi3
{
    public class Program
    {
        public static void Main()
        {
            var mstHelper = new MstHelper();

            // question 1
            var mst = GetMst();
            mstHelper.PrintMst(mst);

            Console.WriteLine("-----------------------------------------");

            // question 2
            mstHelper.AddEdgeToMst(mst, new Edge<char>(mst.Vertices[0], mst.Vertices[4], 2));
            mstHelper.PrintMst(mst);
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
        
        private static DirectedGraph<char> GetMst()
        {
            var graph = GenerateGraph();
            var prim = new PrimAlgorithm<char>();

            return prim.GetMst(graph, graph.Vertices[0]);
        }

        /**************************************************/
    }
}
