using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.SynthesizerPrototype.Calculation
{
    internal class DimensionStack
    {
        private const int DEFAULT_CAPACITY = 128;

        private double[] _array;

        public DimensionStack()
        {
            _array = new double[DEFAULT_CAPACITY];
            Count = 0;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            private set;
        }

        public int CurrentIndex
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Count - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(double value)
        {
            bool mustIncreaseCapacity = _array.Length == Count;

            if (mustIncreaseCapacity)
            {
                int newCapacity;

                if (Count == 0)
                {
                    newCapacity = DEFAULT_CAPACITY;
                }
                else
                {
                    newCapacity = Count * 2;
                }

                var array2 = new double[newCapacity];

                Array.Copy(_array, 0, array2, 0, Count);

                _array = array2;
            }

            _array[Count] = value;

            Count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double PopAndGet()
        {
            Count--;

            double value = _array[Count];
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop() => Count--;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get()
        {
            double value = _array[Count - 1];
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get(int i)
        {
            double value = _array[i];
            return value;
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operations in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(double value) => _array[Count - 1] = value;

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operations in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int i, double value) => _array[i] = value;
    }
}