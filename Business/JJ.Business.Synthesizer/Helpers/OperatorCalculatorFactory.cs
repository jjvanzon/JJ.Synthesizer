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
    internal class OperatorCalculatorFactory
    {
        public static OperatorCalculatorBase CreateResample_OperatorCalculator(
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
        {
            OperatorCalculatorBase calculator;
            switch (resampleInterpolationTypeEnum)
            {
                case ResampleInterpolationTypeEnum.Block:
                    calculator = new Resample_OperatorCalculator_Block(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Stripe:
                    calculator = new Resample_OperatorCalculator_Stripe(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.LineRememberT0:
                    calculator = new Resample_OperatorCalculator_LineRememberT0(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.LineRememberT1:
                    calculator = new Resample_OperatorCalculator_LineRememberT1(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicEquidistant:
                    calculator = new Resample_OperatorCalculator_CubicEquidistant(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                    calculator = new Resample_OperatorCalculator_CubicAbruptSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                    calculator = new Resample_OperatorCalculator_CubicSmoothSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Hermite:
                    calculator = new Resample_OperatorCalculator_Hermite(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                default:
                    throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
            }

            return calculator;
        }
    }
}
