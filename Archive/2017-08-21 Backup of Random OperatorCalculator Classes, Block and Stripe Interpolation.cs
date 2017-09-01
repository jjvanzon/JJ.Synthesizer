//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Calculation.Random;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    /// <summary> Right now there aren't any other variations than VarFrequency. </summary>
//    internal class Random_OperatorCalculator_Block_VarFrequency : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly RandomCalculator_Block _randomCalculator;
//        private readonly OperatorCalculatorBase _rateCalculator;
//        private readonly OperatorCalculatorBase _positionCalculator;

//        private double _phase;
//        private double _previousPosition;

//        public Random_OperatorCalculator_Block_VarFrequency(
//            RandomCalculator_Block randomCalculator,
//            OperatorCalculatorBase rateCalculator,
//            OperatorCalculatorBase positionCalculator)
//            : base(new[] { rateCalculator, positionCalculator })
//        {
//            _randomCalculator = randomCalculator ?? throw new NullException(() => randomCalculator);
//            _rateCalculator = rateCalculator;
//            _positionCalculator = positionCalculator;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();

//            double rate = _rateCalculator.Calculate();

//            double positionChange = position - _previousPosition;
//            _phase = _phase + positionChange * rate;

//            double value = _randomCalculator.Calculate(_phase);

//            _previousPosition = position;

//            return value;
//        }

//        public override void Reset()
//        {
//            base.Reset();

//            ResetNonRecursive();
//        }

//        private void ResetNonRecursive()
//        {
//            _previousPosition = _positionCalculator.Calculate();

//            _randomCalculator.Reseed();
//        }
//    }

//    /// <summary> Right now there aren't any other variations than VarFrequency. </summary>
//    internal class Random_OperatorCalculator_Stripe_VarFrequency : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly RandomCalculator_Stripe _randomCalculator;
//        private readonly OperatorCalculatorBase _rateCalculator;
//        private readonly OperatorCalculatorBase _positionCalculator;

//        private double _phase;
//        private double _previousPosition;

//        public Random_OperatorCalculator_Stripe_VarFrequency(
//            RandomCalculator_Stripe randomCalculator,
//            OperatorCalculatorBase rateCalculator,
//            OperatorCalculatorBase positionCalculator)
//            : base(new[] { rateCalculator, positionCalculator })
//        {
//            _randomCalculator = randomCalculator ?? throw new NullException(() => randomCalculator);
//            _rateCalculator = rateCalculator;
//            _positionCalculator = positionCalculator;

//            ResetNonRecursive();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();

//            double rate = _rateCalculator.Calculate();

//            double positionChange = position - _previousPosition;
//            _phase = _phase + positionChange * rate;

//            double value = _randomCalculator.Calculate(_phase);

//            _previousPosition = position;

//            return value;
//        }

//        public override void Reset()
//        {
//            base.Reset();

//            ResetNonRecursive();
//        }

//        private void ResetNonRecursive()
//        {
//            _previousPosition = _positionCalculator.Calculate();

//            _randomCalculator.Reseed();
//        }
//    }
//}
