//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal abstract class MaxOrMinOverDimension_OperatorCalculatorBase
//        : OperatorCalculatorBase_SamplerOverDimension
//    {
//        private double _aggregate;

//        public MaxOrMinOverDimension_OperatorCalculatorBase(
//            OperatorCalculatorBase collectionCalculator,
//            OperatorCalculatorBase fromCalculator,
//            OperatorCalculatorBase tillCalculator,
//            OperatorCalculatorBase stepCalculator,
//            DimensionStack dimensionStack)
//            : base(
//                  collectionCalculator,
//                  fromCalculator,
//                  tillCalculator,
//                  stepCalculator,
//                  dimensionStack)
//        { }

//        /// <summary> Just returns _aggregate. </summary>
//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            return _aggregate;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override void ProcessFirstSample(double item)
//        {
//            _aggregate = item;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected override void ProcessNextSample(double item)
//        {
//            bool mustOverwrite = MustOverwrite(_aggregate, item);
//            if (mustOverwrite)
//            {
//                _aggregate = item;
//            }
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        protected abstract bool MustOverwrite(double currentValue, double newValue);
//    }
//}