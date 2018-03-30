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
		private readonly MidiMappingElementDto[] _midiMappingElementDtos;
		private readonly IList<MidiMappingCalculatorResult> _results = new List<MidiMappingCalculatorResult>();

		public IList<MidiMappingCalculatorResult> Results => _results;

		public MidiMappingCalculator(Scale scale, IList<MidiMappingElement> midiMappingElements)
		{
			if (midiMappingElements == null) throw new ArgumentNullException(nameof(midiMappingElements));

			midiMappingElements.ForEach(x => new MidiMappingElementValidator(x).Assert());

			var scaleToDtoConverter = new ScaleToDtoConverter();
			_scaleDto = scaleToDtoConverter.Convert(scale);

			var converter = new MidiMappingElementToDtoConverter();
			_midiMappingElementDtos = midiMappingElements.Where(x => x.IsActive).Select(x => converter.Convert(x)).ToArray();
		}

		public void Calculate(
			IList<(int midiControllerCode, int midiControllerValue)> midiControllerCodesAndValues,
			int? midiNoteNumber,
			int? midiVelocity)
		{
			_results.Clear();

			int midiMappingElementDtosCount = _midiMappingElementDtos.Length;
			int midiControllerTupleCount = midiControllerCodesAndValues.Count;

			for (int midiMappingElementIndex = 0; midiMappingElementIndex < midiMappingElementDtosCount; midiMappingElementIndex++)
			{
				MidiMappingElementDto midiMappingElementDto = _midiMappingElementDtos[midiMappingElementIndex];

				bool mustScale = false;
				double ratio = 1.0;

				// Multiple MIDI Controller Values should be considered, but only one should be applied.
				for (int midiControllerTupleIndex = 0; midiControllerTupleIndex < midiControllerTupleCount; midiControllerTupleIndex++)
				{
					(int midiControllerCode, int midiControllerValue) = midiControllerCodesAndValues[midiControllerTupleIndex];

					if (MustScaleByMidiController(midiMappingElementDto, midiControllerCode, midiControllerValue))
					{
						double midiControllerRatio = (midiControllerValue - midiMappingElementDto.FromMidiControllerValue.Value) /
						                             (double)midiMappingElementDto.GetMidiControllerValueRange();
						ratio *= midiControllerRatio;
						mustScale = true;
						break;
					}
				}

				if (MustScaleByMidiNoteNumber(midiMappingElementDto, midiNoteNumber))
				{
					double midiNoteNumberRatio = (midiNoteNumber.Value - midiMappingElementDto.FromMidiNoteNumber.Value) /
					                             (double)midiMappingElementDto.GetMidiNoteNumberRange();
					ratio *= midiNoteNumberRatio;
					mustScale = true;
				}
				
				if (MustScaleByMidiVelocity(midiMappingElementDto, midiVelocity))
				{
					double midiVelocityRatio = (midiVelocity.Value - midiMappingElementDto.FromMidiVelocity.Value) /
					                           (double)midiMappingElementDto.GetMidiVelocityRange();
					ratio *= midiVelocityRatio;
					mustScale = true;
				}

				if (!mustScale)
				{
					continue;
				}

				double? destDimensionValue = null;
				if (midiMappingElementDto.HasDimensionValues())
				{
					destDimensionValue = GetScaledDimensionValue(midiMappingElementDto, ratio);
				}

				int? destPosition = null;
				if (midiMappingElementDto.HasPositions())
				{
					destPosition = GetScaledPosition(midiMappingElementDto, ratio);
				}

				int? destToneNumber = null;
				if (midiMappingElementDto.HasToneNumbers())
				{
					destToneNumber = GetScaledToneNumber(midiMappingElementDto, ratio);
				}

				_results.Add(
					new MidiMappingCalculatorResult(
						midiMappingElementDto.StandardDimensionEnum,
						midiMappingElementDto.CustomDimensionName,
						destDimensionValue,
						destPosition,
						_scaleDto,
						destToneNumber));
			}
		}

		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
		{
			int absoluteMidiControllerValue = inputMidiControllerValue;

			int midiMappingElementsCount = _midiMappingElementDtos.Length;
			for (int i = 0; i < midiMappingElementsCount; i++)
			{
				MidiMappingElementDto midiMappingElementDto = _midiMappingElementDtos[i];

				if (!midiMappingElementDto.IsRelative)
				{
					continue;
				}

				if (!MustScaleByMidiController(midiMappingElementDto, midiControllerCode, inputMidiControllerValue))
				{
					continue;
				}

				int delta = inputMidiControllerValue - MIDDLE_CONTROLLER_VALUE;

				// Overriding mechanism: last applicable mapping wins.
				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
			}

			return absoluteMidiControllerValue;
		}

		private bool MustScaleByMidiController(MidiMappingElementDto midiMappingElementDto, int? midiControllerCode, int? midiControllerValue)
		{
			bool mustScaleByMidiController = midiControllerCode.HasValue &&
			                                 midiControllerValue.HasValue &&
			                                 midiMappingElementDto.HasMidiControllerValues() &&
			                                 midiMappingElementDto.MidiControllerCode == midiControllerCode;

			return mustScaleByMidiController;
		}

		private bool MustScaleByMidiNoteNumber(MidiMappingElementDto midiMappingElementDto, int? midiNoteNumber)
		{
			bool mustScaleByMidiNoteNumber = midiMappingElementDto.HasMidiNoteNumbers() &&
			                                 midiNoteNumber.HasValue;

			return mustScaleByMidiNoteNumber;
		}

		private bool MustScaleByMidiVelocity(MidiMappingElementDto midiMappingElementDto, int? midiVelocity)
		{
			bool mustScaleByMidiVelocity = midiMappingElementDto.HasMidiVelocities() &&
			                               midiVelocity.HasValue;

			return mustScaleByMidiVelocity;
		}

		private double GetScaledDimensionValue(MidiMappingElementDto midiMappingElementDto, double ratio)
		{
			double destRange = midiMappingElementDto.GetDimensionValueRange();

			double destValue = ratio * destRange + midiMappingElementDto.FromDimensionValue.Value;

			if (destValue < midiMappingElementDto.MinDimensionValue)
			{
				destValue = midiMappingElementDto.MinDimensionValue.Value;
			}

			if (destValue > midiMappingElementDto.MaxDimensionValue)
			{
				destValue = midiMappingElementDto.MaxDimensionValue.Value;
			}

			return destValue;
		}

		private int GetScaledPosition(MidiMappingElementDto midiMappingElementDto, double ratio)
		{
			double destRange = midiMappingElementDto.GetPositionRange();

			double destValueDouble = ratio * destRange + midiMappingElementDto.FromPosition.Value;

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}

		private int GetScaledToneNumber(MidiMappingElementDto midiMappingElementDto, double ratio)
		{
			double destRange = midiMappingElementDto.GetToneNumberRange();

			double destValueDouble = ratio * destRange + midiMappingElementDto.FromToneNumber.Value;

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