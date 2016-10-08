//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Demos.Synthesizer.Inlining.Shared;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Demos.Synthesizer.Inlining.WithInheritance
//{
//    internal class Sine_OperatorCalculator_ConstFrequency_WithOriginShifting : OperatorCalculatorBase
//    {
//        private readonly double _frequency;
//        private readonly DimensionStack _dimensionStack;

//        private double _origin;

//        public Sine_OperatorCalculator_ConstFrequency_WithOriginShifting(
//            double frequency,
//            DimensionStack dimensionStack)
//        {
//            if (dimensionStack == null) throw new NullException(() => dimensionStack);

//            _frequency = frequency;
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            double phase = (position - _origin) * _frequency;
//            double value = SineCalculator.Sin(phase);

//            return value;
//        }
//    }
//}
