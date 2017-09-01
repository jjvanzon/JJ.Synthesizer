//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation.Operators
//{
//    internal class SampleWithRate1_OperatorCalculator_NoChannelConversion : OperatorCalculatorBase
//    {
//        private readonly ICalculatorWithPosition[] _underlyingCalculators;
//        private readonly DimensionStack _dimensionStack;
//        private readonly DimensionStack _channelDimensionStack;
//        private readonly double _maxChannelIndexDouble;

//        public SampleWithRate1_OperatorCalculator_NoChannelConversion(
//            IList<ICalculatorWithPosition> underlyingCalculators,
//            DimensionStack dimensionStack,
//            DimensionStack channelDimensionStack)
//        {
//            if (underlyingCalculators == null) throw new NullException(() => underlyingCalculators);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);
//            OperatorCalculatorHelper.AssertDimensionStack(channelDimensionStack);

//            _underlyingCalculators = underlyingCalculators.ToArray();
//            _dimensionStack = dimensionStack;
//            _channelDimensionStack = channelDimensionStack;

//            _maxChannelIndexDouble = _underlyingCalculators.Length - 1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double channelIndexDouble = _channelDimensionStack.Get();

//            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndexDouble))
//            {
//                return 0.0;
//            }
//            int channelIndex = (int)channelIndexDouble;

//            double position = _dimensionStack.Get();

//            double value = _underlyingCalculators[channelIndex].Calculate(position);
//            return value;
//        }
//    }

//    internal class SampleWithRate1_OperatorCalculator_NoChannelConversion_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly ICalculatorWithPosition[] _underlyingCalculators;
//        private readonly OperatorCalculatorBase _positionInput;
//        private readonly OperatorCalculatorBase _channelPositionInput;
//        private readonly double _maxChannelIndexDouble;

//        public SampleWithRate1_OperatorCalculator_NoChannelConversion_WithPositionInput(
//            OperatorCalculatorBase positionInput,
//            OperatorCalculatorBase channelPositionInput,
//            IList<ICalculatorWithPosition> underlyingCalculators)
//        {
//            _channelPositionInput = channelPositionInput ?? throw new ArgumentNullException(nameof(channelPositionInput));
//            _positionInput = positionInput ?? throw new ArgumentNullException(nameof(positionInput));

//            _underlyingCalculators = underlyingCalculators?.ToArray() ?? throw new NullException(() => underlyingCalculators);

//            _maxChannelIndexDouble = _underlyingCalculators.Length - 1;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double channelIndexDouble = _channelPositionInput.Calculate();

//            if (!ConversionHelper.CanCastToNonNegativeInt32WithMax(channelIndexDouble, _maxChannelIndexDouble))
//            {
//                return 0.0;
//            }
//            int channelIndex = (int)channelIndexDouble;

//            double position = _positionInput.Calculate();

//            double value = _underlyingCalculators[channelIndex].Calculate(position);
//            return value;
//        }
//    }

//    internal class SampleWithRate1_OperatorCalculator_MonoToStereo : OperatorCalculatorBase
//    {
//        private readonly ICalculatorWithPosition _underlyingCalculator;
//        private readonly DimensionStack _dimensionStack;

//        public SampleWithRate1_OperatorCalculator_MonoToStereo(
//            ICalculatorWithPosition underlyingCalculator,
//            DimensionStack dimensionStack)
//        {
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            // Return the single channel for both channels.
//            double value = _underlyingCalculator.Calculate(position);

//            return value;
//        }
//    }

//    internal class SampleWithRate1_OperatorCalculator_MonoToStereo_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionInput;
//        private readonly ICalculatorWithPosition _underlyingCalculator;

//        public SampleWithRate1_OperatorCalculator_MonoToStereo_WithPositionInput(
//            OperatorCalculatorBase positionInput,
//            ICalculatorWithPosition underlyingCalculator)
//        {
//            _positionInput = positionInput ?? throw new ArgumentNullException(nameof(positionInput));

//            _underlyingCalculator = underlyingCalculator ?? throw new NullException(() => underlyingCalculator);
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionInput.Calculate();

//            // Return the single channel for both channels.
//            double value = _underlyingCalculator.Calculate(position);

//            return value;
//        }
//    }

//    internal class SampleWithRate1_OperatorCalculator_StereoToMono : OperatorCalculatorBase
//    {
//        private readonly ICalculatorWithPosition[] _underlyingCalculators;
//        private readonly DimensionStack _dimensionStack;

//        public SampleWithRate1_OperatorCalculator_StereoToMono(
//            IList<ICalculatorWithPosition> underlyingCalculators,
//            DimensionStack dimensionStack)
//        {
//            if (underlyingCalculators == null) throw new NullException(() => underlyingCalculators);
//            if (underlyingCalculators.Count != 2) throw new NotEqualException(() => underlyingCalculators.Count, 2);
//            OperatorCalculatorHelper.AssertDimensionStack(dimensionStack);

//            _underlyingCalculators = underlyingCalculators.ToArray();
//            _dimensionStack = dimensionStack;
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _dimensionStack.Get();

//            double value =
//                _underlyingCalculators[0].Calculate(position) +
//                _underlyingCalculators[1].Calculate(position);

//            return value;
//        }
//    }

//    internal class SampleWithRate1_OperatorCalculator_StereoToMono_WithPositionInput : OperatorCalculatorBase
//    {
//        private readonly OperatorCalculatorBase _positionCalculator;
//        private readonly ICalculatorWithPosition[] _underlyingCalculators;

//        public SampleWithRate1_OperatorCalculator_StereoToMono_WithPositionInput(
//            OperatorCalculatorBase positionCalculator,
//            IList<ICalculatorWithPosition> underlyingCalculators)
//        {
//            if (underlyingCalculators == null) throw new NullException(() => underlyingCalculators);
//            if (underlyingCalculators.Count != 2) throw new NotEqualException(() => underlyingCalculators.Count, 2);

//            _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
//            _underlyingCalculators = underlyingCalculators.ToArray();
//        }

//        [MethodImpl(MethodImplOptions.AggressiveInlining)]
//        public override double Calculate()
//        {
//            double position = _positionCalculator.Calculate();

//            double value =
//                _underlyingCalculators[0].Calculate(position) +
//                _underlyingCalculators[1].Calculate(position);

//            return value;
//        }
//    }
//}