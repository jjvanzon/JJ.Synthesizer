//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Calculation.Random;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class Random_OperatorCalculator_Stripe : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly RandomCalculatorBase _randomCalculator;
//        private readonly double _randomCalculatorOffset;
//        private readonly OperatorCalculatorBase _rateCalculator;
//        private readonly OperatorCalculatorBase _phaseShiftCalculator;

//        private double _phase;
//        private double _previousTime;

//        public Random_OperatorCalculator_Stripe(
//            RandomCalculatorBase randomCalculator,
//            double randomCalculatorOffset,
//            OperatorCalculatorBase rateCalculator,
//            OperatorCalculatorBase phaseShiftCalculator)
//            : base(new OperatorCalculatorBase[] { rateCalculator, phaseShiftCalculator })
//        {
//            if (randomCalculator == null) throw new NullException(() => randomCalculator);
//            // TODO: Make assertion strict again, once you have more calculator variations.
//            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(frequencyCalculator, () => frequencyCalculator);
//            //OperatorCalculatorHelper.AssertOperatorCalculatorBase(phaseShiftCalculator, () => phaseShiftCalculator);

//            _randomCalculator = randomCalculator;
//            _randomCalculatorOffset = randomCalculatorOffset;
//            _rateCalculator = rateCalculator;
//            _phaseShiftCalculator = phaseShiftCalculator;

//            _phase = _randomCalculatorOffset;
//        }

//        public override double Calculate(double time, int channelIndex)
//        {
//            double rate = _rateCalculator.Calculate(time, channelIndex);
//            double phaseShift = _phaseShiftCalculator.Calculate(time, channelIndex);

//            double dt = time - _previousTime;
//            _phase = _phase + dt / rate;

//            // TODO: This all looks a little weird. Like it could be done simpler.
//            double shiftedPhase = _phase + phaseShift;

//            double earlierTimeShiftToGetFromBlockedToStriped = 0.5 * rate;

//            // IMPORTANT: To subtract time from the output, you have add time to the input.
//            double transformedPhase = shiftedPhase + earlierTimeShiftToGetFromBlockedToStriped;

//            double value = _randomCalculator.GetValue(transformedPhase);

//            _previousTime = time;

//            return value;
//        }
//    }
//}