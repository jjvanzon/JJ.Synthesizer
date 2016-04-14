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
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factor,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factor == 0) throw new ZeroException(() => factor);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factor = factor;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = position * _factor;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;

        }
    }

    // Var-Var-Zero

    internal class Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = position * factor;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

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
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factorValue, 
            double originValue,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (originValue == 0) throw new ZeroException(() => originValue);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originValue = originValue;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = (position - _originValue) * _factorValue + _originValue;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    // Var-Const-Var

    internal class Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            OperatorCalculatorBase originCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originCalculator = originCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(dimensionStack);

            double transformedPosition = (position - origin) * _factorValue + origin;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result2 = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result2;
        }
    }

    // Var-Var-Const

    internal class Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _origin;
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double origin,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _origin = origin;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = (position - _origin) * factor + _origin;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    // Var-Var-Var

    internal class Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly int _dimensionIndex;

        public Narrower_VarSignal_VarFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator, 
            OperatorCalculatorBase originCalculator,
            DimensionEnum dimensionEnum)
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
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double factor = _factorCalculator.Calculate(dimensionStack);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate(dimensionStack);

            double transformedPosition = (position - origin) * factor + origin;

            dimensionStack.Push(_dimensionIndex, transformedPosition);

            double result = _signalCalculator.Calculate(dimensionStack);

            dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }
}
