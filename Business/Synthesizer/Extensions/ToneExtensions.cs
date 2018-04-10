using System;
using System.Linq;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.TypeChecking;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class ToneExtensions
	{
		private static readonly ToneCalculator _toneCalculator = new ToneCalculator();

		/// <summary> Polymorphic for ToneDto and Tone. </summary>
		public static ScaleTypeEnum GetScaleTypeEnum(this ITone tone)
		{
			switch (tone)
			{
				case ToneDto dto:
					return dto.ScaleTypeEnum;

				case Tone entity:
					if (entity.Scale == null) throw new NullException(() => entity.Scale);
					return entity.Scale.GetScaleTypeEnum();

				default:
					throw new UnexpectedTypeException(() => tone);
			}
		}

		/// <summary> Polymorphic for ToneDto and Tone. </summary>
		public static double GetScaleBaseFrequency(this ITone tone)
		{
			switch (tone)
			{
				case ToneDto dto:
					return dto.ScaleBaseFrequency;

				case Tone entity:
					if (entity.Scale == null) throw new NullException(() => entity.Scale);
					return entity.Scale.BaseFrequency;

				default:
					throw new UnexpectedTypeException(() => tone);
			}
		}

		/// <see cref="ToneCalculator.CalculateFrequency"/>
		public static double GetFrequency(this Tone tone) => _toneCalculator.CalculateFrequency(tone);

		/// <see cref="ToneCalculator.CalculateOctave"/>
		public static int GetCalculatedOctave(this ITone tone) => _toneCalculator.CalculateOctave(tone);

		/// <summary> TODO: Not very fast. It loops through many tones. </summary>
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