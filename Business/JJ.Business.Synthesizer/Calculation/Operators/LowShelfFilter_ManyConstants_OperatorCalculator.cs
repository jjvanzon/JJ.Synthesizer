using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class LowShelfFilter_OperatorCalculator_ManyConstants : OperatorCalculatorBase_WithChildCalculators
    {
        private const double ASSUMED_SAMPLE_RATE = 44100.0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _transitionFrequency;
        private readonly double _dbGain;
        private readonly double _transitionSlope;

        private BiQuadFilter _biQuadFilter;

        public LowShelfFilter_OperatorCalculator_ManyConstants(
            OperatorCalculatorBase signalCalculator,
            double transitionFrequency,
            double dbGain,
            double transitionSlope)
            : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _transitionFrequency = transitionFrequency;
            _dbGain = dbGain;
            _transitionSlope = transitionSlope;

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
            _biQuadFilter = BiQuadFilter.CreateLowShelf(ASSUMED_SAMPLE_RATE, _transitionFrequency, _transitionSlope, _dbGain);
        }
    }


    internal class LowShelfFilter_OperatorCalculator_ManyVariables
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _transitionFrequencyCalculator;
        private readonly OperatorCalculatorBase _dbGainCalculator;
        private readonly OperatorCalculatorBase _transitionSlopeCalculator;
        private readonly double _samplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;

        private BiQuadFilter _biQuadFilter;
        private int _counter;

        public LowShelfFilter_OperatorCalculator_ManyVariables(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase transitionFrequencyCalculator,
            OperatorCalculatorBase dbGainCalculator,
            OperatorCalculatorBase transitionSlopeCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new OperatorCalculatorBase[]
            {
                signalCalculator,
                transitionFrequencyCalculator,
                dbGainCalculator,
                transitionSlopeCalculator
            })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (transitionFrequencyCalculator == null) throw new NullException(() => transitionFrequencyCalculator);
            if (dbGainCalculator == null) throw new NullException(() => dbGainCalculator);
            if (transitionSlopeCalculator == null) throw new NullException(() => transitionSlopeCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _transitionFrequencyCalculator = transitionFrequencyCalculator;
            _dbGainCalculator = dbGainCalculator;
            _transitionSlopeCalculator = transitionSlopeCalculator;
            _samplingRate = samplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > _samplesBetweenApplyFilterVariables)
            {
                double transitionFrequency = _transitionFrequencyCalculator.Calculate();
                double dbGain = _dbGainCalculator.Calculate();
                double transitionSlope = _transitionSlopeCalculator.Calculate();

                _biQuadFilter.SetLowShelfVariables(_samplingRate, transitionFrequency, transitionSlope, dbGain);

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
            double transitionFrequency = _transitionFrequencyCalculator.Calculate();
            double dbGain = _dbGainCalculator.Calculate();
            double transitionSlope = _transitionSlopeCalculator.Calculate();

            _biQuadFilter = BiQuadFilter.CreateLowShelf(_samplingRate, transitionFrequency, transitionSlope, dbGain);

            _counter = 0;
        }
    }
}
