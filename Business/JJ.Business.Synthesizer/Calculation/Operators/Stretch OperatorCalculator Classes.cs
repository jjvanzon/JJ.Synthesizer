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
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_ConstFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorValue = factorValue;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = position / _factorValue;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }

    // Var-Var-Zero

    internal class Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_VarFactor_ZeroOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
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
                double position = _dimensionStack.Get(_previousDimensionStackIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedPosition = position / factor;

                _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

                double result = _signalCalculator.Calculate();

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
        private readonly double _factor;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_ConstFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            double factorValue, 
            double originValue,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            if (originValue == 0) throw new ZeroException(() => originValue);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factorValue;
            _origin = originValue;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double transformedPosition = (position - _origin) / _factor + _origin;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result = _signalCalculator.Calculate();

            return result;
        }
    }

    // Var-Const-Var

    internal class Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _factor;
        private readonly OperatorCalculatorBase _originCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_ConstFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double factorValue,
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator, 
                originCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            if (factorValue == 0) throw new ZeroException(() => factorValue);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(originCalculator, () => originCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factor = factorValue;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
            double origin = _originCalculator.Calculate();

            double transformedPosition = (position - origin) / _factor + origin;

            _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

            double result2 = _signalCalculator.Calculate();

            return result2;
        }
    }

    // Var-Var-Const

    internal class Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _factorCalculator;
        private readonly double _origin;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_VarFactor_ConstOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase factorCalculator,
            double origin,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator, 
                factorCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(factorCalculator, () => factorCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _origin = origin;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
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
                double position = _dimensionStack.Get(_previousDimensionStackIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double transformedPosition = (position - _origin) / factor + _origin;

                _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

                double result = _signalCalculator.Calculate();

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
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        public Stretch_VarSignal_VarFactor_VarOrigin_OperatorCalculator(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase factorCalculator, 
            OperatorCalculatorBase originCalculator,
            DimensionStack dimensionStack)
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
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _factorCalculator = factorCalculator;
            _originCalculator = originCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
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
                double position = _dimensionStack.Get(_previousDimensionStackIndex);

                // IMPORTANT: To divide the time in the output, you have to multiply the time of the input.
                double origin = _originCalculator.Calculate();
                double transformedPosition = (position - origin) / factor + origin;

                _dimensionStack.Set(_currentDimensionStackIndex, transformedPosition);

                double result = _signalCalculator.Calculate();

                return result;
            }
        }
    }
}
