using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    // Const-Const-Zero does not exist.

    // Const-Var-Zero does not exist.

    // Var-Const-Zero

    internal class Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double dimensionValue = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedDimensionValue = dimensionValue / _factorValue;

            _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    // Var-Var-Zero

    internal class Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double factor = _factorCalculator.Calculate();
            if (factor == 0)
            {
                double result = _signalCalculator.Calculate();
                return result;
            }
            else
            {
                double dimensionValue = _dimensionStack.Get(_dimensionIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedDimensionValue = dimensionValue / factor;

                _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);
                double result = _signalCalculator.Calculate();
                _dimensionStack.Pop(_dimensionIndex);

                return result;
            }
        }
    }

    // Const-Const-Const does not exist.

    // Const-Const-Var does not exist.

    // Const-Var-Const does not exist.

    // Const-Var-Var does not exist.

    // Var-Const-Const

    internal class Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly double _originValue;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factorValue, 
            double originValue,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (originValue == 0) throw new ZeroException(() => originValue);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _originValue = originValue;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double dimensionValue = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedDimensionValue = (dimensionValue - _originValue) / _factorValue + _originValue;

            _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);

            double result = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result;
        }
    }

    // Var-Const-Var

    internal class Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factorValue;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            OperatorCalculatorBase originCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
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
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double dimensionValue = _dimensionStack.Get(_dimensionIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate();

            double transformedDimensionValue = (dimensionValue - origin) / _factorValue + origin;

            _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);

            double result2 = _signalCalculator.Calculate();

            _dimensionStack.Pop(_dimensionIndex);

            return result2;
        }
    }

    // Var-Var-Const

    internal class Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _originValue;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double originValue,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator, 
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originValue = originValue;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double factor = _factorCalculator.Calculate();

            if (factor == 0)
            {
                double result = _signalCalculator.Calculate();
                return result;
            }
            else
            {
                double dimensionValue = _dimensionStack.Get(_dimensionIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedDimensionValue = (dimensionValue - _originValue) / factor + _originValue;

                _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);
                double result = _signalCalculator.Calculate();
                _dimensionStack.Pop(_dimensionIndex);

                return result;
            }
        }
    }

    // Var-Var-Var

    internal class Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator, 
            OperatorCalculatorBase originCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
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
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double factor = _factorCalculator.Calculate();
            if (factor == 0)
            {
                double result = _signalCalculator.Calculate();
                return result;
            }
            else
            {
                double dimensionValue = _dimensionStack.Get(_dimensionIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double origin = _originCalculator.Calculate();
                double transformedDimensionValue = (dimensionValue - origin) / factor + origin;

                _dimensionStack.Push(_dimensionIndex, transformedDimensionValue);
                double result = _signalCalculator.Calculate();
                _dimensionStack.Pop(_dimensionIndex);

                return result;
            }
        }
    }
}
