using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _centerFrequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;
        private readonly double _samplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;
        private readonly BiQuadFilter _biQuadFilter;

        private int _counter;

        public BandPassFilterConstantTransitionGain_OperatorCalculator_VarCenterFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase centerFrequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
                : base(new OperatorCalculatorBase[]
                {
                    signalCalculator,
                    centerFrequencyCalculator,
                    bandWidthCalculator
                })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (centerFrequencyCalculator == null) throw new NullException(() => centerFrequencyCalculator);
            if (bandWidthCalculator == null) throw new NullException(() => bandWidthCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _centerFrequencyCalculator = centerFrequencyCalculator;
            _bandWidthCalculator = bandWidthCalculator;
            _samplingRate = samplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;
            _biQuadFilter = new BiQuadFilter();

            ResetNonRecursive();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > _samplesBetweenApplyFilterVariables)
            {
                ResetNonRecursive();
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ResetNonRecursive()
        {
            double centerFrequency = _centerFrequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();

            _biQuadFilter.SetBandPassFilterConstantSkirtGainVariables(_samplingRate, centerFrequency, bandWidth);

            _counter = 0;
        }
    }

    internal class BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly double _centerFrequency;
        private readonly double _bandWidth;
        private readonly double _samplingRate;
        private readonly BiQuadFilter _biQuadFilter;

        public BandPassFilterConstantTransitionGain_OperatorCalculator_ConstCenterFrequency_ConstBandWidth(
            OperatorCalculatorBase signalCalculator,
            double centerFrequency,
            double bandWidth,
            double samplingRate)
                : base(new OperatorCalculatorBase[] { signalCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);

            _signalCalculator = signalCalculator;
            _centerFrequency = centerFrequency;
            _bandWidth = bandWidth;
            _samplingRate = samplingRate;
            _biQuadFilter = new BiQuadFilter();

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
            _biQuadFilter.SetBandPassFilterConstantSkirtGainVariables(_samplingRate, _centerFrequency, _bandWidth);
        }
    }
}