using System;
using System.Runtime.CompilerServices;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Noise_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionCalculator;
        protected readonly NoiseCalculator _noiseCalculator;

        public Noise_OperatorCalculator(OperatorCalculatorBase positionCalculator, NoiseCalculator noiseCalculator)
            : base(new[] { positionCalculator })
        {
            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
            _noiseCalculator = noiseCalculator ?? throw new ArgumentNullException(nameof(noiseCalculator));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionCalculator.Calculate();
            double value = _noiseCalculator.Calculate(position);
            return value;
        }

        public override void Reset()
        {
            _noiseCalculator.Reseed();
        }
    }
}
