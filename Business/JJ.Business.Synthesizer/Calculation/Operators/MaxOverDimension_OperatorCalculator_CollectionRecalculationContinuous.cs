using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous
        : OperatorCalculatorBase_SamplerOverDimension
    {
        private double _aggregate;

        public MaxOverDimension_OperatorCalculator_CollectionRecalculationContinuous(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            DimensionStack dimensionStack)
            : base(signalCalculator, fromCalculator, tillCalculator, stepCalculator, dimensionStack)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            RecalculateCollection();

            return _aggregate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ProcessFirstSample(double sample)
        {
            _aggregate = sample;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void ProcessNextSample(double sample)
        {
            if (sample > _aggregate)
            {
                _aggregate = sample;
            }
        }
    }
}
