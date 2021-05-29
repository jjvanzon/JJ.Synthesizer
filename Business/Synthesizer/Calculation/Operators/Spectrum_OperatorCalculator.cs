using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Mathematics;
using Lomont;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Spectrum_OperatorCalculator : OperatorCalculatorBase_WithChildCalculators
    {
        private readonly OperatorCalculatorBase _positionInputCalculator;
        private readonly VariableInput_OperatorCalculator _positionOutputCalculator;
        private readonly OperatorCalculatorBase _soundCalculator;
        private readonly OperatorCalculatorBase _startCalculator;
        private readonly OperatorCalculatorBase _endCalculator;
        private readonly OperatorCalculatorBase _frequencyCountCalculator;

        // ReSharper disable once InconsistentNaming
        private readonly LomontFFT _lomontFFT;

        private double[] _harmonicVolumes;
        private int _maxPosition;

        public Spectrum_OperatorCalculator(
            OperatorCalculatorBase soundCalculator,
            OperatorCalculatorBase startCalculator,
            OperatorCalculatorBase endCalculator,
            OperatorCalculatorBase frequencyCountCalculator,
            OperatorCalculatorBase positionInputCalculator,
            VariableInput_OperatorCalculator positionOutputCalculator)
            : base(new[]
            {
                soundCalculator,
                startCalculator,
                endCalculator,
                frequencyCountCalculator,
                positionInputCalculator,
                positionOutputCalculator
            })
        {
            _soundCalculator = soundCalculator;
            _startCalculator = startCalculator;
            _endCalculator = endCalculator;
            _frequencyCountCalculator = frequencyCountCalculator;
            _positionInputCalculator = positionInputCalculator;
            _positionOutputCalculator = positionOutputCalculator;

            _lomontFFT = new LomontFFT();

            ResetPrivate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double position = _positionInputCalculator.Calculate();

            if (position < 0) position = 0;
            if (position > _maxPosition) position = _maxPosition;

            int i = (int)position;

            double frequency = _harmonicVolumes[i];

            return frequency;
        }

        public override void Reset() => ResetPrivate();

        private void ResetPrivate()
        {
            _harmonicVolumes = CreateHarmonicVolumes();
            _maxPosition = _harmonicVolumes.Length - 1;
        }

        private double[] CreateHarmonicVolumes()
        {
            double start = _startCalculator.Calculate();
            double end = _endCalculator.Calculate();
            double frequencyCountDouble = _frequencyCountCalculator.Calculate();

            // We need a lot of lenience in this code, because validity is dependent on user input,
            // and we cannot obtrusively interrupt the user with validation messages, 
            // because he is busy making music and the show must go on.
            bool startIsValid = StartIsValid(start);
            bool endIsValid = EndIsValid(end);
            bool frequencyCountIsValid = FrequencyCountIsValid(frequencyCountDouble);
            bool startComparedToEndIsValid = end > start;
            bool allValuesAreValid = startIsValid &&
                                     endIsValid &&
                                     frequencyCountIsValid &&
                                     startComparedToEndIsValid;
            if (!allValuesAreValid)
            {
                return CreateNaNHarmonicVolumes();
            }

            int frequencyCount = (int)frequencyCountDouble;
            int frequencyCountTimesTwo = frequencyCount * 2;
            var harmonicVolumes = new double[frequencyCount];

            // FFT requires an array size twice as large as the number of frequencies I want.
            var data = new double[frequencyCountTimesTwo];
            double dt = (end - start) / frequencyCountTimesTwo;

            double t = start;
            for (int i = 0; i < frequencyCountTimesTwo; i++)
            {
                _positionOutputCalculator._value = t;

                double value = _soundCalculator.Calculate();

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

        private static bool StartIsValid(double start) => !DoubleHelper.IsSpecialValue(start);

        private static bool EndIsValid(double end) => !DoubleHelper.IsSpecialValue(end);

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

            if (!MathHelper.IsPowerOf2((int)frequencyCount))
            {
                return false;
            }

            return true;
        }

        private double[] CreateNaNHarmonicVolumes() => new[] { double.NaN };
    }
}