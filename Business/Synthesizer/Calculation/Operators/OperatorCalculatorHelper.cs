using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class OperatorCalculatorHelper
    {
        public const double DEFAULT_TIME = 0.0;
        public const int DEFAULT_CHANNEL_INDEX = 0;

        public static void AssertIsNotSpecialNumber(double value, Expression<Func<object>> expression)
        {
            if (double.IsNaN(value)) throw new NaNException(expression);
            if (double.IsInfinity(value)) throw new InfinityException(expression);
        }

        public static void AssertFrequency(double frequency)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (double.IsNaN(frequency)) throw new NaNException(() => frequency);
            if (double.IsInfinity(frequency)) throw new InfinityException(() => frequency);
        }

        public static void AssertWidth(double width)
        {
            if (width == 0.0) throw new ZeroException(() => width);
            if (width >= 1.0) throw new GreaterThanOrEqualException(() => width, 1.0);
            if (double.IsNaN(width)) throw new NaNException(() => width);
            if (double.IsInfinity(width)) throw new InfinityException(() => width);
        }

        public static void AssertDimensionStack(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (dimensionStack.CurrentIndex < 0) throw new LessThanException(() => dimensionStack.CurrentIndex, 0);
        }

        public static void AssertDimensionEnum(DimensionEnum dimensionEnum)
        {
            if (!EnumHelper.IsValidEnum(dimensionEnum)) throw new InvalidValueException(dimensionEnum);
            if (dimensionEnum == DimensionEnum.Undefined) throw new ValueNotSupportedException(dimensionEnum);
        }

        public static void AssertRoundOffset(double offset)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (offset % 1.0 == 0.0) throw new Exception($"{nameof(offset)} cannot be a multple of 1.");
            if (double.IsNaN(offset)) throw new NaNException(() => offset);
            if (double.IsInfinity(offset)) throw new InfinityException(() => offset);
        }

        public static void AssertRoundStep(double step)
        {
            if (step == 0.0) throw new ZeroException(() => step);
            if (double.IsNaN(step)) throw new NaNException(() => step);
            if (double.IsInfinity(step)) throw new InfinityException(() => step);
        }

        public static void AssertFilterFrequency(double filterFrequency, double targetSamplingRate)
        {
            if (filterFrequency == 0.0) throw new ZeroException(() => filterFrequency);
            if (double.IsNaN(filterFrequency)) throw new NaNException(() => filterFrequency);
            if (double.IsInfinity(filterFrequency)) throw new InfinityException(() => filterFrequency);

            if (targetSamplingRate == 0.0) throw new ZeroException(() => targetSamplingRate);
            if (double.IsNaN(targetSamplingRate)) throw new NaNException(() => targetSamplingRate);
            if (double.IsInfinity(targetSamplingRate)) throw new InfinityException(() => targetSamplingRate);

            double nyquistFrequency = targetSamplingRate / 2.0;
            if (filterFrequency > nyquistFrequency)
            {
                throw new GreaterThanException(() => filterFrequency, () => nyquistFrequency);
            }
        }

        public static void AssertFactor(double factor)
        {
            if (factor == 0.0) throw new ZeroException(() => factor);
            if (factor == 1.0) throw new EqualException(() => factor, 1);
            if (double.IsNaN(factor)) throw new NaNException(() => factor);
            if (double.IsInfinity(factor)) throw new InfinityException(() => factor);
        }

        public static void AssertReverseFactor(double factor)
        {
            if (factor == 0.0) throw new ZeroException(() => factor);
            if (double.IsNaN(factor)) throw new NaNException(() => factor);
            if (double.IsInfinity(factor)) throw new InfinityException(() => factor);
        }

        /// <summary> Asserts that the calculator is not null and not a Number_OperatorCalculator.</summary>
        public static void AssertChildOperatorCalculator(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
            if (operatorCalculatorBase is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(expression);
        }

        public static void AssertChildOperatorCalculator_OnlyUsedUponResetState(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertStackIndex(DimensionStack dimensionStack, int dimensionStackIndex)
        {
            if (dimensionStack.CurrentIndex != dimensionStackIndex)
            {
                throw new NotEqualException(() => dimensionStack.CurrentIndex, () => dimensionStackIndex);
                //string message = String.Format("dimensionStack.CurrentIndex was expected to be '{0}' but is actually '{1}'.", dimensionStackIndex, dimensionStack.CurrentIndex);
                //Debug.WriteLine(message);
                //Debug.WriteLine(Environment.StackTrace);
            }
        }
    }
}