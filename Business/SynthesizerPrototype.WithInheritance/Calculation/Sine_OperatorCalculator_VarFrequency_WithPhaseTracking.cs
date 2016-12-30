using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Sine_OperatorCalculator_VarFrequency_WithPhaseTracking : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly DimensionStack _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sine_OperatorCalculator_VarFrequency_WithPhaseTracking(
            OperatorCalculatorBase frequencyCalculator,
            DimensionStack dimensionStack)
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get();

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;
            double value = SineCalculator.Sin(_phase);

            _previousPosition = position;

            return value;
        }
    }
}
