using System;
using System.Collections.Generic;
using System.Linq;

namespace shlosi3
{
    public class PriorityQueue<T>
        where T : IComparable
    {
        protected List<T> StoredValues { get; }
        public virtual int Count => StoredValues.Count - 1;

        public PriorityQueue()
        {
            StoredValues = new List<T>();
        }

        public virtual void Enqueue(T value)
        {
            StoredValues.Add(value);
            BubbleUp(StoredValues.Count - 1);
        }

        public virtual T Dequeue()
        {
            if (Count == 0)
            {
                return default(T);
            }

            var minValue = StoredValues[0];

            if (StoredValues.Count > 2)
            {
                var lastValue = StoredValues[StoredValues.Count - 1];

                StoredValues.RemoveAt(StoredValues.Count - 1);
                StoredValues[0] = lastValue;

                BubbleDown(0);
            }
            else
            {
                StoredValues.RemoveAt(0);
            }

            return minValue;
        }

        protected virtual void BubbleUp(int startCell)
        {
            var cell = startCell;

            while (IsParentBigger(cell))
            {
                var parentValue = StoredValues[cell / 2];
                var childValue = StoredValues[cell];

                StoredValues[cell / 2] = childValue;
                StoredValues[cell] = parentValue;

                cell /= 2;
            }
        }

        protected virtual void BubbleDown(int startCell)
        {
            var cell = startCell;

            while (IsLeftChildSmaller(cell) || IsRightChildSmaller(cell))
            {
                var child = CompareChild(cell);

                if (child == -1)
                {
                    T parentValue = StoredValues[cell];
                    T leftChildValue = StoredValues[2 * cell];

                    StoredValues[cell] = leftChildValue;
                    StoredValues[2 * cell] = parentValue;

                    cell = 2 * cell;
                }
                else if (child == 1)
                {
                    var parentValue = StoredValues[cell];
                    var rightChildValue = StoredValues[2 * cell + 1];

                    StoredValues[cell] = rightChildValue;
                    StoredValues[2 * cell + 1] = parentValue;

                    cell = 2 * cell + 1;
                }
            }
        }

        protected virtual bool IsParentBigger(int childCell)
        {
            return childCell != 0 && StoredValues[childCell / 2].CompareTo(StoredValues[childCell]) > 0;
        }

        protected virtual bool IsLeftChildSmaller(int parentCell)
        {
            return 2 * parentCell < StoredValues.Count &&
                   StoredValues[2 * parentCell].CompareTo(StoredValues[parentCell]) < 0;
        }

        protected virtual bool IsRightChildSmaller(int parentCell)
        {
            return 2 * parentCell + 1 < StoredValues.Count &&
                   StoredValues[2 * parentCell + 1].CompareTo(StoredValues[parentCell]) < 0;
        }

        protected virtual int CompareChild(int parentCell)
        {
            var leftChildSmaller = IsLeftChildSmaller(parentCell);
            var rightChildSmaller = IsRightChildSmaller(parentCell);

            if (leftChildSmaller || rightChildSmaller)
            {
                if (leftChildSmaller && rightChildSmaller)
                {
                    var leftChild = 2 * parentCell;
                    var rightChild = 2 * parentCell + 1;

                    var leftValue = StoredValues[leftChild];
                    var rightValue = StoredValues[rightChild];

                    return leftValue.CompareTo(rightValue) <= 0 ? -1 : 1;
                }

                return leftChildSmaller ? -1 : 1;
            }

            return 0;
        }

        public void EnqueueRange(IList<T> values)
        {
            foreach (var value in values)
            {
                Enqueue(value);
            }
        }

        public T GetItem(Func<T, bool> predicate)
        {
            return StoredValues.FirstOrDefault(predicate);
        }

        public void UpdateItem(T item)
        {
            BubbleUp(StoredValues.IndexOf(item));
        }
    }
}
