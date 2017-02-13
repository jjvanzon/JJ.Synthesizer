using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Arrays
{
    internal static class ArrayCalculatorFactory
    {
        /// <param name="isRotatingPosition">Recently added to make this method a little more flexibly usable. It is optional for backwards compatibility.</param>
        public static ArrayCalculatorBase CreateArrayCalculator(
            double[] array,
            double rate,
            double minPosition,
            double valueBefore,
            double valueAfter,
            InterpolationTypeEnum interpolationTypeEnum,
            bool isRotatingPosition = false)
        {
            if (isRotatingPosition)
            {
                return CreateArrayCalculator_RotatePosition(array, rate, interpolationTypeEnum);
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (minPosition == 0.0)
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinPositionZero_Block(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinPositionZero_Cubic(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinPositionZero_Hermite(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinPositionZero_Line(array, rate, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Stripe:
                        // ReSharper disable once CompareOfFloatsByEqualityOperator
                        if (rate == 1.0)
                        {
                            return new ArrayCalculator_MinPositionZero_Stripe_NoRate(array, valueBefore, valueAfter);
                        }
                        // ReSharper disable once RedundantIfElseBlock
                        else
                        {
                            return new ArrayCalculator_MinPositionZero_Stripe(array, rate, valueBefore, valueAfter);
                        }

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
            // ReSharper disable once RedundantIfElseBlock
            else
            {
                switch (interpolationTypeEnum)
                {
                    case InterpolationTypeEnum.Block:
                        return new ArrayCalculator_MinPosition_Block(array, rate, minPosition, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Cubic:
                        return new ArrayCalculator_MinPosition_Cubic(array, rate, minPosition, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Hermite:
                        return new ArrayCalculator_MinPosition_Hermite(array, rate, minPosition, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Line:
                        return new ArrayCalculator_MinPosition_Line(array, rate, minPosition, valueBefore, valueAfter);

                    case InterpolationTypeEnum.Stripe:
                        return new ArrayCalculator_MinPosition_Stripe(array, rate, minPosition, valueBefore, valueAfter);

                    default:
                        throw new ValueNotSupportedException(interpolationTypeEnum);
                }
            }
        }

        public static ArrayCalculatorBase CreateArrayCalculator_RotatePosition(
            double[] array,
            double rate,
            InterpolationTypeEnum interpolationTypeEnum)
        {
            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotatePosition_Block_NoRate(array);
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        return new ArrayCalculator_RotatePosition_Block(array, rate);
                    }

                case InterpolationTypeEnum.Cubic:
                    return new ArrayCalculator_RotatePosition_Cubic(array, rate);

                case InterpolationTypeEnum.Hermite:
                    return new ArrayCalculator_RotatePosition_Hermite(array, rate);

                case InterpolationTypeEnum.Line:
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotatePosition_Line_NoRate(array);
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        return new ArrayCalculator_RotatePosition_Line(array, rate);
                    }

                case InterpolationTypeEnum.Stripe:
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (rate == 1.0)
                    {
                        return new ArrayCalculator_RotatePosition_Stripe_NoRate(array);
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        return new ArrayCalculator_RotatePosition_Stripe(array, rate);
                    }

                default:
                    throw new ValueNotSupportedException(interpolationTypeEnum);
            }
        }
    }
}
