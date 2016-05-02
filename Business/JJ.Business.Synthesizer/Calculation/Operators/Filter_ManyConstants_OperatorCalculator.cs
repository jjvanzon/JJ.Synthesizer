using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly FilterTypeEnum _filterTypeEnum;
        private readonly double _frequency;
        private readonly double _bandWidth;
        private readonly double _dbGain;
        private readonly double _shelfSlope;

        private BiQuadFilter _biQuadFilter;

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
            _filterTypeEnum = filterTypeEnum;
            _frequency = frequency;
            _bandWidth = bandWidth;
            _dbGain = dbGain;
            _shelfSlope = shelfSlope;

            Reset(new DimensionStack());
        }

        public override double Calculate(DimensionStack dimensionStack)
        {
            double signal = _signalCalculator.Calculate(dimensionStack);

            float value = _biQuadFilter.Transform((float)signal);

            return value;
        }

        public override void Reset(DimensionStack dimensionStack)
        {
            base.Reset(dimensionStack);

            switch (_filterTypeEnum)
            {
                case FilterTypeEnum.LowPassFilter:
                    _biQuadFilter = BiQuadFilter.LowPassFilter(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.HighPassFilter:
                    _biQuadFilter = BiQuadFilter.HighPassFilter(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantSkirtGain:
                    _biQuadFilter = BiQuadFilter.BandPassFilterConstantSkirtGain(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantPeakGain:
                    _biQuadFilter = BiQuadFilter.BandPassFilterConstantPeakGain(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.NotchFilter:
                    _biQuadFilter = BiQuadFilter.NotchFilter(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.AllPassFilter:
                    _biQuadFilter = BiQuadFilter.AllPassFilter(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth);
                    break;

                case FilterTypeEnum.PeakingEQ:
                    _biQuadFilter = BiQuadFilter.PeakingEQ(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_bandWidth, (float)_dbGain);
                    break;

                case FilterTypeEnum.LowShelf:
                    _biQuadFilter = BiQuadFilter.LowShelf(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_shelfSlope, (float)_dbGain);
                    break;

                case FilterTypeEnum.HighShelf:
                    _biQuadFilter = BiQuadFilter.HighShelf(ASSUMED_SAMPLE_RATE, (float)_frequency, (float)_shelfSlope, (float)_dbGain);
                    break;

                default:
                    throw new InvalidValueException(_filterTypeEnum);
            }
        }
    }
}