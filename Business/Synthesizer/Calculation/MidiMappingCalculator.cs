using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
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
		public const int MIDDLE_CONTROLLER_VALUE = 64;

		private readonly ScaleDto _scaleDto;
		private readonly MidiMappingDto[] _midiMappingDtos;
		private readonly IList<MidiMappingCalculatorResult> _results = new List<MidiMappingCalculatorResult>();

		public IList<MidiMappingCalculatorResult> Results => _results;

		public MidiMappingCalculator(Scale scale, IList<MidiMapping> midiMappings)
		{
			if (midiMappings == null) throw new ArgumentNullException(nameof(midiMappings));

			midiMappings.ForEach(x => new MidiMappingValidator(x).Assert());

			var scaleToDtoConverter = new ScaleToDtoConverter();
			_scaleDto = scaleToDtoConverter.Convert(scale);

			var converter = new MidiMappingToDtoConverter();
			_midiMappingDtos = midiMappings.Where(x => x.IsActive).Select(x => converter.Convert(x)).ToArray();
		}

		public void Calculate(
			IList<(int midiControllerCode, int midiControllerValue)> midiControllerCodesAndValues,
			int? midiNoteNumber,
			int? midiVelocity)
		{
			_results.Clear();

			int midiMappingDtosCount = _midiMappingDtos.Length;
			int midiControllerTupleCount = midiControllerCodesAndValues.Count;

			for (int midiMappingDtoIndex = 0; midiMappingDtoIndex < midiMappingDtosCount; midiMappingDtoIndex++)
			{
				MidiMappingDto midiMappingDto = _midiMappingDtos[midiMappingDtoIndex];

				bool mustScale = false;
				double ratio = 1.0;

				// Multiple MIDI Controller Values should be considered, but only one should be applied.
				for (int midiControllerTupleIndex = 0; midiControllerTupleIndex < midiControllerTupleCount; midiControllerTupleIndex++)
				{
					(int midiControllerCode, int midiControllerValue) = midiControllerCodesAndValues[midiControllerTupleIndex];

					if (MustScaleByMidiController(midiMappingDto, midiControllerCode, midiControllerValue))
					{
						double midiControllerRatio = (midiControllerValue - midiMappingDto.FromMidiControllerValue.Value) /
						                             (double)midiMappingDto.GetMidiControllerValueRange();
						ratio *= midiControllerRatio;
						mustScale = true;
						break;
					}
				}

				if (MustScaleByMidiNoteNumber(midiMappingDto, midiNoteNumber))
				{
					double midiNoteNumberRatio = (midiNoteNumber.Value - midiMappingDto.FromMidiNoteNumber.Value) /
					                             (double)midiMappingDto.GetMidiNoteNumberRange();
					ratio *= midiNoteNumberRatio;
					mustScale = true;
				}
				
				if (MustScaleByMidiVelocity(midiMappingDto, midiVelocity))
				{
					double midiVelocityRatio = (midiVelocity.Value - midiMappingDto.FromMidiVelocity.Value) /
					                           (double)midiMappingDto.GetMidiVelocityRange();
					ratio *= midiVelocityRatio;
					mustScale = true;
				}

				if (!mustScale)
				{
					continue;
				}

				double? destDimensionValue = null;
				if (midiMappingDto.HasDimensionValues())
				{
					destDimensionValue = GetScaledDimensionValue(midiMappingDto, ratio);
				}

				int? destPosition = null;
				if (midiMappingDto.HasPositions())
				{
					destPosition = GetScaledPosition(midiMappingDto, ratio);
				}

				int? destToneNumber = null;
				if (midiMappingDto.HasToneNumbers())
				{
					destToneNumber = GetScaledToneNumber(midiMappingDto, ratio);
				}

				_results.Add(
					new MidiMappingCalculatorResult(
						midiMappingDto.StandardDimensionEnum,
						midiMappingDto.CustomDimensionName,
						destDimensionValue,
						destPosition,
						_scaleDto,
						destToneNumber));
			}
		}

		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
		{
			int absoluteMidiControllerValue = inputMidiControllerValue;

			int midiMappingDtosCount = _midiMappingDtos.Length;
			for (int i = 0; i < midiMappingDtosCount; i++)
			{
				MidiMappingDto midiMappingDto = _midiMappingDtos[i];

				if (!midiMappingDto.IsRelative)
				{
					continue;
				}

				if (!MustScaleByMidiController(midiMappingDto, midiControllerCode, inputMidiControllerValue))
				{
					continue;
				}

				int delta = inputMidiControllerValue - MIDDLE_CONTROLLER_VALUE;

				// Overriding mechanism: last applicable mapping wins.
				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
			}

			return absoluteMidiControllerValue;
		}

		private bool MustScaleByMidiController(MidiMappingDto midiMappingDto, int? midiControllerCode, int? midiControllerValue)
		{
			bool mustScaleByMidiController = midiControllerCode.HasValue &&
			                                 midiControllerValue.HasValue &&
			                                 midiMappingDto.HasMidiControllerValues() &&
			                                 midiMappingDto.MidiControllerCode == midiControllerCode;

			return mustScaleByMidiController;
		}

		private bool MustScaleByMidiNoteNumber(MidiMappingDto midiMappingDto, int? midiNoteNumber)
		{
			bool mustScaleByMidiNoteNumber = midiMappingDto.HasMidiNoteNumbers() &&
			                                 midiNoteNumber.HasValue;

			return mustScaleByMidiNoteNumber;
		}

		private bool MustScaleByMidiVelocity(MidiMappingDto midiMappingDto, int? midiVelocity)
		{
			bool mustScaleByMidiVelocity = midiMappingDto.HasMidiVelocities() &&
			                               midiVelocity.HasValue;

			return mustScaleByMidiVelocity;
		}

		private double GetScaledDimensionValue(MidiMappingDto midiMappingDto, double ratio)
		{
			double destRange = midiMappingDto.GetDimensionValueRange();

			double destValue = ratio * destRange + midiMappingDto.FromDimensionValue.Value;

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

		private int GetScaledPosition(MidiMappingDto midiMappingDto, double ratio)
		{
			double destRange = midiMappingDto.GetPositionRange();

			double destValueDouble = ratio * destRange + midiMappingDto.FromPosition.Value;

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}

		private int GetScaledToneNumber(MidiMappingDto midiMappingDto, double ratio)
		{
			double destRange = midiMappingDto.GetToneNumberRange();

			double destValueDouble = ratio * destRange + midiMappingDto.FromToneNumber.Value;

			if (destValueDouble < 1)
			{
				destValueDouble = 1;
			}

			if (destValueDouble > _scaleDto.Frequencies.Count)
			{
				destValueDouble = _scaleDto.Frequencies.Count;
			}

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}
	}
}