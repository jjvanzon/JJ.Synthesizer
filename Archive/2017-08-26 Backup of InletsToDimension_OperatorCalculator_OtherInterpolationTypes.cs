﻿//// MIND THE HACKS IN THIS FILE! IT MAY BE THE CAUSE OF YOUR PROBLEMS!

//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    [Obsolete("Used only in deprecated OperatorEntityToCalculatorDirectlyVisitor.")]
//    internal class InletsToDimension_OperatorCalculator_OtherInterpolationTypes : OperatorCalculatorBase_WithChildCalculators
//    {
//        private readonly OperatorCalculatorBase _interpolateOperator;

//        public InletsToDimension_OperatorCalculator_OtherInterpolationTypes(
//            IList<OperatorCalculatorBase> operandCalculators,
//            ResampleInterpolationTypeEnum resampleInterpolationTypeEnum,
//            DimensionStack dimensionStack)
//            : base(operandCalculators)
//        {
//            // HACK in a piece of patch, 
//            // to reuse the behavior of InletsToDimension
//            // and to reuse the Interpolate_OperatorCalculator's capability of
//            // handling many types of interpolation.

//            var inletsToDimensionCalculator = new InletsToDimension_OperatorCalculator_Stripe(operandCalculators, dimensionStack);

//            _interpolateOperator = OperatorCalculatorFactory.Create_Interpolate_OperatorCalculator(
//                resampleInterpolationTypeEnum,
//                inletsToDimensionCalculator,
//                new Number_OperatorCalculator(1.0),
//                dimensionStack);
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