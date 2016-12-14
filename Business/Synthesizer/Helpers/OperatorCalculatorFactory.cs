using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary>
    /// For when creating an OperatorCalculator based on criteria is used
    /// in more places than just the OptimizedPatchCalculatorVisitor.
    /// </summary>
    internal static partial class OperatorCalculatorFactory
    {
        public static OperatorCalculatorBase Create_Interpolate_OperatorCalculator(
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
                    calculator = new Interpolate_OperatorCalculator_Stripe_LagBehind(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Line:
                    bool samplingRateIsConst = samplingRateCalculator is Number_OperatorCalculator;
                    if (samplingRateIsConst)
                    {
                        double samplingRate = samplingRateCalculator.Calculate();
                        calculator = new Interpolate_OperatorCalculator_Line_LagBehind_ConstSamplingRate(signalCalculator, samplingRate, dimensionStack);
                    }
                    else
                    {
                        calculator = new Interpolate_OperatorCalculator_Line_LagBehind_VarSamplingRate(signalCalculator, samplingRateCalculator, dimensionStack);
                    }
                    break;

                case ResampleInterpolationTypeEnum.CubicEquidistant:
                    calculator = new Interpolate_OperatorCalculator_CubicEquidistant(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicAbruptSlope:
                    calculator = new Interpolate_OperatorCalculator_CubicAbruptSlope(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.CubicSmoothSlope:
                    calculator = new Interpolate_OperatorCalculator_CubicSmoothSlope_LagBehind(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                case ResampleInterpolationTypeEnum.Hermite:
                    calculator = new Interpolate_OperatorCalculator_Hermite_LagBehind(signalCalculator, samplingRateCalculator, dimensionStack);
                    break;

                default:
                    throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
            }

            return calculator;
        }

        public static OperatorCalculatorBase Create_Cache_OperatorCalculator(
            IList<ArrayCalculatorBase> arrayCalculators,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (arrayCalculators.Count == 1)
            {
                ArrayCalculatorBase arrayCalculator = arrayCalculators[0];
                OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_SingleChannel(arrayCalculator, dimensionStack);
                return calculator;
            }
            else
            {
                OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_MultiChannel(arrayCalculators, dimensionStack, channelDimensionStack);
                return calculator;
            }
        }

        // The array calculators really dictate which Cache_OperatorCalculator to instantiate.
        // We need the array calculator as a concrete type argument 
        // to prevent some indirections imposed by calling an abstract type.

        public static OperatorCalculatorBase Create_Cache_OperatorCalculator_MultiChannel(IList<ArrayCalculatorBase> arrayCalculators, DimensionStack dimensionStack, DimensionStack channelDimensionStack)
        {
            Type arrayCalculatorType = arrayCalculators.GetItemType();
            Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_MultiChannel<>).MakeGenericType(new Type[] { arrayCalculatorType });
            
            var calculator = (OperatorCalculatorBase)Activator.CreateInstance(cache_OperatorCalculator_Type, arrayCalculators, dimensionStack, channelDimensionStack);
            return calculator;
        }

        public static OperatorCalculatorBase Create_Cache_OperatorCalculator_SingleChannel(ArrayCalculatorBase arrayCalculator, DimensionStack dimensionStack)
        {
            Type arrayCalculatorType = arrayCalculator.GetType();
            Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_SingleChannel<>).MakeGenericType(new Type[] { arrayCalculatorType });
            var calculator = (OperatorCalculatorBase)Activator.CreateInstance(cache_OperatorCalculator_Type, arrayCalculator, dimensionStack);
            return calculator;
        }
    }
}