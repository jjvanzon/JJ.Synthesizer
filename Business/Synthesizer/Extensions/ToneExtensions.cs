using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class ToneExtensions
	{
		/// <summary> Do not call this method repeatedly to get the same value, because that would harm performance. </summary>
		public static double GetFrequency(this Tone tone)
		{
			if (tone == null) throw new NullException(() => tone);
			if (tone.Scale == null) throw new NullException(() => tone.Scale);

			ScaleTypeEnum scaleTypeEnum = tone.Scale.GetScaleTypeEnum();

			switch (scaleTypeEnum)
			{
				case ScaleTypeEnum.LiteralFrequency:
					return tone.Value;

				case ScaleTypeEnum.Factor:
				{
					// BaseFrequency * (2 ^ octave) * number
					double frequency = tone.Scale.BaseFrequency * Math.Pow(2, tone.Octave) * tone.Value;
					return frequency;
				}

				case ScaleTypeEnum.Exponent:
				{
					// BaseFrequency * 2 ^ (octave + number)
					double frequency = tone.Scale.BaseFrequency * Math.Pow(2, tone.Octave + tone.Value);
					return frequency;
				}

				case ScaleTypeEnum.SemiTone:
				{
					// BaseFrequency * 2 ^ (octave + 1/12 * tone)
					double frequency = tone.Scale.BaseFrequency * Math.Pow(2, tone.Octave + 1.0 / 12.0 * (tone.Value - 1));
					return frequency;
				}

				case ScaleTypeEnum.Cent:
				{
					// BaseFrequency * 2 ^ (octave + number / 1200)
					double frequency = tone.Scale.BaseFrequency * Math.Pow(2, tone.Octave + tone.Value / 1200.0);
					return frequency;
				}

				default:
					throw new InvalidValueException(scaleTypeEnum);
			}
		}

		public static IEnumerable<Tone> Sort(this IEnumerable<Tone> tones)
		{
			if (tones == null) throw new ArgumentNullException(nameof(tones));

			IList<Tone> sortedTones = tones.OrderBy(x => x.Octave)
			                               .ThenBy(x => x.Value)
			                               .ToArray();
			return sortedTones;
		}

		/// <summary>
		/// Tone.Octave is a number that a user can freely assign.
		/// But GetCalculatoedOctave will derive the octave arithmetically from the 
		/// frequency and the base frequency.
		/// </summary>
		public static int GetCalculatedOctave(this Tone tone)
		{
			if (tone == null) throw new ArgumentNullException(nameof(tone));
			if (tone.Scale == null) throw new NullException(() => tone.Scale);

			double factorOfBaseFrequency = tone.Value / tone.Scale.BaseFrequency;
			double octave = Math.Log(factorOfBaseFrequency, 2.0);

			// Floating point imprecision
			octave = Math.Round(octave, 3, MidpointRounding.AwayFromZero);

			if (octave > int.MaxValue) octave = int.MaxValue;
			if (octave < int.MinValue) octave = int.MinValue;

			return (int)octave;
		}

		public static int GetOrdinal(this Tone tone)
		{
			if (tone == null) throw new ArgumentNullException(nameof(tone));
			if (tone.Scale == null) throw new NullException(() => tone.Scale);

			int ordinal = tone.Scale.Tones
			                  .Where(x => x.Octave == tone.Octave)
			                  .Sort()
			                  .IndexOf(tone) + 1;
			return ordinal;
		}
	}
}