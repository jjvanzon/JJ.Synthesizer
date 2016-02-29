using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal static class ArrayCalculatorFactory
    {
        public static ArrayCalculatorBase CreateArrayCalculator_RotateTime(
            double[] array,
            double rate,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotateTime_Block_NoRate(array);
                    }
                    else
                    {
                        return new ArrayCalculator_RotateTime_Block(array, rate);
                    }

                case InterpolationTypeEnum.Cubic:
                    return new ArrayCalculator_RotateTime_Cubic(array, rate);

                case InterpolationTypeEnum.Hermite:
                    return new ArrayCalculator_RotateTime_Hermite(array, rate);

                case InterpolationTypeEnum.Line:
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotateTime_Line_NoRate(array);
                    }
                    else
                    {
                        return new ArrayCalculator_RotateTime_Line(array, rate);
                    }

                case InterpolationTypeEnum.Stripe:
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotateTime_Stripe_NoRate(array);
                    }
                    else
                    {
                        return new ArrayCalculator_RotateTime_Stripe(array, rate);
                    }

                default:
                    throw new ValueNotSupportedException(interpolationTypeEnum);
            }
        }

        public static ArrayCalculatorBase CreateArrayCalculator(
            double[] array,
            double rate,
            double minTime,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            if (minTime == 0.0)
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTimeZero_Block(array, rate);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinTimeZero_Cubic(array, rate);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTimeZero_Hermite(array, rate);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinTimeZero_Line(array, rate);

                    case InterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTimeZero_Stripe(array, rate);

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
            else
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTime_Block(array, rate, minTime);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinTime_Cubic(array, rate, minTime);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTime_Hermite(array, rate, minTime);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinTime_Line(array, rate, minTime);

                    case InterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTime_Stripe(array, rate, minTime);

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
        }

        public static ArrayCalculatorBase CreateArrayCalculator(
            double[] array,
            double rate,
            double minTime,
            double valueBefore,
            double valueAfter,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            if (minTime == 0.0)
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTimeZero_Block(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinTimeZero_Cubic(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTimeZero_Hermite(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinTimeZero_Line(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTimeZero_Stripe(array, rate, valueBefore, valueAfter);

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
            else
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinTime_Block(array, rate, minTime, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinTime_Cubic(array, rate, minTime, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinTime_Hermite(array, rate, minTime, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinTime_Line(array, rate, minTime, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinTime_Stripe(array, rate, minTime, valueBefore, valueAfter);

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
        }
    }
}
