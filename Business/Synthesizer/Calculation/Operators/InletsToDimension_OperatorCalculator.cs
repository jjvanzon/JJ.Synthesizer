// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // TODO: Also add block and stripe interpolation.

    internal class InletsToDimension_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _interpolateOperator;

        public InletsToDimension_OperatorCalculator(
            IList<OperatorCalculatorBase> operandCalculators,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
            DimensionStack dimensionStack)
            : base(operandCalculators)
        {
            // HACK in a piece of patch, 
            // to reuse the behavior of Unbundle
            // and to reuse the Interpolate_OperatorCalculator's capability of
            // handling many types of interpolation.

            var bundleCalculator = new Bundle_OperatorCalculator(operandCalculators, dimensionStack);

            _interpolateOperator = OperatorCalculatorFactory.CreateInterpolate_OperatorCalculator(
                resampleInterpolationTypeEnum,
                bundleCalculator,
                new One_OperatorCalculator(),
                dimensionStack);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            return _interpolateOperator.Calculate();
        }

        public override void Reset()
        {
            // HACK
            _interpolateOperator.Reset();
        }
    }
}