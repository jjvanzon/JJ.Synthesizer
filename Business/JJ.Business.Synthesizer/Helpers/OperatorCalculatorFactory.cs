using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary>
    /// For when creating an OperatorCalculator based on criteria is used
    /// in more places than just the OptimizedPatchCalculatorVisitor.
    /// </summary>
    internal static partial class OperatorCalculatorFactory
    {
        public static OperatorCalculatorBase CreateInterpolate_OperatorCalculator(
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorBase calculator;
            switch (resampleInterpolationTypeEnum)
            {
                case ResampleInterpolationTypeEnum.Block:
                    calculator = new Interpolate_OperatorCalculator_Block(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Stripe:
                    calculator = new Interpolate_OperatorCalculator_Stripe(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.LineRememberT0:
                    calculator = new Interpolate_OperatorCalculator_LineRememberX0(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.LineRememberT1:
                    calculator = new Interpolate_OperatorCalculator_LineRememberX1(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicEquidistant:
                    calculator = new Interpolate_OperatorCalculator_CubicEquidistant(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                    calculator = new Interpolate_OperatorCalculator_CubicAbruptSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                    calculator = new Interpolate_OperatorCalculator_CubicSmoothSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Hermite:
                    calculator = new Interpolate_OperatorCalculator_Hermite(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                default:
                    throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
            }

            return calculator;
        }
    }
}
