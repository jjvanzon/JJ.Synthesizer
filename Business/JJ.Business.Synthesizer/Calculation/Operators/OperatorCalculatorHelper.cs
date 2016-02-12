using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal static class OperatorCalculatorHelper
    {
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

        public static void AssertPhaseShift(double phaseShift)
        {
            if (phaseShift >= 1.0) throw new GreaterThanOrEqualException(() => phaseShift, 1.0);
            if (Double.IsNaN(phaseShift)) throw new NaNException(() => phaseShift);
            if (Double.IsInfinity(phaseShift)) throw new InfinityException(() => phaseShift);
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

        public static void AssertOperatorCalculatorBase(
            OperatorCalculatorBase operatorCalculatorBase,
            Expression<Func<object>> expression)
        {
            if (operatorCalculatorBase == null) throw new NullException(expression);
            if (operatorCalculatorBase is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(expression);
        }
    }
}