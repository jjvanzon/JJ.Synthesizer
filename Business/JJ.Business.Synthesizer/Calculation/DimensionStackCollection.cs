using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation
{
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
            return _stacks[(int)dimensionEnum];
        }
    }
}
