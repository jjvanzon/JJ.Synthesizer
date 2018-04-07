//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Converters;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Collections;
//using JJ.Framework.Exceptions.InvalidValues;

//// ReSharper disable SuggestBaseTypeForParameter
//// ReSharper disable PossibleInvalidOperationException
//// ReSharper disable ConvertToAutoProperty
//// ReSharper disable ConvertToAutoPropertyWhenPossible

//namespace JJ.Business.Synthesizer.Calculation
//{
//	/// <summary>
//	/// Not thread-safe.
//	/// In particular the Results property is overwritten in-place in the Calculate method.
//	/// This is done to avoid garbage collection.
//	/// </summary>
//	public class MidiMappingCalculator
//	{
//		public const int MIDDLE_CONTROLLER_VALUE = 64;

//		private static readonly (DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[] _emptyDimensionValueArray =
//			new(DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[0];

//		private readonly IList<MidiMappingCalculatorResult> _results = new List<MidiMappingCalculatorResult>();
//		public IList<MidiMappingCalculatorResult> Results => _results;

//		private readonly MidiMappingDto[] _midiMappingDtos;

//		private readonly Dictionary<int, MidiMappingDto[]> _midiControllerCode_ToMidiMappingDtos_Dictionary;
//		private readonly MidiMappingDto[] _midiVelocity_MidiMappingDtos;
//		private readonly MidiMappingDto[] _midiNoteNumber_MidiMappingDtos;
//		private readonly MidiMappingDto[] _midiChannel_MidiMappingDtos;

//		public MidiMappingCalculator(IList<MidiMapping> midiMappings)
//		{
//			if (midiMappings == null) throw new ArgumentNullException(nameof(midiMappings));

//			midiMappings.ForEach(x => new MidiMappingValidator(x).Assert());

//			var converter = new MidiMappingToDtoConverter();

//			_midiMappingDtos = midiMappings.Where(x => x.IsActive).Select(x => converter.Convert(x)).ToArray();

//			_midiVelocity_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiVelocity).ToArray();
//			_midiNoteNumber_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiNoteNumber).ToArray();
//			_midiChannel_MidiMappingDtos = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiChannel).ToArray();
//			_midiControllerCode_ToMidiMappingDtos_Dictionary = _midiMappingDtos.Where(x => x.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiController)
//			                                                                   .Where(x => x.MidiControllerCode.HasValue)
//			                                                                   .GroupBy(x => x.MidiControllerCode.Value)
//			                                                                   .ToDictionary(x => x.Key, x => x.ToArray());
//		}

//		public (DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[] CalculateFromMidiControllerValue(
//			int midiControllerCode,
//			int midiControllerValue)
//		{
//			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out MidiMappingDto[] sourceDtos))
//			{
//				return _emptyDimensionValueArray;
//			}

//			for (int i = 0; i < sourceDtos.Length; i++)
//			{

//			}
//			// TODO: Avoid enumerables.
//			var results = sourceDtos.Select(x => (x.DimensionEnum, x.Name, x.Position, CalculateDimensionValue(midiControllerValue, x))).ToArray();

//			return results;
//		}

//		public (DimensionEnum dimensionEnum, string canonicalName, int? position, double value)[] CalculateFromMidiNote(
//			int midiNoteNumber,
//			int midiVelocity,
//			int midiChannel)
//		{
//			// TODO: Avoid enumerables.
//			var enumerable1 =
//				_midiNoteNumber_MidiMappingDtos.Select(x => (x.DimensionEnum, x.Name, x.Position, CalculateDimensionValue(midiNoteNumber, x)));

//			var enumerable2 =
//				_midiVelocity_MidiMappingDtos.Select(x => (x.DimensionEnum, x.Name, x.Position, CalculateDimensionValue(midiVelocity, x)));

//			var enumerable3 =
//				_midiChannel_MidiMappingDtos.Select(x => (x.DimensionEnum, x.Name, x.Position, CalculateDimensionValue(midiChannel, x)));

//			return enumerable1.Union(enumerable2).Union(enumerable3).ToArray();
//		}

//		public int? CalculateMidiControllerValueOrNull(
//			DimensionEnum dimensionEnum,
//			string canonicalName,
//			int? position,
//			double dimensionValue,
//			int midiControllerCode)
//		{
//			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out MidiMappingDto[] midiMappingDtos))
//			{
//				return null;
//			}

//			if (midiMappingDtos.Length == 0)
//			{
//				return null;
//			}

//			MidiMappingDto midiMappingDto = midiMappingDtos[0];

