//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Framework.Common;

//namespace JJ.Business.Synthesizer.Calculation
//{
//    /// <summary>
//    /// This class maintains for each dimension a stack of values 
//    /// and tries to make access to these values as fast as possible.
//    /// </summary>
//    internal class DimensionStackCollection
//    {
//        private static int _dimensionCount = GetDimensionCount();

//        /// <summary> Array index is the dimension enum member. </summary>
//        private DimensionStack[] _stacks;

//        private static int GetDimensionCount()
//        {
//            int dimensionEnumMaxValue = EnumHelper.GetValues<DimensionEnum>()
//                                                  .Select(x => (int)x)
//                                                  .Max();

//            int dimensionCount = dimensionEnumMaxValue + 1;
//            return dimensionCount;
//        }

//        public DimensionStackCollection()
//        {
//            _stacks = new DimensionStack[_dimensionCount];

//            for (int i = 0; i < _dimensionCount; i++)
//            {
//                var stack = new DimensionStack();

//                // Initialize stack with one item in it, so Peek immediately works to return 0.0.
//                stack.Push(0.0);

//                _stacks[i] = stack;
//            }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public DimensionStack GetDimensionStack(DimensionEnum dimensionEnum)
//        {
//            return _stacks[(int)dimensionEnum];
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Push(DimensionEnum dimensionEnum, double value)
//        {
//            GetDimensionStack(dimensionEnum).Push(value);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Pop(DimensionEnum dimensionEnum)
//        {
//            GetDimensionStack(dimensionEnum).Pop();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double PopAndGet(DimensionEnum dimensionEnum)
//        {
//            double value = GetDimensionStack(dimensionEnum).PopAndGet();
//            return value;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public double Get(DimensionEnum dimensionEnum)
//        {
//            return GetDimensionStack(dimensionEnum).Get();
//        }

//        /// <summary>
//        /// A slightly quicker alternative to a subsequent Pop and Push,
//        /// when you know there will not be any stack operators in between,
//        /// or when you know you are at the top level of the stack.
//        /// </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Set(DimensionEnum dimensionEnum, double value)
//        {
//            GetDimensionStack(dimensionEnum).Set(value);
//        }

//        /// <summary>
//        /// A slightly quicker alternative to a subsequent Pop and Push,
//        /// when you know there will not be any stack operators in between,
//        /// or when you know you are at the top level of the stack.
//        /// </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public void Set(DimensionEnum dimensionEnum, int i, double value)
//        {
//            GetDimensionStack(dimensionEnum).Set(i, value);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public int Count(DimensionEnum dimensionEnum)
//        {
//            int count = GetDimensionStack(dimensionEnum).Count;
//            return count;
//        }
//    }
//}
