using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Mathematics;
using JJ.Framework.Reflection.Exceptions;
using Lomont;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Spectrum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private const double DEFAULT_TIME = 0.0;
        private const int DEFAULT_CHANNEL_INDEX = 0;

        private readonly OperatorCalculatorBase _signalCalculator;
        private readonly OperatorCalculatorBase _startTimeCalculator;
        private readonly OperatorCalculatorBase _endTimeCalculator;
        private readonly OperatorCalculatorBase _frequencyCountCalculator;

        private readonly LomontFFT _lomontFFT;

        private double[] _harmonicVolumes;

        private int _channelIndex;
        private double _previousTime;

        public Spectrum_OperatorCalculator(
            OperatorCalculatorBase signalCalculator,
            OperatorCalculatorBase startTimeCalculator,
            OperatorCalculatorBase endTimeCalculator,
            OperatorCalculatorBase frequencyCountCalculator)
            : base(new OperatorCalculatorBase[] 
            {
                signalCalculator,
                startTimeCalculator,
                endTimeCalculator,
                frequencyCountCalculator
            })
        {
            OperatorCalculatorHelper.AssertOperatorCalculatorBase(signalCalculator, () => signalCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase_OnlyUsedUponResetState(startTimeCalculator, () => startTimeCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase_OnlyUsedUponResetState(endTimeCalculator, () => endTimeCalculator);
            OperatorCalculatorHelper.AssertOperatorCalculatorBase_OnlyUsedUponResetState(frequencyCountCalculator, () => frequencyCountCalculator);

            _signalCalculator = signalCalculator;
            _startTimeCalculator = startTimeCalculator;
            _endTimeCalculator = endTimeCalculator;
            _frequencyCountCalculator = frequencyCountCalculator;

            _lomontFFT = new LomontFFT();

            Reset(DEFAULT_TIME, DEFAULT_CHANNEL_INDEX);
        }

        public override double Calculate(double time, int channelIndex)
        {
            _channelIndex = channelIndex;

            if (time < 0) time = 0;
            if (time > _harmonicVolumes.Length - 1) time = _harmonicVolumes.Length - 1;

            int i = (int)time;

            double frequency = _harmonicVolumes[i];

            _previousTime = time;

            return frequency;
        }

        public override void Reset(double time, int channelIndex)
        {
            _previousTime = time;
            _harmonicVolumes = CreateHarmonicVolumes(time, channelIndex);

            // NOTE: Do not call base.
            // The Spectrum Operator is an exception to the rule.
            // Reset for spectrum means NOT resetting the signal,
            // but just recalculating the spectrum.
        }

        private double[] CreateHarmonicVolumes(double time, int channelIndex)
        {
            double startTime = _startTimeCalculator.Calculate(time, channelIndex);
            double endTime = _endTimeCalculator.Calculate(time, channelIndex);
            double frequencyCountDouble = _frequencyCountCalculator.Calculate(time, channelIndex);

            // We need a lot of lenience in this code, because validity is dependent on user input,
            // and we cannot obtrusively interrupt the user with validation messages, 
            // because he is busy making music and the show must go on.
            bool startTimeIsValid = StartTimeIsValid(startTime);
            bool endTimeIsValid = EndTimeIsValid(endTime);
            bool frequencyCountIsValid = FrequencyCountIsValid(frequencyCountDouble);
            bool startTimeComparedToEndTimeIsValid = endTime > startTime;
            bool allValuesAreValid = startTimeIsValid &&
                                     endTimeIsValid &&
                                     frequencyCountIsValid &&
                                     startTimeComparedToEndTimeIsValid;
            if (!allValuesAreValid)
            {
                return CreateNaNHarmonicVolumes();
            }

            int frequencyCount = (int)frequencyCountDouble;
            int frequencyCountTimesTwo = frequencyCount * 2;
            double[] harmonicVolumes = new double[frequencyCount];

            // FFT requires an array size twice as large as the number of frequencies I want.
            double[] data = new double[frequencyCountTimesTwo];
            double dt = (endTime - startTime) / frequencyCountTimesTwo;

            double t = startTime;
            for (int i = 0; i < frequencyCountTimesTwo; i++)
            {
                double value = _signalCalculator.Calculate(t, _channelIndex);
                data[i] = value;

                t += dt;
            }

            // TODO: Not sure which is faster: forward or backward.
            _lomontFFT.RealFFT(data, forward: true);

            int j = 0;
            for (int i = 3; i < frequencyCountTimesTwo; i += 2)
            {
                harmonicVolumes[j] = data[i];
                j++;
            }

            // TODO: The FFT algorithm I borrowed does not give me the last frequency.
            harmonicVolumes[frequencyCount - 1] = 0;

            return harmonicVolumes;
        }

        private static bool StartTimeIsValid(double startTime)
        {
            return !Double.IsNaN(startTime) && !Double.IsInfinity(startTime);
        }

        private static bool EndTimeIsValid(double endTime)
        {
            return !Double.IsNaN(endTime) && !Double.IsInfinity(endTime);
        }

        private static bool FrequencyCountIsValid(double frequencyCount)
        {
            if (!CalculationHelper.CanCastToInt32(frequencyCount))
            {
                return false;
            }

            if (frequencyCount < 2.0)
            {
                return false;
            }

            if (!Maths.IsPowerOf2((int)frequencyCount))
            {
                return false;
            }

            return true;
        }

        private double[] CreateNaNHarmonicVolumes()
        {
            return new double[] { Double.NaN };
        }
    }
}