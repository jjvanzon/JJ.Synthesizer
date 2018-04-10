//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer.Interfaces;
//using JJ.Framework.Exceptions.InvalidValues;

//namespace JJ.Business.Synthesizer.Calculation
//{
//	public class ToneCalculator
//	{
//		private const int LOWEST_DEST_OCTAVE = -1;
//		private const int HIGHEST_DEST_OCTAVE = 11;
//		private const int DEST_OCTAVE_COUNT = HIGHEST_DEST_OCTAVE - LOWEST_DEST_OCTAVE + 1;
//		private const int LOWEST_DEST_ORDINAL = 1;
//		private const int HIGHEST_DEST_ORDINAL = 12;
//		private const int DEST_TONE_COUNT = DEST_OCTAVE_COUNT * 12;

//		public IList<ToneDto> CompleteOctaves(ScaleTypeEnum scaleTypeEnum, double baseFrequency, IList<ITone> sourceTones)
//		{
//			if (sourceTones == null) throw new ArgumentNullException(nameof(sourceTones));

//			int sourceToneCount = sourceTones.Count;

//			if (sourceToneCount == 0)
//			{
//				return new List<ToneDto>();
//			}

//			IList<ITone> sourceSortedTones = sourceTones.Sort().ToArray();

//			var destToneDtos = new ToneDto[DEST_TONE_COUNT];

//			int destToneIndex = 0;

//			for (int destOctave = LOWEST_DEST_OCTAVE; destOctave < HIGHEST_DEST_OCTAVE; destOctave++)
//			{
//				for (int destOrdinal = LOWEST_DEST_ORDINAL; destOrdinal < HIGHEST_DEST_ORDINAL; destOrdinal++)
//				{
//					int sourceToneIndex = destToneIndex % sourceToneCount;

//					ITone sourceTone = sourceSortedTones[sourceToneIndex];

//					var destToneDto = new ToneDto
//					{
//						Number = destToneIndex,
//						Octave = destOctave,
//						Ordinal = destOrdinal,
//						Value = sourceTone.Value,
//						ScaleTypeEnum = sourceTone.GetScaleTypeEnum()
//					};

//					destToneDto.Frequency = GetFrequency(scaleTypeEnum, baseFrequency, destToneDto);

//					destToneDtos[destToneIndex++] = destToneDto;
//				}
//			}

//			return destToneDtos;
//		}

//		public double GetFrequency(ScaleTypeEnum scaleTypeEnum, double baseFrequency, ITone tone)
//		{
//			if (tone == null) throw new ArgumentNullException(nameof(tone));

//			switch (scaleTypeEnum)
//			{
//				case ScaleTypeEnum.LiteralFrequency:
//					return tone.Value;

//				case ScaleTypeEnum.Factor:
//				{
//					// BaseFrequency * (2 ^ octave) * number
//					double frequency = baseFrequency * Math.Pow(2, tone.Octave) * tone.Value;
//					return frequency;
//				}

//				case ScaleTypeEnum.Exponent:
//				{
//					// BaseFrequency * 2 ^ (octave + number)
//					double frequency = baseFrequency * Math.Pow(2, tone.Octave + tone.Value);
//					return frequency;
//				}

//				case ScaleTypeEnum.SemiTone:
//				{
//					// BaseFrequency * 2 ^ (octave + 1/12 * tone)
//					double frequency = baseFrequency * Math.Pow(2, tone.Octave + 1.0 / 12.0 * (tone.Value - 1));
//					return frequency;
//				}

//				case ScaleTypeEnum.Cent:
//				{
//					// BaseFrequency * 2 ^ (octave + number / 1200)
//					double frequency = baseFrequency * Math.Pow(2, tone.Octave + tone.Value / 1200.0);
//					return frequency;
//				}

//				default:
//					throw new InvalidValueException(scaleTypeEnum);
//			}
//		}
//	}
//}