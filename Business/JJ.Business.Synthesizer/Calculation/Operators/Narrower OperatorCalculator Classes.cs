using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Const-Const-Zero does not exist.

    // Const-Var-Zero does not exist.

    // Var-Const-Zero

    internal class Narrower_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;

        public Narrower_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);

            _signalCalculator = signalCalculator;
            _factor = factorValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * _factor;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;

        }
    }

    // Var-Var-Zero

    internal class Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;

        public Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = time * factor;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    // Const-Const-Const does not exist.

    // Const-Const-Var does not exist.

    // Const-Var-Const does not exist.

    // Const-Var-Var does not exist.

    // Var-Const-Const

    internal class Narrower_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly double _originValue;

        public Narrower_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factorValue, 
            double originValue)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
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
            double transformedTime = (time - _originValue) * _factorValue + _originValue;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    // Var-Const-Var

    internal class Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly OperatorCalculatorBase _originCalculator;

        public Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(dimensionStack);

            double transformedTime = (time - origin) * _factorValue + origin;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result2 = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result2;
        }
    }

    // Var-Var-Const

    internal class Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _originValue;

        public Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double originValue)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originValue = originValue;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedTime = (time - _originValue) * factor + _originValue;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }

    // Var-Var-Var

    internal class Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;

        public Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator, 
            OperatorCalculatorBase originCalculator)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double time = dimensionStack.Get(DimensionEnum.Time);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(dimensionStack);

            double transformedTime = (time - origin) * factor + origin;

            dimensionStack.Push(DimensionEnum.Time, transformedTime);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(DimensionEnum.Time);

            return result;
        }
    }
}
