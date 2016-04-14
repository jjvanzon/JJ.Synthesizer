using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;
        private readonly int _dimensionIndex;

        private double _position0;
        protected double _value0;

        public Resample_OperatorCalculator_Block(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase samplingRateCalculator,
            DimensionEnum dimensionEnum)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
            _dimensionIndex = (int)dimensionEnum;
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double samplingRate = _samplingRateCalculator.Calculate(dimensionStack);

            return Calculate(dimensionStack, samplingRate);
        }

        /// <summary> This extra overload prevents additional invokations of the _samplingRateCalculator in derived classes </summary>
        protected double Calculate(DimensionStack dimensionStack, double samplingRate)
        {
            double position = dimensionStack.Get(_dimensionIndex);

            double offset = position - _position0;
            double sampleCount = offset * samplingRate;
            sampleCount = Math.Truncate(sampleCount);

            if (sampleCount != 0.0)
            {
                _position0 += sampleCount / samplingRate;

                dimensionStack.Push(_dimensionIndex, _position0);

                _value0 = _signalCalculator.Calculate(dimensionStack);

                dimensionStack.Pop(_dimensionIndex);
            }

            return _value0;
        }
    }
}