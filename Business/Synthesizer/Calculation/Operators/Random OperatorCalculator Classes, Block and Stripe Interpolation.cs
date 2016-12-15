using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    /// <summary> Right now there aren't any other variations than VarFrequency. </summary>
    internal class Random_OperatorCalculator_BlockAndStripe_VarFrequency : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly RandomCalculatorBase _randomCalculator;
        private readonly OperatorCalculatorBase _rateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _dimensionStackIndex;

        private double _phase;
        private double _previousPosition;

        public Random_OperatorCalculator_BlockAndStripe_VarFrequency(
            RandomCalculatorBase randomCalculator,
            OperatorCalculatorBase rateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { rateCalculator })
        {
            if (randomCalculator == null) throw new NullException(() => randomCalculator);
            // TODO: Make assertion strict again, once you have more calculator variations.
            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

            _randomCalculator = randomCalculator;
            _rateCalculator = rateCalculator;
            _dimensionStack = dimensionStack;
            _dimensionStackIndex = dimensionStack.CurrentIndex;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
#if !USE_INVAR_INDICES
            double position = _dimensionStack.Get();
#else
            double position = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            double rate = _rateCalculator.Calculate();

            double positionChange = position - _previousPosition;
            _phase = _phase + positionChange * rate;

            double value = _randomCalculator.GetValue(_phase);

            _previousPosition = position;

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
#if !USE_INVAR_INDICES
            _previousPosition = _dimensionStack.Get();
#else
            _previousPosition = _dimensionStack.Get(_dimensionStackIndex);
#endif
#if ASSERT_INVAR_INDICES
            OperatorCalculatorHelper.AssertStackIndex(_dimensionStack, _dimensionStackIndex);
#endif
            _randomCalculator.Reseed();
        }
    }
}
