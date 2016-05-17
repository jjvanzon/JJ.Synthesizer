using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation
{
    /// <summary>
    /// This class maintains for each dimension a stack of values 
    /// and tries to make access to these values as fast as possible.
    /// </summary>
    internal class DimensionStackCollection
    {
        private static int _dimensionCount = GetDimensionCount();

        /// <summary> Array index is the dimension enum member. </summary>
        private DimensionStack[] _stacks;

        private static int GetDimensionCount()
        {
            int dimensionEnumMaxValue = EnumHelper.GetValues<DimensionEnum>()
                                                  .Select(x => (int)x)
                                                  .Max();

            int dimensionCount = dimensionEnumMaxValue + 1;
            return dimensionCount;
        }

        public DimensionStackCollection()
        {
            _stacks = new DimensionStack[_dimensionCount];

            for (int i = 0; i < _dimensionCount; i++)
            {
                var stack = new DimensionStack();

                // Initialize stack with one item in it, so Peek immediately works to return 0.0.
                stack.Push(0.0);

                _stacks[i] = stack;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DimensionStack GetDimensionStack(DimensionEnum dimensionEnum)
        {
            return GetDimensionStack((int)dimensionEnum);
        }

        // TODO: Get rid of this overload.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DimensionStack GetDimensionStack(int dimensionIndex)
        {
            return _stacks[dimensionIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(DimensionEnum dimensionEnum, double value)
        {
            Push((int)dimensionEnum, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(int dimensionIndex, double value)
        {
            _stacks[dimensionIndex].Push(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop(DimensionEnum dimensionEnum)
        {
            Pop((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pop(int dimensionIndex)
        {
            _stacks[dimensionIndex].Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double PopAndGet(DimensionEnum dimensionEnum)
        {
            return PopAndGet((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double PopAndGet(int dimensionIndex)
        {
            double value = _stacks[dimensionIndex].PopAndGet();
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get(DimensionEnum dimensionEnum)
        {
            return Get((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Get(int dimensionIndex)
        {
            return _stacks[dimensionIndex].Get();
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operators in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(DimensionEnum dimensionEnum, int dimensionStackIndex, double value)
        {
            Set((int)dimensionEnum, dimensionStackIndex, value);
        }

        /// <summary>
        /// A slightly quicker alternative to a subsequent Pop and Push,
        /// when you know there will not be any stack operators in between,
        /// or when you know you are at the top level of the stack.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(int dimensionIndex, int dimensionStackIndex, double value)
        {
            _stacks[dimensionIndex].Set(dimensionStackIndex, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(DimensionEnum dimensionEnum)
        {
            return Count((int)dimensionEnum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Count(int dimensionIndex)
        {
            int count = _stacks[dimensionIndex].Count;
            return count;
        }
    }
}
