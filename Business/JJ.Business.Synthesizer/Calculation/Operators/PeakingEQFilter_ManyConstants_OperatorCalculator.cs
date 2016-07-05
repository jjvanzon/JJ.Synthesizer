using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class PeakingEQFilter_OperatorCalculator_ManyConstants : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _centerFrequency;
        private readonly double _bandWidth;
        private readonly double _dbGain;
        private readonly double _samplingRate;

        private BiQuadFilter _biQuadFilter;

        public PeakingEQFilter_OperatorCalculator_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double dbGain,
            double samplingRate)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            if (signalCalculator == null) throw new NullException(() => signalCalculator);
            if (signalCalculator is Number_OperatorCalculator) throw new IsTypeException<Number_OperatorCalculator>(() => signalCalculator);

            _signalCalculator = signalCalculator;
            _centerFrequency = centerFrequency;
            _bandWidth = bandWidth;
            _dbGain = dbGain;
            _samplingRate = samplingRate;

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
            _biQuadFilter = BiQuadFilter.CreatePeakingEQ(_samplingRate, _centerFrequency, _bandWidth, _dbGain);
        }
    }

    internal class PeakingEQFilter_OperatorCalculator_ManyVariables
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _centerFrequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly double _samplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

        public PeakingEQFilter_OperatorCalculator_ManyVariables(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            OperatorCalculatorBase dbGainCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                centerFrequencyCalculator,
                bandWidthCalculator,
                dbGainCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (centerFrequencyCalculator == null) throw new NullException(() => centerFrequencyCalculator);
            if (bandWidthCalculator == null) throw new NullException(() => bandWidthCalculator);
            if (dbGainCalculator == null) throw new NullException(() => dbGainCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _centerFrequencyCalculator = centerFrequencyCalculator;
            _bandWidthCalculator = bandWidthCalculator;
            _dbGainCalculator = dbGainCalculator;
            _samplingRate = samplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > _samplesBetweenApplyFilterVariables)
            {
                double centerFrequency = _centerFrequencyCalculator.Calculate();
                double bandWidth = _bandWidthCalculator.Calculate();
                double dbGain = _dbGainCalculator.Calculate();

                _biQuadFilter.SetPeakingEQVariables(_samplingRate, centerFrequency, bandWidth, dbGain);

                _counter = 0;
            }

            double signal = _signalCalculator.Calculate();
            double value = _biQuadFilter.Transform(signal);

            _counter++;

            return value;
        }

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            double centerFrequency = _centerFrequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();

            _biQuadFilter = BiQuadFilter.CreatePeakingEQ(_samplingRate, centerFrequency, bandWidth, dbGain);

            _counter = 0;
        }
    }
}
