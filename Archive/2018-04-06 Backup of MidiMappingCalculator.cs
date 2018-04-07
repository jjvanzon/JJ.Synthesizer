//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Converters;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
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

//		private readonly MidiMappingDto[] _midiMappingDtos;

//		private readonly IList<MidiMappingCalculatorResult> _results = new List<MidiMappingCalculatorResult>();
//		public IList<MidiMappingCalculatorResult> Results => _results;

//		private readonly Dictionary<int, IList<MidiMappingDto>> _midiControllerCode_ToMidiMappingDtos_Dictionary;
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
//			                                                                   .ToNonUniqueDictionary(x => x.MidiControllerCode.Value);
//		}

//		//public IList<(DimensionEnum dimensionEnum, string canonicalName, int position)> CalculateFromMidiControllerValue(int midiControllerCode, int midiControllerValue)
//		public IList<MidiMappingCalculatorResult> CalculateDimensionValuesFromMidiControllerValue(int midiControllerCode, int midiControllerValue)
//		{
//			if (!_midiControllerCode_ToMidiMappingDtos_Dictionary.TryGetValue(midiControllerCode, out IList<MidiMappingDto> sourceDtos))
//			{
//				return new MidiMappingCalculatorResult[0];
//			}

//			IList<MidiMappingCalculatorResult> results = sourceDtos.Select(
//				                                                       x => new MidiMappingCalculatorResult(
//					                                                       x.DimensionEnum,
//					                                                       x.Name,
//					                                                       x.Position,
//					                                                       GetScaledDimensionValue(midiControllerValue, x)))
//			                                                       .ToArray();
//			return results;

//			//return new (DimensionEnum dimensionEnum, string canonicalName, int position)[0];
//		}

//		public IList<MidiMappingCalculatorResult> CalculateDimensionValuesFromMidiNoteValues(int midiNoteNumber, int midiVelocity, int midiChannel)
//		{
//			//_midiVelocity_MidiMappingDtos
//			return null;
//		}

//		public int CalculateDimensionValueToMidiControllerValue(DimensionEnum dimensionEnum, string canonicalName, int position, int midiControllerCode)
//		{
//			return default;
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

//				double destDimensionValue = GetScaledDimensionValue(midiValue.Value, midiMappingDto);

//				_results.Add(
//					new MidiMappingCalculatorResult(midiMappingDto.DimensionEnum, midiMappingDto.Name, midiMappingDto.Position, destDimensionValue));
//			}
//		}

//		private double GetScaledDimensionValue(double midiValue, MidiMappingDto midiMappingDto)
//		{
//			double ratio = (midiValue - midiMappingDto.FromMidiValue) / midiMappingDto.GetMidiValueRange();

//			double destRange = midiMappingDto.GetDimensionValueRange();

//			double destValue = ratio * destRange + midiMappingDto.FromDimensionValue;

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