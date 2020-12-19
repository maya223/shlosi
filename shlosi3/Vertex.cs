using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class Vertex<T>
    {
        public T Value { get; }

        public Vertex(T value)
        {
            Value = value;
        }
    }
}