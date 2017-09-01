//using System;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Noise_OperatorCalculator : Noise_OperatorCalculator_Base
//    {
//        private readonly DimensionStack _dimensionStack;

//        public Noise_OperatorCalculator(NoiseCalculator noiseCalculator, DimensionStack dimensionStack)
//            : base(noiseCalculator)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();
//            double value = _noiseCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal class Noise_OperatorCalculator_WithPositionInput : Noise_OperatorCalculator_Base
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;

//        public Noise_OperatorCalculator_WithPositionInput(OperatorCalculatorBase positionCalculator, NoiseCalculator noiseCalculator)
//            : base(noiseCalculator)
//        {
//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();
//            double value = _noiseCalculator.Calculate(position);
//            return value;
//        }
//    }

//    internal abstract class Noise_OperatorCalculator_Base : OperatorCalculatorBase
//    {
//        protected readonly NoiseCalculator _noiseCalculator;

//        public Noise_OperatorCalculator_Base(NoiseCalculator noiseCalculator)
//        {
//            _noiseCalculator = noiseCalculator ?? throw new ArgumentNullException(nameof(noiseCalculator));
//        }

//        public override void Reset()
//        {
//            _noiseCalculator.Reseed();
//        }
//    }
//}