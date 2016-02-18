using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal static class ArrayCalculatorFactory
    {
        public static ArrayCalculatorBase CreateArrayCalculator_RotateTime(
            double[] array,
            double rate,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
        {
            switch (resampleInterpolationTypeEnum)
            {
                case ResampleInterpolationTypeEnum.Block:
                    return new ArrayCalculator_RotateTime_Block(array, rate);

                case ResampleInterpolationTypeEnum.CubicAbruptInclination:
                case ResampleInterpolationTypeEnum.CubicEquidistant:
                case ResampleInterpolationTypeEnum.CubicSmoothInclination:
                    return new ArrayCalculator_RotateTime_Cubic(array, rate);

                case ResampleInterpolationTypeEnum.Hermite:
                    return new ArrayCalculator_RotateTime_Hermite(array, rate);

                case ResampleInterpolationTypeEnum.LineRememberT0:
                case ResampleInterpolationTypeEnum.LineRememberT1:
                    return new ArrayCalculator_RotateTime_Line(array, rate);

                case ResampleInterpolationTypeEnum.Stripe:
                    return new ArrayCalculator_RotateTime_Stripe(array, rate);

                default:
                    throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
            }
        }

        public static ArrayCalculatorBase CreateArrayCalculator(
            double[] array,
            double rate,
            double minTime,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
        {
            if (minTime == 0.0)
            {
                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTimeZero_Block(array, rate);

                    case ResampleInterpolationTypeEnum.CubicAbruptInclination:
                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                    case ResampleInterpolationTypeEnum.CubicSmoothInclination:
                        return new ArrayCalculator_MinTimeZero_Cubic(array, rate);

                    case ResampleInterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTimeZero_Hermite(array, rate);

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        return new ArrayCalculator_MinTimeZero_Line(array, rate);

                    case ResampleInterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTimeZero_Stripe(array, rate);

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }
            else
            {
                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTime_Block(array, rate, minTime);

                    case ResampleInterpolationTypeEnum.CubicAbruptInclination:
                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                    case ResampleInterpolationTypeEnum.CubicSmoothInclination:
                        return new ArrayCalculator_MinTime_Cubic(array, rate, minTime);

                    case ResampleInterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTime_Hermite(array, rate, minTime);

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        return new ArrayCalculator_MinTime_Line(array, rate, minTime);

                    case ResampleInterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTime_Stripe(array, rate, minTime);

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }
        }

        public static ArrayCalculatorBase CreateArrayCalculator(
            double[] array,
            double rate,
            double minTime,
            double valueBefore,
            double valueAfter,
            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
        {
            if (minTime == 0.0)
            {
                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTimeZero_Block(array, rate, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.CubicAbruptInclination:
                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                    case ResampleInterpolationTypeEnum.CubicSmoothInclination:
                        return new ArrayCalculator_MinTimeZero_Cubic(array, rate, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTimeZero_Hermite(array, rate, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        return new ArrayCalculator_MinTimeZero_Line(array, rate, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTimeZero_Stripe(array, rate, valueBefore, valueAfter);

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }
            else
            {
                switch (resampleInterpolationTypeEnum)
                {
                    case ResampleInterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTime_Block(array, rate, minTime, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.CubicAbruptInclination:
                    case ResampleInterpolationTypeEnum.CubicEquidistant:
                    case ResampleInterpolationTypeEnum.CubicSmoothInclination:
                        return new ArrayCalculator_MinTime_Cubic(array, rate, minTime, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTime_Hermite(array, rate, minTime, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.LineRememberT0:
                    case ResampleInterpolationTypeEnum.LineRememberT1:
                        return new ArrayCalculator_MinTime_Line(array, rate, minTime, valueBefore, valueAfter);

                    case ResampleInterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTime_Stripe(array, rate, minTime, valueBefore, valueAfter);

                    default:
                        throw new ValueNotSupportedException(resampleInterpolationTypeEnum);
                }
            }
        }
    }
}
