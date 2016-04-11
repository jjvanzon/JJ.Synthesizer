using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Const-Const-Zero does not exist.

    // Const-Var-Zero does not exist.

    // Var-Const-Zero

    internal class Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _factorValue;

        public Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time / _factorValue;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    // Var-Var-Zero

    internal class Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _factorCalculator;

        public Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase factorCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorCalculator == null) throw new NullException(() => factorCalculator);
            if (factorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double factor = _factorCalculator.Calculate(dimensionStack);
            if (factor == 0)
            {
                double result = _signalCalculator.Calculate(dimensionStack);
                return result;
            }
            else
            {
                double time = dimensionStack.Get(DimensionEnum.Time);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedTime = time / factor;

                dimensionStack.Push(DimensionEnum.Time, transformedTime);
                double result = _signalCalculator.Calculate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                return result;
            }
        }
    }

    // Const-Const-Const does not exist.

    // Const-Const-Var does not exist.

    // Const-Var-Const does not exist.

    // Const-Var-Var does not exist.

    // Var-Const-Const

    internal class Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _factorValue;
        private double _originValue;

        public Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factorValue, 
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (originValue == 0) throw new ZeroException(() => originValue);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originValue = originValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = (time - _originValue) / _factorValue + _originValue;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    // Var-Const-Var

    internal class Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private double _factorValue;
        private OperatorCalculatorBase _originCalculator;

        public Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            OperatorCalculatorBase originCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (originCalculator == null) throw new NullException(() => originCalculator);
            if (originCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originCalculator);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(dimensionStack);

            double transformedTime = (time - origin) / _factorValue + origin;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);
            double result2 = _signalCalculator.Calculate(dimensionStack);
            dimensionStack.Pop(DimensionEnum.Time);

            return result2;
        }
    }

    // Var-Var-Const

    internal class Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _factorCalculator;
        private double _originValue;

        public Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double originValue)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorCalculator == null) throw new NullException(() => factorCalculator);
            if (factorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originValue = originValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double factor = _factorCalculator.Calculate(dimensionStack);

            if (factor == 0)
            {
                double result = _signalCalculator.Calculate(dimensionStack);
                return result;
            }
            else
            {
                double time = dimensionStack.Get(DimensionEnum.Time);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedTime = (time - _originValue) / factor + _originValue;

                dimensionStack.Push(DimensionEnum.Time, transformedTime);
                double result = _signalCalculator.Calculate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                return result;
            }
        }
    }

    // Var-Var-Var

    internal class Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase
    {
        private OperatorCalculatorBase _signalCalculator;
        private OperatorCalculatorBase _factorCalculator;
        private OperatorCalculatorBase _originOutletCalculator;

        public Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase factorCalculator, OperatorCalculatorBase originOutletCalculator)
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (factorCalculator == null) throw new NullException(() => factorCalculator);
            if (factorCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => factorCalculator);
            if (originOutletCalculator == null) throw new NullException(() => originOutletCalculator);
            if (originOutletCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => originOutletCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originOutletCalculator = originOutletCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double factor = _factorCalculator.Calculate(dimensionStack);
            if (factor == 0)
            {
                double result = _signalCalculator.Calculate(dimensionStack);
                return result;
            }
            else
            {
                double time = dimensionStack.Get(DimensionEnum.Time);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double origin = _originOutletCalculator.Calculate(dimensionStack);
                double transformedTime = (time - origin) / factor + origin;

                dimensionStack.Push(DimensionEnum.Time, transformedTime);
                double result = _signalCalculator.Calculate(dimensionStack);
                dimensionStack.Pop(DimensionEnum.Time);

                return result;
            }
        }
    }
}
