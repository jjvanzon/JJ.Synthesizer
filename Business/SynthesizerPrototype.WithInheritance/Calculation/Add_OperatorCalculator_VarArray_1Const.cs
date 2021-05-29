using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Add_OperatorCalculator_VarArray_1Const : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase[] _varOperandCalculators;
        private readonly int _varOperandCalculatorsCount;
        private readonly double _constValue;

        public Add_OperatorCalculator_VarArray_1Const(IList<OperatorCalculatorBase> varOperandCalculators, double constValue)
        {
            _varOperandCalculators = varOperandCalculators?.ToArray() ?? throw new NullException(() => varOperandCalculators);
            _varOperandCalculatorsCount = _varOperandCalculators.Length;

            _constValue = constValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double sum = _constValue;

            for (int i = 0; i < _varOperandCalculatorsCount; i++)
            {
                double value = _varOperandCalculators[i].Calculate();

                sum += value;
            }

            return sum;
        }
    }
}