using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal abstract class OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth
        : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _frequencyCalculator;
        private readonly OperatorCalculatorBase _bandWidthCalculator;
        protected readonly double _samplingRate;
        private readonly int _samplesBetweenApplyFilterVariables;

        protected BiQuadFilter _biQuadFilter;
        private int _counter;

        public OperatorCalculatorBase_Filter_VarFrequency_VarBandWidth(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase frequencyCalculator,
            OperatorCalculatorBase bandWidthCalculator,
            double samplingRate,
            int samplesBetweenApplyFilterVariables)
            : base(new OperatorCalculatorBase[] { signalCalculator, frequencyCalculator, bandWidthCalculator })
        {
            OperatorCalculatorHelper.AssertChildOperatorCalculator(signalCalculator, () => signalCalculator);
            if (frequencyCalculator == null) throw new NullException(() => frequencyCalculator);
            if (bandWidthCalculator == null) throw new NullException(() => bandWidthCalculator);
            if (samplesBetweenApplyFilterVariables < 1) throw new LessThanException(() => samplesBetweenApplyFilterVariables, 1);

            _signalCalculator = signalCalculator;
            _frequencyCalculator = frequencyCalculator;
            _bandWidthCalculator = bandWidthCalculator;
            _samplingRate = samplingRate;
            _samplesBetweenApplyFilterVariables = samplesBetweenApplyFilterVariables;

            ResetNonRecursive();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            if (_counter > _samplesBetweenApplyFilterVariables)
            {
                double frequency = _frequencyCalculator.Calculate();
                double bandWidth = _bandWidthCalculator.Calculate();

                SetBiQuadFilterVariables(frequency, bandWidth);

                _counter = 0;
            }

            double signal = _signalCalculator.Calculate();
            double value = _biQuadFilter.Transform(signal);

            _counter++;

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void SetBiQuadFilterVariables(double frequency, double bandWidth);

        public override void Reset()
        {
            base.Reset();

            ResetNonRecursive();
        }

        private void ResetNonRecursive()
        {
            double frequency = _frequencyCalculator.Calculate();
            double bandWidth = _bandWidthCalculator.Calculate();

            CreateBiQuadFilter(frequency, bandWidth);

            _counter = 0;
        }

        protected abstract void CreateBiQuadFilter(double frequency, double bandWidth);
    }
}
