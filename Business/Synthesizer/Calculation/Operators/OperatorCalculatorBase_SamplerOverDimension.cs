using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_SamplerOverDimension
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _collectionCalculator;
        private readonly OperatorCalculatorBase _fromCalculator;
        private readonly OperatorCalculatorBase _tillCalculator;
        private readonly OperatorCalculatorBase _stepCalculator;
        protected readonly OperatorCalculatorBase _positionInputCalculator;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;

        protected double _step;
        protected double _length;

        public OperatorCalculatorBase_SamplerOverDimension(
            OperatorCalculatorBase collectionCalculator,
            OperatorCalculatorBase fromCalculator,
            OperatorCalculatorBase tillCalculator,
            OperatorCalculatorBase stepCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(new[]
            {
                collectionCalculator,
                fromCalculator,
                tillCalculator,
                stepCalculator,
                positionInputCalculator,
                positionOutputCalculator
            })
        {
            _collectionCalculator = collectionCalculator;
            _fromCalculator = fromCalculator;
            _tillCalculator = tillCalculator;
            _stepCalculator = stepCalculator;
            _positionInputCalculator = positionInputCalculator;
            _positionOutputCalculator = positionOutputCalculator;

            // ReSharper disable once VirtualMemberCallInConstructor
            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sealed override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessFirstSample(double sample)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ProcessNextSample(double sample)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void FinalizeSampling()
        { }

        /// <summary> does nothing </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void ResetNonRecursive()
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void RecalculateCollection()
        {
            double originalPosition = _positionInputCalculator.Calculate();

            double from = _fromCalculator.Calculate();
            double till = _tillCalculator.Calculate();
            _step = _stepCalculator.Calculate();

            _length = till - from;
            bool isForward = _length >= 0.0;

            double position = from;

            _positionOutputCalculator._value = from;

            double sample = _collectionCalculator.Calculate();

            ProcessFirstSample(sample);

            // Prevent infinite loop.
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (_step == 0.0)
            {
                return;
            }

            position += _step;

            if (isForward)
            {
                while (position <= till)
                {
                    _positionOutputCalculator._value = position;

                    sample = _collectionCalculator.Calculate();

                    ProcessNextSample(sample);

                    position += _step;
                }
            }
            else
            {
                // Is backwards
                while (position >= till)
                {
                    _positionOutputCalculator._value = position;

                    sample = _collectionCalculator.Calculate();

                    ProcessNextSample(sample);

                    position += _step;
                }
            }

            _positionOutputCalculator._value = originalPosition;

            FinalizeSampling();
        }
    }
}
