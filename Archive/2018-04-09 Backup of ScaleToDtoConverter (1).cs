//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.Converters
//{
//	public class ScaleToDtoConverter
//	{
//		private const int LOWEST_DEST_OCTAVE = -1;
//		private const int HIGHEST_DEST_OCTAVE = 11;
//		private const int DEST_OCTAVE_COUNT = HIGHEST_DEST_OCTAVE - LOWEST_DEST_OCTAVE + 1;
//		private const int LOWEST_DEST_ORDINAL = 1;
//		private const int HIGHEST_DEST_ORDINAL = 12;
//		private const int DEST_TONE_COUNT = DEST_OCTAVE_COUNT * 12;

//		private readonly ToneToDtoConverter _toneToDtoConverter;

//		public ScaleToDtoConverter() => _toneToDtoConverter = new ToneToDtoConverter();

//		public ScaleDto Convert(Scale scale)
//		{
//			if (scale == null) throw new ArgumentNullException(nameof(scale));

//			var dto = new ScaleDto
//			{
//				ID = scale.ID,
//				Name = scale.Name,
//				Frequencies = GenerateToneDtos(scale)
//				              .Select(x => x.Frequency)
//				              .ToArray()
//			};

//			return dto;
//		}

//		// TODO: This is not conversion. This is calculation.
//		public IList<ToneDto> GenerateToneDtos(Scale scale)
//		{
//			if (scale == null) throw new ArgumentNullException(nameof(scale));

//			IList<ToneDto> sourceToneDtos = scale.Tones
//			                                     .Sort()
//			                                     .Select(x => _toneToDtoConverter.Convert(x))
//			                                     .ToArray();
//			if (sourceToneDtos.Count == 0)
//			{
//				return sourceToneDtos;
//			}

//			int firstSourceOctave = sourceToneDtos.First().Octave;
//			int lastSourceOctave = sourceToneDtos.Last().Octave;
//			int sourceOctaveCount = lastSourceOctave - firstSourceOctave + 1;

//			//int differenceInOctave = firstSourceOctave - LOWEST_DEST_OCTAVE;

//			int sourceToneCount = sourceToneDtos.Count;
//			int sourceToneLastIndex = sourceToneCount - 1;
//			int destToneIndex = 0;

//			var destToneDtos = new ToneDto[DEST_TONE_COUNT];

//			int sourceToneIndex = 0;
//			int sourceRepetition = 0;


//			for (int destOctave = LOWEST_DEST_OCTAVE; destOctave < HIGHEST_DEST_OCTAVE; destOctave++)
//			{
//				for (int destOrdinal = LOWEST_DEST_ORDINAL; destOrdinal < HIGHEST_DEST_ORDINAL; destOrdinal++)
//				{
//					//int sourceToneIndex = destToneIndex % sourceToneCount;

//					// TODO: Could you calculate this with simple increments?
//					//int octaveDistance = destToneIndex / sourceToneCount - differenceInOctave;

//					ToneDto sourceToneDto = sourceToneDtos[sourceToneIndex];

//					var destToneDto = new ToneDto
//					{
//						Number = destToneIndex,
//						Octave = destOctave,
//						Ordinal = destOrdinal,
//						Value = sourceToneDto.Value,
//						ScaleTypeEnum = sourceToneDto.ScaleTypeEnum,
//						Frequency = sourceToneDto.Frequency * Math.Pow(2, octaveDistance)
//					};

//					destToneDtos[destToneIndex++] = destToneDto;

//					sourceToneIndex++;
//					if (sourceToneIndex > sourceToneLastIndex)
//					{
//						sourceToneIndex = 0;
//						sourceRepetition++;
//					}
//				}
//			}

//			return destToneDtos;
//		}
//	}
//}