using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class OperatorCalculatorHelper
    {
        public const double DEFAULT_TIME = 0.0;
        public const int DEFAULT_CHANNEL_INDEX = 0;

        public static void AssertIsNotSpecialNumber(double value, Expression<Func<object>> expression)
        {
            if (Double.IsNaN(value)) throw new NaNException(expression);
            if (Double.IsInfinity(value)) throw new InfinityException(expression);
        }

        public static void AssertFrequency(double frequency)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (Double.IsNaN(frequency)) throw new NaNException(() => frequency);
            if (Double.IsInfinity(frequency)) throw new InfinityException(() => frequency);
        }

        public static void AssertWidth(double width)
        {
            if (width == 0.0) throw new ZeroException(() => width);
            if (width >= 1.0) throw new GreaterThanOrEqualException(() => width, 1.0);
            if (Double.IsNaN(width)) throw new NaNException(() => width);
            if (Double.IsInfinity(width)) throw new InfinityException(() => width);
        }

        // TODO: This variation is not necessary, now it does the same as AssertDimensionStack_ForReaders.
        public static void AssertDimensionStack_ForWriters(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (dimensionStack.CurrentIndex < 0) throw new LessThanException(() => dimensionStack.CurrentIndex, 0);
            //if (dimensionStack.CurrentIndex < 1) throw new Exception("dimensionStack.CurrentIndex cannot be less than 1, because a DimenionStack-writing OperatorCalculator must use the previous DimensionStack index too.");
        }

        public static void AssertDimensionStack_ForReaders(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            if (dimensionStack.CurrentIndex < 0) throw new LessThanException(() => dimensionStack.CurrentIndex, 0);
        }

        public static void AssertPhaseShift(double phaseShift)
        {
            if (phaseShift >= 1.0) throw new GreaterThanOrEqualException(() => phaseShift, 1.0);
            if (Double.IsNaN(phaseShift)) throw new NaNException(() => phaseShift);
            if (Double.IsInfinity(phaseShift)) throw new InfinityException(() => phaseShift);
            if (phaseShift % 1.0 == 0.0) throw new Exception("phaseShift cannot be a multiple of 1.");
        }

        public static void AssertDimensionEnum(DimensionEnum dimensionEnum)
        {
            if (!EnumHelper.IsValidEnum(dimensionEnum)) throw new InvalidValueException(dimensionEnum);
            if (dimensionEnum == DimensionEnum.Undefined) throw new ValueNotSupportedException(dimensionEnum);
        }

        public static void AssertRoundOffset(double offset)
        {
            if (offset % 1.0 == 0.0) throw new Exception(String.Format("{0} cannot be a multple of 1.", ExpressionHelper.GetText(() => offset)));
            if (Double.IsNaN(offset)) throw new NaNException(() => offset);
            if (Double.IsInfinity(offset)) throw new InfinityException(() => offset);
        }

        public static void AssertRoundStep(double step)
        {
            if (step == 0.0) throw new ZeroException(() => step);
            if (Double.IsNaN(step)) throw new NaNException(() => step);
            if (Double.IsInfinity(step)) throw new InfinityException(() => step);
        }

        /// <summary> Asserts that the calculator is not null and a Number_OperatorCalculator.</summary>
        public static void AssertOperatorCalculatorBase(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
            if (operatorCalculatorBase is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(expression);
        }

        public static void AssertOperatorCalculatorBase_OnlyUsedUponResetState(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AssertStackIndex(DimensionStack dimensionStack, int dimensionStackIndex)
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