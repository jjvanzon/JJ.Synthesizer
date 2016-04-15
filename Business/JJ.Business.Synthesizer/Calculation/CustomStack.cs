using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    /// <summary> A less safe, faster variation of Stack<T>. </summary>
    internal class CustomStack<T>
    {
        private const int DEFAULT_CAPACITY = 128;

        private int _size;
        private T[] _array;

        public CustomStack()
        {
            _array = new T[DEFAULT_CAPACITY];
            _size = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(T item)
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

                var array2 = new T[capacity];

                Array.Copy(_array, 0, array2, 0, _size);

                _array = array2;
            }

            _array[_size] = item;

            _size++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Pop()
        {
            _size--;

            T item = _array[_size];

            return item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Peek()
        {
            T item = _array[_size - 1];

            return item;
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operations in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(T item)
        {
            _array[_size - 1] = item;
        }
    }
}
