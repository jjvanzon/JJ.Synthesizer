﻿using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class DimensionToOutlets_OperatorCalculator_WithSignalOutput : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _operandCalculator;
        private readonly double _position;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        public DimensionToOutlets_OperatorCalculator_WithSignalOutput(
            OperatorCalculatorBase operandCalculator,
            double position,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(new[] { operandCalculator, positionOutputCalculator })
        {
            _operandCalculator = operandCalculator;
            _position = position;
            _positionOutputCalculator = positionOutputCalculator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            _positionOutputCalculator._value = _position;

            double result = _operandCalculator.Calculate();

            return result;
        }
    }
}