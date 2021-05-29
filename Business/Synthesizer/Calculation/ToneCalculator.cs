using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions.InvalidValues;
// ReSharper disable PossibleMultipleEnumeration

namespace JJ.Business.Synthesizer.Calculation
{
    public class ToneCalculator
    {
        private const int LOWEST_DEST_OCTAVE = -1;
        private const int HIGHEST_DEST_OCTAVE = 11;
        private const int DEST_OCTAVE_COUNT = HIGHEST_DEST_OCTAVE - LOWEST_DEST_OCTAVE + 1;
        private const int LOWEST_DEST_ORDINAL = 1;
        private const int HIGHEST_DEST_ORDINAL = 12;
        private const int DEST_TONE_COUNT = DEST_OCTAVE_COUNT * 12;

        public IList<ToneDto> MakeOctavesComplete(IEnumerable<ITone> sourceTones)
        {
            if (sourceTones == null) throw new ArgumentNullException(nameof(sourceTones));

            if (!sourceTones.Any())
            {
                return new List<ToneDto>();
            }

            IList<ITone> sourceSortedTones = sourceTones.Sort().ToArray();
            int sourceToneCount = sourceSortedTones.Count;

            var destToneDtos = new ToneDto[DEST_TONE_COUNT];

            int destToneIndex = 0;

            for (int destOctave = LOWEST_DEST_OCTAVE; destOctave <= HIGHEST_DEST_OCTAVE; destOctave++)
            {
                for (int destOrdinal = LOWEST_DEST_ORDINAL; destOrdinal <= HIGHEST_DEST_ORDINAL; destOrdinal++)
                {
                    int sourceToneIndex = destToneIndex % sourceToneCount;

                    ITone sourceTone = sourceSortedTones[sourceToneIndex];

                    var destToneDto = new ToneDto
                    {
                        Octave = destOctave,
                        Ordinal = destOrdinal,
                        Value = sourceTone.Value,
                        ScaleTypeEnum = sourceTone.GetScaleTypeEnum(),
                        ScaleBaseFrequency = sourceTone.GetScaleBaseFrequency()
                    };

                    destToneDto.Frequency = CalculateFrequency(destToneDto);

                    destToneDtos[destToneIndex++] = destToneDto;
                }
            }

            return destToneDtos;
        }

        /// <summary> Do not call this method repeatedly to get the same value, because that would harm performance. </summary>
        public double CalculateFrequency(ITone tone)
        {
            if (tone == null) throw new ArgumentNullException(nameof(tone));

            ScaleTypeEnum scaleTypeEnum = tone.GetScaleTypeEnum();
            double baseFrequency = tone.GetScaleBaseFrequency();

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    return tone.Value;

                case ScaleTypeEnum.Factor:
                {
                    // BaseFrequency * (2 ^ octave) * number
                    double frequency = baseFrequency * Math.Pow(2, tone.Octave) * tone.Value;
                    return frequency;
                }

                case ScaleTypeEnum.Exponent:
                {
                    // BaseFrequency * 2 ^ (octave + number)
                    double frequency = baseFrequency * Math.Pow(2, tone.Octave + tone.Value);
                    return frequency;
                }

                case ScaleTypeEnum.SemiTone:
                {
                    // BaseFrequency * 2 ^ (octave + 1/12 * tone)
                    double frequency = baseFrequency * Math.Pow(2, tone.Octave + 1.0 / 12.0 * (tone.Value - 1));
                    return frequency;
                }

                case ScaleTypeEnum.Cent:
                {
                    // BaseFrequency * 2 ^ (octave + number / 1200)
                    double frequency = baseFrequency * Math.Pow(2, tone.Octave + tone.Value / 1200.0);
                    return frequency;
                }

                default:
                    throw new InvalidValueException(scaleTypeEnum);
            }
        }
        
        /// <summary>
        /// Tone.Octave is a number that a user can freely assign.
        /// But GetCalculatedOctave will derive the octave arithmetically from the 
        /// frequency and the base frequency.
        /// </summary>
        public int CalculateOctave(ITone tone)
        {
            if (tone == null) throw new ArgumentNullException(nameof(tone));

            double factorOfBaseFrequency = CalculateFrequency(tone) / tone.GetScaleBaseFrequency();
            double octave = Math.Log(factorOfBaseFrequency, 2.0);

            // Floating point imprecision
            octave = Math.Round(octave, 3, MidpointRounding.AwayFromZero);

            if (octave > int.MaxValue) octave = int.MaxValue;
            if (octave < int.MinValue) octave = int.MinValue;

            return (int)octave;
        }
    }
}