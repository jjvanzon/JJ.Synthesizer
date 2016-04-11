using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation
{
    internal class DimensionContainer
    {
        /// <summary> Index is the dimension enum member. </summary>
        private Stack<double>[] _stacks;

        public DimensionContainer()
        {
            int dimensionCount = EnumHelper.GetValues<DimensionEnum>().Count;
            _stacks = new Stack<double>[dimensionCount];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(DimensionEnum dimensionEnum, double value)
        {
            _stacks[(int)dimensionEnum].Push(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Pop(DimensionEnum dimensionEnum)
        {
            return _stacks[(int)dimensionEnum].Pop();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Peek(DimensionEnum dimensionEnum)
        {
            return _stacks[(int)dimensionEnum].Peek();
        }
    }
}
