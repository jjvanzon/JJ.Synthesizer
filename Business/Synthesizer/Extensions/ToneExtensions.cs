using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class ToneExtensions
    {
        /// <summary> Do not call this method repeatedly to get the same value, because that would harm performance. </summary>
        public static double GetFrequency(this Tone tone)
        {
            if (tone == null) throw new NullException(() => tone);

            // Officially this is an unnecessary null-check, but I suspect there could be programming errors.
            if (tone.Scale == null) throw new NullException(() => tone.Scale); 

            ScaleTypeEnum scaleTypeEnum = tone.Scale.GetScaleTypeEnum();

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    return tone.Number;

                case ScaleTypeEnum.Factor:
                    {
                        // BaseFrequency * (2 ^ octave) * number
                        AssertBaseFrequency(tone);
                        double frequency = tone.Scale.BaseFrequency.Value * Math.Pow(2, tone.Octave) * tone.Number;
                        return frequency;
                    }

                case ScaleTypeEnum.Exponent:
                    {
                        // BaseFrequency * 2 ^ (octave + number)
                        AssertBaseFrequency(tone);
                        double frequency = tone.Scale.BaseFrequency.Value * Math.Pow(2, tone.Octave + tone.Number);
                        return frequency;
                    }

                case ScaleTypeEnum.SemiTone:
                    {
                        // BaseFrequency * 2 ^ (octave + 1/12 * tone)
                        AssertBaseFrequency(tone);
                        double frequency = tone.Scale.BaseFrequency.Value * Math.Pow(2, tone.Octave + 1.0 / 12.0 * (tone.Number - 1));
                        return frequency;
                    }

                case ScaleTypeEnum.Cent:
                    {
                        // BaseFrequency * 2 ^ (octave + number / 1200)
                        AssertBaseFrequency(tone);
                        double frequency = tone.Scale.BaseFrequency.Value * Math.Pow(2, tone.Octave + tone.Number / 1200.0);
                        return frequency;
                    }

                default:
                    throw new InvalidValueException(scaleTypeEnum);
            }
        }

        private static void AssertBaseFrequency(Tone tone)
        {
            if (!tone.Scale.BaseFrequency.HasValue) throw new NullException(() => tone.Scale.BaseFrequency);
        }
    }
}
