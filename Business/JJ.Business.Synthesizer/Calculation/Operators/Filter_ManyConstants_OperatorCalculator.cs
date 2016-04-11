using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection.Exceptions;
using NAudio.Dsp;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Filter_ManyConstants_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const float ASSUMED_SAMPLE_RATE = 44100;
        private const float DEFAULT_BAND_WIDTH = 1;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly BiQuadFilter _biQuadFilter;

        public Filter_ManyConstants_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            double frequency,
            double bandWidth,
            double dbGain,
            double shelfSlope,
            FilterTypeEnum filterTypeEnum)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;

            switch (filterTypeEnum)
            {
                case FilterTypeEnum.LowPassFilter:
                    _biQuadFilter = BiQuadFilter.LowPassFilter(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.HighPassFilter:
                    _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantSkirtGain:
                    _biQuadFilter = BiQuadFilter.BandPassFilterConstantSkirtGain(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantPeakGain:
                    _biQuadFilter = BiQuadFilter.BandPassFilterConstantPeakGain(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.NotchFilter:
                    _biQuadFilter = BiQuadFilter.NotchFilter(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.AllPassFilter:
                    _biQuadFilter = BiQuadFilter.AllPassFilter(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth);
                    break;

                case FilterTypeEnum.PeakingEQ:
                    _biQuadFilter = BiQuadFilter.PeakingEQ(ASSUMED_SAMPLE_RATE, (float)frequency, (float)bandWidth, (float)dbGain);
                    break;

                case FilterTypeEnum.LowShelf:
                    _biQuadFilter = BiQuadFilter.LowShelf(ASSUMED_SAMPLE_RATE, (float)frequency, (float)shelfSlope, (float)dbGain);
                    break;

                case FilterTypeEnum.HighShelf:
                    _biQuadFilter = BiQuadFilter.HighShelf(ASSUMED_SAMPLE_RATE, (float)frequency, (float)shelfSlope, (float)dbGain);
                    break;

                default:
                    throw new InvalidValueException(filterTypeEnum);
            }
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double signal = _signalCalculator.Calculate(dimensionStack);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }
    }

}
