using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

// ReSharper disable PossibleInvalidOperationException

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

		private readonly IList<MidiMappingElement> _midiMappingElements;

		public MidiMappingCalculator(IList<MidiMappingElement> midiMappingElements)
		{
			_midiMappingElements = midiMappingElements ?? throw new ArgumentNullException(nameof(midiMappingElements));
			_midiMappingElements.ForEach(x => new MidiMappingElementValidator(x).Assert());
			Results = new List<MidiMappingCalculatorResult>();
		}

		public IList<MidiMappingCalculatorResult> Results { get; }

		public void Calculate(
			IList<(int midiControllerCode, int midiControllerValue)> midiControllerCodesAndValues,
			int? midiNoteNumber,
			int? midiVelocity)
		{
			Results.Clear();

			int midiMappingElementsCount = _midiMappingElements.Count;
			int midiControllerTupleCount = midiControllerCodesAndValues.Count;

			for (int midiMappingElementIndex = 0; midiMappingElementIndex < midiMappingElementsCount; midiMappingElementIndex++)
			{
				MidiMappingElement midiMappingElement = _midiMappingElements[midiMappingElementIndex];
				if (!midiMappingElement.IsActive)
				{
					continue;
				}

				bool mustScale = false;
				double ratio = 1.0;

				// Multiple MIDI Controller Values should be considered, but only one should be applied.
				for (int midiControllerTupleIndex = 0; midiControllerTupleIndex < midiControllerTupleCount; midiControllerTupleIndex++)
				{
					(int midiControllerCode, int midiControllerValue) = midiControllerCodesAndValues[midiControllerTupleIndex];

					if (MustScaleByMidiController(midiMappingElement, midiControllerCode, midiControllerValue))
					{
						double midiControllerRatio = (midiControllerValue - midiMappingElement.FromMidiControllerValue.Value) /
						                             (double)midiMappingElement.GetMidiControllerValueRange();
						ratio *= midiControllerRatio;
						mustScale = true;
						break;
					}
				}

				if (MustScaleByMidiNoteNumber(midiMappingElement, midiNoteNumber))
				{
					double midiNoteNumberRatio = (midiNoteNumber.Value - midiMappingElement.FromMidiNoteNumber.Value) /
					                             (double)midiMappingElement.GetMidiNoteNumberRange();
					ratio *= midiNoteNumberRatio;
					mustScale = true;
				}

				if (MustScaleByMidiVelocity(midiMappingElement, midiVelocity))
				{
					double midiVelocityRatio = (midiVelocity.Value - midiMappingElement.FromMidiVelocity.Value) /
					                           (double)midiMappingElement.GetMidiVelocityRange();
					ratio *= midiVelocityRatio;
					mustScale = true;
				}

				if (!mustScale)
				{
					continue;
				}

				double? destDimensionValue = null;
				if (midiMappingElement.HasDimensionValues())
				{
					destDimensionValue = GetScaledDimensionValue(midiMappingElement, ratio);
				}

				int? destPosition = null;
				if (midiMappingElement.HasPositions())
				{
					destPosition = GetScaledPosition(midiMappingElement, ratio);
				}

				int? destToneNumber = null;
				if (midiMappingElement.HasToneNumbers())
				{
					destToneNumber = GetScaledToneNumber(midiMappingElement, ratio);
				}

				Results.Add(
					new MidiMappingCalculatorResult(
						midiMappingElement.GetStandardDimensionEnum(),
						midiMappingElement.CustomDimensionName,
						destDimensionValue,
						destPosition,
						midiMappingElement.Scale,
						destToneNumber));
			}
		}

		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
		{
			int absoluteMidiControllerValue = inputMidiControllerValue;

			int midiMappingElementsCount = _midiMappingElements.Count;
			for (int i = 0; i < midiMappingElementsCount; i++)
			{
				MidiMappingElement midiMappingElement = _midiMappingElements[i];
				if (!midiMappingElement.IsActive)
				{
					continue;
				}

				if (!midiMappingElement.IsRelative)
				{
					continue;
				}

				if (!MustScaleByMidiController(midiMappingElement, midiControllerCode, inputMidiControllerValue))
				{
					continue;
				}

				int delta = inputMidiControllerValue - MIDDLE_CONTROLLER_VALUE;

				// Overriding mechanism: last applicable mapping wins.
				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
			}

			return absoluteMidiControllerValue;
		}

		private bool MustScaleByMidiController(MidiMappingElement midiMappingElement, int? midiControllerCode, int? midiControllerValue)
		{
			bool mustScaleByMidiController = midiControllerCode.HasValue &&
			                                 midiControllerValue.HasValue &&
			                                 midiMappingElement.HasMidiControllerValues() &&
			                                 midiMappingElement.MidiControllerCode == midiControllerCode;

			return mustScaleByMidiController;
		}

		private bool MustScaleByMidiNoteNumber(MidiMappingElement midiMappingElement, int? midiNoteNumber)
		{
			bool mustScaleByMidiNoteNumber = midiMappingElement.HasMidiNoteNumbers() &&
			                                 midiNoteNumber.HasValue;

			return mustScaleByMidiNoteNumber;
		}

		private bool MustScaleByMidiVelocity(MidiMappingElement midiMappingElement, int? midiVelocity)
		{
			bool mustScaleByMidiVelocity = midiMappingElement.HasMidiVelocities() &&
			                               midiVelocity.HasValue;

			return mustScaleByMidiVelocity;
		}

		private double GetScaledDimensionValue(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.GetDimensionValueRange();

			double destValue = ratio * destRange + midiMappingElement.FromDimensionValue.Value;

			if (destValue < midiMappingElement.MinDimensionValue)
			{
				destValue = midiMappingElement.MinDimensionValue.Value;
			}

			if (destValue > midiMappingElement.MaxDimensionValue)
			{
				destValue = midiMappingElement.MaxDimensionValue.Value;
			}

			return destValue;
		}

		private int GetScaledPosition(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.GetPositionRange();

			double destValueDouble = ratio * destRange + midiMappingElement.FromPosition.Value;

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}

		private int GetScaledToneNumber(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.GetToneNumberRange();

			double destValueDouble = ratio * destRange + midiMappingElement.FromToneNumber.Value;

			if (destValueDouble < 1)
			{
				destValueDouble = 1;
			}

			if (destValueDouble > midiMappingElement.Scale.Tones.Count)
			{
				destValueDouble = midiMappingElement.Scale.Tones.Count;
			}

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}
	}
}