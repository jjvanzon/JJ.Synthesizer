using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.CopiedCode.FromFramework;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable ConvertToAutoProperty
// ReSharper disable ConvertToAutoPropertyWhenPossible

namespace JJ.Business.Synthesizer.Calculation
{
	/// <summary>
	/// Not thread-safe.
	/// In particular the Results property is overwritten in-place in the Calculate method.
	/// This is done to avoid garbage collection.
	/// </summary>
	public class MidiMappingCalculator
	{
		public const int CENTER_CONTROLLER_VALUE = 64;

		private static readonly (DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[] _emptyDimensionValueArray =
			new(DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[0];

		private readonly MidiMappingDto[] _midiMappingDtos;

		private readonly Dictionary<int, MidiMappingDto[]> _midiControllerCode_ToMidiMappingDtos_Dictionary;
		private readonly MidiMappingDto[] _midiVelocity_MidiMappingDtos;
		private readonly MidiMappingDto[] _midiNoteNumber_MidiMappingDtos;
		private readonly MidiMappingDto[] _midiChannel_MidiMappingDtos;
		private readonly int _noteMappingDtoCount;

		public MidiMappingCalculator(IList<MidiMapping> midiMappings)
		{
			if (midiMappings == null) throw new ArgumentNullException(nameof(midiMappings));

			midiMappings.ForEach(x => new MidiMappingValidator(x).Assert());

			var converter = new MidiMappingToDtoConverter();

			_midiMappingDtos = midiMappings.Where(x => x.IsActive).Select(x => converter.Convert(x)).ToArray();

			_midiVelocity_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiVelocity).ToArray();
			_midiNoteNumber_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiNoteNumber).ToArray();
			_midiChannel_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiChannel).ToArray();
			_midiControllerCode_ToMidiMappingDtos_Dictionary = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiController)
			                                                                   .Where(x => x.MidiControllerCode.HasValue)
			                                                                   .GroupBy(x => x.MidiControllerCode.Value)
			                                                                   .ToDictionary(x => x.Key, x => x.ToArray());

			_noteMappingDtoCount =
				_midiVelocity_MidiMappingDtos.Length + _midiNoteNumber_MidiMappingDtos.Length + _midiChannel_MidiMappingDtos.Length;
		}

		public (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[] CalculateForMidiController(
			int midiControllerCode,
			int midiControllerValue)
		{
			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out MidiMappingDto[] sourceMidiMappingDtos))
			{
				return _emptyDimensionValueArray;
			}

			int count = sourceMidiMappingDtos.Length;
			var results = new(DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[count];

			for (int i = 0; i < count; i++)
			{
				MidiMappingDto dto = sourceMidiMappingDtos[i];
				results[i] = (dto.DimensionEnum, dto.Name, dto.Position, CalculateDimensionValue(midiControllerValue, dto));
			}

			return results;
		}

		public (DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[] CalculateForMidiNote(
			int midiNoteNumber,
			int midiVelocity,
			int midiChannel)
		{
			int j = 0;
			var results = new(DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue)[_noteMappingDtoCount];

			{
				MidiMappingDto[] dtos = _midiNoteNumber_MidiMappingDtos;
				int dtoCount = dtos.Length;
				for (int i = 0; i < dtoCount; i++)
				{
					results[j++] = (
						dtos[i].DimensionEnum,
						dtos[i].Name,
						dtos[i].Position,
						CalculateDimensionValue(midiNoteNumber, dtos[i]));
				}
			}

			{
				MidiMappingDto[] dtos = _midiVelocity_MidiMappingDtos;
				int dtoCount = dtos.Length;
				for (int i = 0; i < dtoCount; i++)
				{
					results[j++] = (
						dtos[i].DimensionEnum,
						dtos[i].Name,
						dtos[i].Position,
						CalculateDimensionValue(midiVelocity, dtos[i]));
				}
			}

			{
				MidiMappingDto[] dtos = _midiChannel_MidiMappingDtos;
				int dtoCount = dtos.Length;
				for (int i = 0; i < dtoCount; i++)
				{
					results[j++] = (
						dtos[i].DimensionEnum,
						dtos[i].Name,
						dtos[i].Position,
						CalculateDimensionValue(midiChannel, dtos[i]));
				}
			}

			return results;
		}

		public int? CalculateMidiControllerValueOrNull(int midiControllerCode, double dimensionValue)
		{
			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out MidiMappingDto[] midiMappingDtos))
			{
				return null;
			}

			if (midiMappingDtos.Length == 0)
			{
				return null;
			}

			MidiMappingDto midiMappingDto = midiMappingDtos[0];

			int midiControllerValue = CalculateMidiControllerValue(dimensionValue, midiMappingDto);

			return midiControllerValue;
		}

		private double CalculateDimensionValue(double midiValue, MidiMappingDto midiMappingDto)
		{
			double destValue = MathHelper.ScaleLinearly(
				midiValue,
				midiMappingDto.FromMidiValue,
				midiMappingDto.TillMidiValue,
				midiMappingDto.FromDimensionValue,
				midiMappingDto.TillDimensionValue);

			if (destValue < midiMappingDto.MinDimensionValue)
			{
				destValue = midiMappingDto.MinDimensionValue.Value;
			}

			if (destValue > midiMappingDto.MaxDimensionValue)
			{
				destValue = midiMappingDto.MaxDimensionValue.Value;
			}

			return destValue;
		}

		private int CalculateMidiControllerValue(double dimensionValue, MidiMappingDto midiMappingDto)
		{
			double destValue = MathHelper.ScaleLinearly(
				dimensionValue,
				midiMappingDto.FromDimensionValue,
				midiMappingDto.TillDimensionValue,
				midiMappingDto.FromMidiValue,
				midiMappingDto.TillMidiValue);

			if (destValue > int.MaxValue)
			{
				destValue = int.MaxValue;
			}

			if (destValue < int.MinValue)
			{
				destValue = int.MinValue;
			}

			return (int)destValue;
		}

		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
		{
			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out MidiMappingDto[] midiMappingDtos))
			{
				return inputMidiControllerValue;
			}

			int absoluteMidiControllerValue = inputMidiControllerValue;

			int count = midiMappingDtos.Length;
			for (int i = 0; i < count; i++)
			{
				MidiMappingDto midiMappingDto = _midiMappingDtos[i];

				if (!midiMappingDto.IsRelative)
				{
					continue;
				}

				int delta = inputMidiControllerValue - CENTER_CONTROLLER_VALUE;

				// Overriding mechanism: last applicable mapping wins.
				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
			}

			return absoluteMidiControllerValue;
		}
	}
}