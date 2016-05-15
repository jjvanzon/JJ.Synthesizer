using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly DimensionStack _dimensionStack;
        private readonly int _currentDimensionStackIndex;
        private readonly int _previousDimensionStackIndex;

        private double _position0;
        protected double _value0;

        public Resample_OperatorCalculator_Block(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionStack dimensionStack)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);
            OperatorCalculatorHelper.AssertDimensionStack_ForWriters(dimensionStack);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionStack = dimensionStack;
            _currentDimensionStackIndex = dimensionStack.CurrentIndex;
            _previousDimensionStackIndex = dimensionStack.CurrentIndex - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double samplingRate = _samplingRateCalculator.Calculate();

            return Calculate(samplingRate);
        }

        /// <summary> This extra overload prevents additional invokations of the _samplingRateCalculator in derived classes </summary>
        protected double Calculate(double samplingRate)
        {
            double position = _dimensionStack.Get(_previousDimensionStackIndex);

            double offset = position - _position0;
            double sampleCount = offset * samplingRate;
            sampleCount = Math.Truncate(sampleCount);

            if (sampleCount != 0.0)
            {
                _position0 += sampleCount / samplingRate;

                _dimensionStack.Set(_currentDimensionStackIndex, _position0);

                _value0 = _signalCalculator.Calculate();
            }

            return _value0;
        }
    }
}