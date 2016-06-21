using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Multiply_OperatorCalculator_WithOperandArray_WithConst : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _constValue;
        private readonly OperatorCalculatorBase[] _varOperandCalculators;

        public Multiply_OperatorCalculator_WithOperandArray_WithConst(double constValue, IList<OperatorCalculatorBase> varOperandCalculators)
            : base(varOperandCalculators)
        {
            if (varOperandCalculators == null) throw new NullException(() => varOperandCalculators);

            _constValue = constValue;

            _varOperandCalculators = varOperandCalculators.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double product = _constValue;

            for (int i = 0; i < _varOperandCalculators.Length; i++)
            {
                double value = _varOperandCalculators[i].Calculate();

                product *= value;
            }

            return product;
        }
    }
}