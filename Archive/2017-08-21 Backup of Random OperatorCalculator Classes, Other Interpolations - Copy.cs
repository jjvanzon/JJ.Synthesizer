//// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

//using System;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Calculation.Random;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    [Obsolete("Used only in deprecated OperatorEntityToCalculatorDirectlyVisitor.")]
//    internal class Random_OperatorCalculator_OtherInterpolationTypes : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _interpolateOperator;

//        public Random_OperatorCalculator_OtherInterpolationTypes(
//            RandomCalculator_Stripe randomCalculator,
//            OperatorCalculatorBase rateCalculator,
//            OperatorCalculatorBase positionCalculator,
//            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum)
//            : base(new[] { rateCalculator, positionCalculator })
//        {
//            // HACK in a piece of patch, to reuse the Interpolate_OperatorCalculator's capability of
//            // handling many types of interpolation.

//            // Create a second Random operator calculator.
//            var randomCalculator2 = new Random_OperatorCalculator_Stripe_VarFrequency(
//                randomCalculator, rateCalculator, positionCalculator);

//            // Lead their outputs to a Interpolate operator calculator
//            _interpolateOperator = OperatorCalculatorFactory.Create_Interpolate_OperatorCalculator(
//                resampleInterpolationTypeEnum,
//                signalCalculator: randomCalculator2,
//                samplingRateCalculator: rateCalculator,
//                positionCalculator: positionCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            return _interpolateOperator.Calculate();
//        }

//        public override void Reset()
//        {
//            // HACK
//            _interpolateOperator.Reset();
//        }
//    }
//}