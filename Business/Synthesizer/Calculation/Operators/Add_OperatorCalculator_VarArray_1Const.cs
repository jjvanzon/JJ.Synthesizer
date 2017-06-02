using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Add_OperatorCalculator_VarArray_1Const : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase[] _varItemCalculators;
        private readonly int _varItemCalculatorsCount;

        private readonly double _constValue;

        public Add_OperatorCalculator_VarArray_1Const(IList<OperatorCalculatorBase> varItemCalculators, double constValue)
            : base(varItemCalculators)
        {
            _varItemCalculators = varItemCalculators?.ToArray() ?? throw new NullException(() => varItemCalculators);
            _varItemCalculatorsCount = _varItemCalculators.Length;
            _constValue = constValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double sum = _constValue;

            for (int i = 0; i < _varItemCalculatorsCount; i++)
            {
                double value = _varItemCalculators[i].Calculate();

                sum += value;
            }

            return sum;
        }
    }
}