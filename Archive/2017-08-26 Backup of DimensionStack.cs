﻿//using System;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Enums;

//namespace JJ.Business.Synthesizer.Calculation
//{
//    internal class DimensionStack
//    {
//        private const int DEFAULT_CAPACITY = 128;

//        public DimensionEnum StandardDimensionEnum { get; }
//        public string CanonicalCustomDimensionName { get; }

//        private int _count;
//        private double[] _array;

//        public DimensionStack(DimensionEnum standardDimensionEnum)
//            : this()
//        {
//            StandardDimensionEnum = standardDimensionEnum;
//        }

//        public DimensionStack(string canonicalCustomDimensionName)
//            : this()
//        {
//            CanonicalCustomDimensionName = canonicalCustomDimensionName;
//        }

//        public DimensionStack(DimensionEnum standardDimensionEnum, string canonicalCustomDimensionName)
//            : this()
//        {
//            StandardDimensionEnum = standardDimensionEnum;
//            CanonicalCustomDimensionName = canonicalCustomDimensionName;
//        }

//        private DimensionStack()
//        {
//            _array = new double[DEFAULT_CAPACITY];
//            _count = 0;
//        }

//        // ReSharper disable once ConvertToAutoPropertyWhenPossible
//        public int Count
//        {
//            [MethodImpl(MethodImplOptions.AggressiveInlining)]
//            get => _count; 
//        }

//        public int CurrentIndex
//        {
//            [MethodImpl(MethodImplOptions.AggressiveInlining)]
//            get => _count - 1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Push(double value)
//        {
//            bool mustIncreaseCapacity = _array.Length == _count;
//            if (mustIncreaseCapacity)
//            {
//                int newCapacity;
//                if (_count == 0)
//                {
//                    newCapacity = DEFAULT_CAPACITY;
//                }
//                else
//                {
//                    newCapacity = _count * 2;
//                }

//                var array2 = new double[newCapacity];

//                Array.Copy(_array, 0, array2, 0, _count);

//                _array = array2;
//            }

//            _array[_count] = value;

//            _count++;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double PopAndGet()
//        {
//            _count--;

//            double value = _array[_count];
//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Pop()
//        {
//            _count--;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public virtual double Get()
//        {
//            double value = _array[_count - 1];
//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double Get(int i)
//        {
//            double value = _array[i];
//            return value;
//        }

//        /// <summary>
//        /// A slightly quicker alternative to a subsequent Pop and Push,
//        /// when you know there will not be any stack operations in between,
//        /// or when you know you are at the top level of the stack.
//        /// </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Set(double value)
//        {
//            _array[_count - 1] = value;
//        }

//        /// <summary>
//        /// A slightly quicker alternative to a subsequent Pop and Push,
//        /// when you know there will not be any stack operations in between,
//        /// or when you know you are at the top level of the stack.
//        /// </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Set(int i, double value)
//        {
//            _array[i] = value;
//        }
//    }
//}
