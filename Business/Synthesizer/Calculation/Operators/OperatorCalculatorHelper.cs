﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
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

        public static void AssertFilterFrequency(double filterFrequency, double samplingRate)
        {
            if (filterFrequency == 0.0) throw new ZeroException(() => filterFrequency);
            if (Double.IsNaN(filterFrequency)) throw new NaNException(() => filterFrequency);
            if (Double.IsInfinity(filterFrequency)) throw new InfinityException(() => filterFrequency);

            if (samplingRate == 0.0) throw new ZeroException(() => samplingRate);
            if (Double.IsNaN(samplingRate)) throw new NaNException(() => samplingRate);
            if (Double.IsInfinity(samplingRate)) throw new InfinityException(() => samplingRate);

            double nyquistFrequency = samplingRate / 2.0;
            if (filterFrequency > nyquistFrequency)
            {
                throw new GreaterThanException(() => filterFrequency, () => nyquistFrequency);
            }
        }

        public static void AssertFactor(double factor)
        {
            if (factor == 0) throw new ZeroException(() => factor);
            if (factor == 1) throw new ZeroException(() => factor);
            if (Double.IsNaN(factor)) throw new NaNException(() => factor);
            if (Double.IsInfinity(factor)) throw new InfinityException(() => factor);
        }

        public static void AssertReverseSpeed(double speed)
        {
            if (speed == 0) throw new ZeroException(() => speed);
            if (Double.IsNaN(speed)) throw new NaNException(() => speed);
            if (Double.IsInfinity(speed)) throw new InfinityException(() => speed);
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