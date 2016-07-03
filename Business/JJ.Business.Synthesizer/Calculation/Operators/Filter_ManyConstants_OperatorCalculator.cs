using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Filter_ManyConstants_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;

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

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double signal = _signalCalculator.Calculate();

            double value = _biQuadFilter.Transform(signal);

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            switch (_filterTypeEnum)
            {
                case FilterTypeEnum.LowPassFilter:
                    _biQuadFilter = BiQuadFilter.CreateLowPassFilter(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.HighPassFilter:
                    _biQuadFilter = BiQuadFilter.CreateHighPassFilter(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantSkirtGain:
                    _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantSkirtGain(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.BandPassFilterConstantPeakGain:
                    _biQuadFilter = BiQuadFilter.CreateBandPassFilterConstantPeakGain(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.NotchFilter:
                    _biQuadFilter = BiQuadFilter.CreateNotchFilter(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.AllPassFilter:
                    _biQuadFilter = BiQuadFilter.CreateAllPassFilter(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth);
                    break;

                case FilterTypeEnum.PeakingEQ:
                    _biQuadFilter = BiQuadFilter.CreatePeakingEQ(ASSUMED_SAMPLE_RATE, _frequency, _bandWidth, _dbGain);
                    break;

                case FilterTypeEnum.LowShelf:
                    _biQuadFilter = BiQuadFilter.CreateLowShelf(ASSUMED_SAMPLE_RATE, _frequency, _shelfSlope, _dbGain);
                    break;

                case FilterTypeEnum.HighShelf:
                    _biQuadFilter = BiQuadFilter.CreateHighShelf(ASSUMED_SAMPLE_RATE, _frequency, _shelfSlope, _dbGain);
                    break;

                default:
                    throw new InvalidValueException(_filterTypeEnum);
            }
        }
    }
}