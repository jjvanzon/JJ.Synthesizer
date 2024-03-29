﻿using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset 
        : OperatorCalculatorBase_SamplerOverDimension
    {
        private double _aggregate;

        public MinOverDimension_OperatorCalculator_CollectionRecalculationUponReset(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, positionInputCalculator, positionOutputCalculator)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ResetNonRecursive() => RecalculateCollection();

        /// <summary> Just returns _aggregate. </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate() => _aggregate;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ProcessFirstSample(double sample) => _aggregate = sample;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ProcessNextSample(double sample)
        {
            if (sample < _aggregate)
            {
                _aggregate = sample;
            }
        }
    }
}
