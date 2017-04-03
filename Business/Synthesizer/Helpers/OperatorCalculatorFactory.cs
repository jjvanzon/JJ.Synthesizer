using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
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
            IList<ICalculatorWithPosition> arrayCalculators,
            DimensionStack dimensionStack,
            DimensionStack channelDimensionStack)
        {
            if (arrayCalculators.Count == 1)
            {
                ICalculatorWithPosition arrayCalculator = arrayCalculators[0];
                OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_SingleChannel(arrayCalculator, dimensionStack);
                return calculator;
            }
            else
            {
                OperatorCalculatorBase calculator = Create_Cache_OperatorCalculator_MultiChannel(arrayCalculators, dimensionStack, channelDimensionStack);
                return calculator;
            }
        }

        public static OperatorCalculatorBase Create_Cache_OperatorCalculator_MultiChannel(IList<ICalculatorWithPosition> arrayCalculators, DimensionStack dimensionStack, DimensionStack channelDimensionStack)
        {
            Type arrayCalculatorType = arrayCalculators.GetItemType();
            Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_MultiChannel<>).MakeGenericType(arrayCalculatorType);
            
            var calculator = (OperatorCalculatorBase)Activator.CreateInstance(cache_OperatorCalculator_Type, arrayCalculators, dimensionStack, channelDimensionStack);
            return calculator;
        }

        public static OperatorCalculatorBase Create_Cache_OperatorCalculator_SingleChannel(ICalculatorWithPosition arrayCalculator, DimensionStack dimensionStack)
        {
            Type arrayCalculatorType = arrayCalculator.GetType();
            Type cache_OperatorCalculator_Type = typeof(Cache_OperatorCalculator_SingleChannel<>).MakeGenericType(arrayCalculatorType);
            var calculator = (OperatorCalculatorBase)Activator.CreateInstance(cache_OperatorCalculator_Type, arrayCalculator, dimensionStack);
            return calculator;
        }

        public static OperatorCalculatorBase Create_Curve_OperatorCalculator(
            [NotNull] Operator op,
            [NotNull] DimensionStackCollection dimensionStackCollection,
            [NotNull] CalculatorCache calculatorCache,
            [NotNull] ICurveRepository curveRepository)
        {
            if (op == null) throw new NullException(() => op);

            var wrapper = new Curve_OperatorWrapper(op, curveRepository);
            Curve curve = wrapper.Curve;
            ArrayDto arrayDto = calculatorCache.GetCurveArrayDto(curve);

            OperatorCalculatorBase calculator = Create_Curve_OperatorCalculator(arrayDto, op.GetStandardDimensionEnum(), dimensionStackCollection);

            return calculator;
        }

        public static OperatorCalculatorBase Create_Curve_OperatorCalculator(
            [NotNull] Curve_OperatorDtoBase_WithoutMinX dto,
            [NotNull] DimensionStackCollection dimensionStackCollection)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            OperatorCalculatorBase calculator = Create_Curve_OperatorCalculator(dto.ArrayDto, dto.StandardDimensionEnum, dimensionStackCollection);

            return calculator;
        }

        /// <param name="arrayDto">nullable</param>
        public static OperatorCalculatorBase Create_Curve_OperatorCalculator(
            [CanBeNull] ArrayDto arrayDto,
            DimensionEnum standardDimensionEnum,
            [NotNull] DimensionStackCollection dimensionStackCollection)
        {
            if (dimensionStackCollection == null) throw new ArgumentNullException(nameof(dimensionStackCollection));

            if (arrayDto == null)
            {
                return new Number_OperatorCalculator_Zero();
            }

            DimensionStack dimensionStack = dimensionStackCollection.GetDimensionStack(standardDimensionEnum);

            ICalculatorWithPosition arrayCalculator = ArrayCalculatorFactory.CreateArrayCalculator(arrayDto);

            var arrayCalculator_MinPosition = arrayCalculator as ArrayCalculator_MinPosition_Line;
            if (arrayCalculator_MinPosition != null)
            {
                if (standardDimensionEnum == DimensionEnum.Time)
                {
                    return new Curve_OperatorCalculator_MinX_WithOriginShifting(arrayCalculator_MinPosition, dimensionStack);
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    return new Curve_OperatorCalculator_MinX_NoOriginShifting(arrayCalculator_MinPosition, dimensionStack);
                }
            }

            var arrayCalculator_MinPositionZero = arrayCalculator as ArrayCalculator_MinPositionZero_Line;
            if (arrayCalculator_MinPositionZero != null)
            {
                if (standardDimensionEnum == DimensionEnum.Time)
                {
                    return new Curve_OperatorCalculator_MinXZero_WithOriginShifting(arrayCalculator_MinPositionZero, dimensionStack);
                }
                // ReSharper disable once RedundantIfElseBlock
                else
                {
                    return new Curve_OperatorCalculator_MinXZero_NoOriginShifting(arrayCalculator_MinPositionZero, dimensionStack);
                }
            }

            throw new CalculatorNotFoundException(MethodBase.GetCurrentMethod());
        }
    }
}