using System;
using JJ.Data.Synthesizer.Entities;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.SideEffects
{
    /// <summary>
    /// All the complexity in the implementation is because of floating point imprecision problems
    /// trying to get the next tone value.
    /// </summary>
    internal abstract class Tone_SideEffect_SetDefaults_BaseWithToneValueArray : Tone_SideEffect_SetDefaults_Base
    {
        private readonly double[] _toneValues;

        /// <inheritdoc />
        public Tone_SideEffect_SetDefaults_BaseWithToneValueArray(Tone tone, Tone previousTone, double[] toneValues)
            : base(tone, previousTone)
            => _toneValues = toneValues ?? throw new ArgumentNullException(nameof(toneValues));

        public override void Execute()
        {
            if (_previousTone != null)
            {
                _tone.Value = GetNextToneValueOrDefault(_previousTone.Value);
                _tone.Octave = _previousTone.Octave;

                if (_tone.Value == default)
                {
                    _tone.Value = _toneValues[0];
                    _tone.Octave++;
                }
            }
            else
            {
                _tone.Value = _toneValues[0];
                _tone.Octave = 1;
            }
        }

        private double GetNextToneValueOrDefault(double previousToneValue)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (double toneValue in _toneValues)
            {
                if (Math.Round(toneValue, 4) > Math.Round(previousToneValue, 4))
                {
                    return toneValue;
                }
            }

            return default;
        }
    }
}