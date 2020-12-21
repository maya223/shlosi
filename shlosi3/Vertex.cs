namespace prim
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