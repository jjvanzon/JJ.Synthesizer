﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation
{
    public class DimensionStack
    {
        private static int _dimensionCount = GetDimensionCount();

        /// <summary> Array index is the dimension enum member. </summary>
        private CustomStack<double>[] _stacks;

        private static int GetDimensionCount()
        {
            int dimensionEnumMaxValue = EnumHelper.GetValues<DimensionEnum>()
                                                  .Select(x => (int)x)
                                                  .Max();

            int dimensionCount = dimensionEnumMaxValue + 1;
            return dimensionCount;
        }

        public DimensionStack()
        {
            _stacks = new CustomStack<double>[_dimensionCount];

            for (int i = 0; i < _dimensionCount; i++)
            {
                var stack = new CustomStack<double>();

                // Initialize stack with one item in it, so Peek immediately works to return 0.0.
                stack.Push(0.0);

                _stacks[i] = stack;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(DimensionEnum dimensionEnum, double value)
        {
            Push((int)dimensionEnum, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(int dimensionEnumInt, double value)
        {
            _stacks[dimensionEnumInt].Push(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop(DimensionEnum dimensionEnum)
        {
            Pop((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop(int dimensionEnumInt)
        {
            _stacks[dimensionEnumInt].Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get(DimensionEnum dimensionEnum)
        {
            return Get((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get(int dimensionEnumInt)
        {
            return _stacks[dimensionEnumInt].Peek();
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operators in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(DimensionEnum dimensionEnum, double value)
        {
            Set((int)dimensionEnum, value);
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operators in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int dimensionEnumInt, double value)
        {
            _stacks[dimensionEnumInt].Set(value);
        }
    }
}