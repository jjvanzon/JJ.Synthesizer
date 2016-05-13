using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class DimensionStack
    {
        private const int DEFAULT_CAPACITY = 128;

        private int _size;
        private double[] _array;

        public DimensionStack()
        {
            _array = new double[DEFAULT_CAPACITY];
            _size = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(double item)
        {
            bool mustIncreaseCapacity = _array.Length == _size;
            if (mustIncreaseCapacity)
            {
                int capacity;
                if (_size == 0)
                {
                    capacity = DEFAULT_CAPACITY;
                }
                else
                {
                    capacity = _size * 2;
                }

                var array2 = new double[capacity];

                Array.Copy(_array, 0, array2, 0, _size);

                _array = array2;
            }

            _array[_size] = item;

            _size++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double PopAndGet()
        {
            _size--;

            double item = _array[_size];

            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop()
        {
            _size--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get()
        {
            double item = _array[_size - 1];

            return item;
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operations in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(double item)
        {
            _array[_size - 1] = item;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _size; }
        }
    }
}
