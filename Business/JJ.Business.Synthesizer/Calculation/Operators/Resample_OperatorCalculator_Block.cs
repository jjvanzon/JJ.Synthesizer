using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator_Block : OperatorCalculatorBase_WithChildCalculators
    {
        private double MINIMUM_SAMPLING_RATE = 1.0 / 8.0; // 8 Hz.

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _samplingRateCalculator;

        private double _previousTime;

        private double _t0;
        private double _x0;
        private double _tOffset;

        public Resample_OperatorCalculator_Block(
            OperatorCalculatorBase signalCalculator, 
            OperatorCalculatorBase samplingRateCalculator)
            : base(new OperatorCalculatorBase[] { signalCalculator, samplingRateCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => signalCalculator);
            if (samplingRateCalculator == null) throw new NullException(() => samplingRateCalculator);
            // TODO: Resample with constant sampling rate does not have specialized calculators yet. Reactivate code line after those specialized calculators have been programmed.
            //if (samplingRateCalculator is Number_OperatorCalculator) throw new IsNotTypeException<Number_OperatorCalculator>(() => samplingRateCalculator);

            _signalCalculator = signalCalculator;
            _samplingRateCalculator = samplingRateCalculator;
        }

        public override double Calculate(double time, int channelIndex)
        {
            double samplingRate = _samplingRateCalculator.Calculate(time, channelIndex);

            if (samplingRate != 0.0) // Weird number: time stands still.
            {
                double sampleDuration = 1.0 / samplingRate;

                double dt = time - _previousTime;

                _tOffset += dt;

                if (_tOffset > sampleDuration)
                {
                    _t0 += sampleDuration;
                    _x0 = _signalCalculator.Calculate(_t0, channelIndex);

                    _tOffset -= sampleDuration;
                }
            }

            _previousTime = time;

            return _x0;
        }
    }
}