//			int midiControllerValue = CalculateMidiControllerValue(dimensionValue, midiMappingDto);
//			return midiControllerValue;
//		}

//		public void Calculate(
//			Dictionary<int, int> midiControllerDictionary,
//			int? midiNoteNumber,
//			int? midiVelocity,
//			int? midiChannel)
//		{
//			_results.Clear();

//			int count = _midiMappingDtos.Length;

//			for (int i = 0; i < count; i++)
//			{
//				MidiMappingDto midiMappingDto = _midiMappingDtos[i];

//				double? midiValue = null;

//				switch (midiMappingDto.MidiMappingTypeEnum)
//				{
//					case MidiMappingTypeEnum.MidiNoteNumber:
//						midiValue = midiNoteNumber;
//						break;

//					case MidiMappingTypeEnum.MidiVelocity:
//						midiValue = midiVelocity;
//						break;

//					case MidiMappingTypeEnum.MidiChannel:
//						midiValue = midiChannel;
//						break;

//					case MidiMappingTypeEnum.MidiController:
//						if (midiMappingDto.MidiControllerCode.HasValue)
//						{
//							if (midiControllerDictionary.TryGetValue(midiMappingDto.MidiControllerCode.Value, out int midiControllerValue))
//							{
//								midiValue = midiControllerValue;
//							}
//						}

//						break;

//					default:
//						throw new ValueNotSupportedException(midiMappingDto.MidiMappingTypeEnum);
//				}

//				if (!midiValue.HasValue)
//				{
//					continue;
//				}

//				double destDimensionValue = CalculateDimensionValue(midiValue.Value, midiMappingDto);

//				_results.Add(
//					new MidiMappingCalculatorResult(midiMappingDto.DimensionEnum, midiMappingDto.Name, midiMappingDto.Position, destDimensionValue));
//			}
//		}

//		private double CalculateDimensionValue(double midiValue, MidiMappingDto midiMappingDto)
//		{
//			double destValue = MathHelper.ScaleLinearly(
//				midiValue,
//				midiMappingDto.FromMidiValue,
//				midiMappingDto.TillMidiValue,
//				midiMappingDto.FromDimensionValue,
//				midiMappingDto.TillDimensionValue);

//			if (destValue < midiMappingDto.MinDimensionValue)
//			{
//				destValue = midiMappingDto.MinDimensionValue.Value;
//			}

//			if (destValue > midiMappingDto.MaxDimensionValue)
//			{
//				destValue = midiMappingDto.MaxDimensionValue.Value;
//			}

//			return destValue;
//		}

//		//private int CalculateMidiControllerValue(DimensionEnum dimensionEnum, string canonicalName, int? position, double dimensionValue, MidiMappingDto midiMappingDto)
//		private int CalculateMidiControllerValue(double dimensionValue, MidiMappingDto midiMappingDto)
//		{
//			double destValue = MathHelper.ScaleLinearly(
//				dimensionValue,
//				midiMappingDto.FromDimensionValue,
//				midiMappingDto.TillDimensionValue,
//				midiMappingDto.FromMidiValue,
//				midiMappingDto.TillMidiValue);

//			if (destValue > int.MaxValue)
//			{
//				destValue = int.MaxValue;
//			}

//			if (destValue < int.MinValue)
//			{
//				destValue = int.MinValue;
//			}

//			return (int)destValue;
//		}

//		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
//		{
//			int absoluteMidiControllerValue = inputMidiControllerValue;

//			int count = _midiMappingDtos.Length;
//			for (int i = 0; i < count; i++)
//			{
//				MidiMappingDto midiMappingDto = _midiMappingDtos[i];

//				if (!midiMappingDto.IsRelative)
//				{
//					continue;
//				}

//				if (!MustScaleByMidiController(midiMappingDto, midiControllerCode))
//				{
//					continue;
//				}

//				int delta = inputMidiControllerValue - MIDDLE_CONTROLLER_VALUE;

//				// Overriding mechanism: last applicable mapping wins.
//				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
//			}

//			return absoluteMidiControllerValue;
//		}

//		private bool MustScaleByMidiController(MidiMappingDto midiMappingDto, int midiControllerCode)
//		{
//			bool mustScaleByMidiController = midiMappingDto.MidiMappingTypeEnum == MidiMappingTypeEnum.MidiController &&
//			                                 midiMappingDto.MidiControllerCode.HasValue &&
//			                                 midiMappingDto.MidiControllerCode == midiControllerCode;

//			return mustScaleByMidiController;
//		}
//	}
//}