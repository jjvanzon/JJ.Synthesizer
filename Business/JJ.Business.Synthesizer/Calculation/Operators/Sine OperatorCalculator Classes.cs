using JJ.Framework.Reflection.Exceptions;
using System;
using JJ.Framework.Mathematics;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sine_WithVarFrequency_WithoutPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double value = SineCalculator.Sin(_phase);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly double _phaseShift;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sine_WithVarFrequency_WithConstPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            double phaseShift,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { frequencyCalculator })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (phaseShift % 1.0 == 0.0) throw new Exception("phaseShift cannot be a multiple of 1.");
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShift = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double result = SineCalculator.Sin(_phase + _phaseShift);

            _previousPosition = position;

            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        private double _phase;
        private double _previousPosition;

        public Sine_WithVarFrequency_WithVarPhaseShift_OperatorCalculator(
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] 
            {
                frequencyCalculator,
                phaseShiftCalculator
            })
        {
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequencyCalculator = frequencyCalculator;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double frequency = _frequencyCalculator.Calculate();
            double phaseShift = _phaseShiftCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * frequency;

            double result = SineCalculator.Sin(_phase + phaseShift);

            _previousPosition = position;

            return result;
        }

        public override void Reset()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            _previousPosition = position;
            _phase = 0.0;

            base.Reset();
        }
    }

    internal class Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Sine_WithConstFrequency_WithoutPhaseShift_OperatorCalculator(
            double frequency,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequency = frequency;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double value = SineCalculator.Sin(position * _frequency);
            return value;
        }
    }

    internal class Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator : OperatorCalculatorBase
    {
        private readonly double _frequency;
        private readonly double _phaseShift;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Sine_WithConstFrequency_WithConstPhaseShift_OperatorCalculator(
            double frequency, 
            double phaseShift,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (phaseShift % 1.0 == 0.0) throw new Exception("phaseShift cannot be a multiple of 1.");
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequency = frequency;
            _phaseShift = phaseShift;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            double result = SineCalculator.Sin(position * _frequency + _phaseShift);
            return result;
        }
    }

    internal class Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly double _frequency;
        private readonly OperatorCalculatorBase _phaseShiftCalculator;
        private readonly int _dimensionIndex;
        private readonly DimensionStacks _dimensionStack;

        public Sine_WithConstFrequency_WithVarPhaseShift_OperatorCalculator(
            double frequency, 
            OperatorCalculatorBase phaseShiftCalculator,
            DimensionEnum dimensionEnum,
            DimensionStacks dimensionStack)
            : base(new OperatorCalculatorBase[] { phaseShiftCalculator })
        {
            if (frequency == 0.0) throw new ZeroException(() => frequency);
            if (phaseShiftCalculator == null) throw new NullException(() => phaseShiftCalculator);
            OperatorCalculatorHelper.AssertDimensionEnum(dimensionEnum);
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            _frequency = frequency;
            _phaseShiftCalculator = phaseShiftCalculator;
            _dimensionIndex = (int)dimensionEnum;
            _dimensionStack = dimensionStack;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _dimensionStack.Get(_dimensionIndex);

            // TODO: Not tested.
            double phaseShift = _phaseShiftCalculator.Calculate();
            double result = SineCalculator.Sin(position * _frequency + phaseShift);
            return result;
        }
    }
}