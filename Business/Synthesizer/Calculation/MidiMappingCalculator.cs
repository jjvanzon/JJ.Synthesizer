using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer.Calculation
{
	public class MidiMappingCalculator
	{
		public class Result
		{
			public Result(
				DimensionEnum standardDimensionEnum,
				string customDimensionName,
				double? dimensionValue,
				int? position,
				Scale scale,
				int? toneNumber)
			{
				StandardDimensionEnum = standardDimensionEnum;
				CustomDimensionName = customDimensionName;
				DimensionValue = dimensionValue;
				Position = position;
				Scale = scale;
				ToneNumber = toneNumber;
			}

			public DimensionEnum StandardDimensionEnum { get; }
			public string CustomDimensionName { get; }
			public double? DimensionValue { get; }
			public int? Position { get; }
			public Scale Scale { get; }
			public int? ToneNumber { get; }
		}

		private readonly IList<MidiMappingElement> _midiMappingElements;

		public MidiMappingCalculator(IList<MidiMappingElement> midiMappingElements)
		{
			_midiMappingElements = midiMappingElements ?? throw new ArgumentNullException(nameof(midiMappingElements));
			_midiMappingElements.ForEach(x => new MidiMappingElementValidator(x).Assert());
		}

		public int ToAbsoluteControllerValue(int midiControllerCode, int inputMidiControllerValue, int previousAbsoluteMidiControllerValue)
		{
			int absoluteMidiControllerValue = inputMidiControllerValue;

			foreach (MidiMappingElement midiMappingElement in _midiMappingElements)
			{
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

				int delta = inputMidiControllerValue - 64;

				// Overriding mechanism: last applicable mapping wins.
				absoluteMidiControllerValue = previousAbsoluteMidiControllerValue + delta;
			}

			return absoluteMidiControllerValue;
		}

		public IList<Result> Calculate(int? midiControllerCode, int? midiControllerValue, int? midiNoteNumber, int? midiVelocity)
		{
			var list = new List<Result>();

			foreach (MidiMappingElement midiMappingElement in _midiMappingElements)
			{
				if (!midiMappingElement.IsActive)
				{
					continue;
				}

				double ratio = 1.0;

				if (MustScaleByMidiController(midiMappingElement, midiControllerCode, midiControllerValue))
				{
					double midiControllerRatio = (midiControllerValue.Value - midiMappingElement.FromMidiControllerValue.Value) /
					                             (double)(midiMappingElement.TillMidiControllerValue.Value -
					                                      midiMappingElement.FromMidiControllerValue.Value);

					ratio *= midiControllerRatio;
				}

				if (MustScaleByMidiNoteNumber(midiMappingElement, midiNoteNumber))
				{
					double midiNoteNumberRatio = (midiNoteNumber.Value - midiMappingElement.FromMidiNoteNumber.Value) /
					                             (double)(midiMappingElement.TillMidiNoteNumber.Value - midiMappingElement.FromMidiNoteNumber.Value);

					ratio *= midiNoteNumberRatio;
				}

				if (MustScaleByMidiVelocity(midiMappingElement, midiVelocity))
				{
					double midiVelocityRatio = (midiVelocity.Value - midiMappingElement.FromMidiVelocity.Value) /
					                           (double)(midiMappingElement.TillMidiVelocity.Value - midiMappingElement.FromMidiVelocity.Value);

					ratio *= midiVelocityRatio;
				}

				double? destDimensionValue = null;
				if (MustScaleDimension(midiMappingElement))
				{
					destDimensionValue = GetScaledDimensionValue(midiMappingElement, ratio);
				}

				int? destPosition = null;
				if (MustScalePosition(midiMappingElement))
				{
					destPosition = GetScaledPosition(midiMappingElement, ratio);
				}

				int? destToneNumber = null;
				if (MustScaleToneNumber(midiMappingElement))
				{
					destToneNumber = GetScaledToneNumber(midiMappingElement, ratio);
				}

				list.Add(
					new Result(
						midiMappingElement.GetStandardDimensionEnum(),
						midiMappingElement.CustomDimensionName,
						destDimensionValue,
						destPosition,
						midiMappingElement.Scale,
						destToneNumber));
			}

			return list;
		}

		private bool MustScaleByMidiController(MidiMappingElement midiMappingElement, int? midiControllerCode, int? midiControllerValue)
		{
			if (!midiControllerCode.HasValue)
			{
				return false;
			}

			if (!midiControllerValue.HasValue)
			{
				return false;
			}

			if (midiMappingElement.MidiControllerCode != midiControllerCode)
			{
				return false;
			}

			if (!midiMappingElement.FromMidiControllerValue.HasValue ||
			    !midiMappingElement.TillMidiControllerValue.HasValue)
			{
				return false;
			}

			return true;
		}

		private bool MustScaleByMidiNoteNumber(MidiMappingElement midiMappingElement, int? midiNoteNumber)
		{
			bool mustScaleByMidiNoteNumber = midiMappingElement.Scale != null &&
			                                 midiMappingElement.FromMidiNoteNumber.HasValue &&
			                                 midiMappingElement.TillMidiNoteNumber.HasValue &&
			                                 midiNoteNumber.HasValue;

			return mustScaleByMidiNoteNumber;
		}

		private bool MustScaleByMidiVelocity(MidiMappingElement midiMappingElement, int? midiVelocity)
		{
			bool mustScaleByMidiVelocity = midiMappingElement.FromMidiVelocity.HasValue &&
			                               midiMappingElement.TillMidiVelocity.HasValue &&
			                               midiVelocity.HasValue;

			return mustScaleByMidiVelocity;
		}

		private bool MustScalePosition(MidiMappingElement midiMappingElement)
		{
			bool mustScalePosition = midiMappingElement.FromPosition.HasValue &&
			                         midiMappingElement.TillPosition.HasValue;

			return mustScalePosition;
		}

		private bool MustScaleDimension(MidiMappingElement midiMappingElement)
		{
			bool mustScaleDimension = midiMappingElement.FromDimensionValue.HasValue &&
			                          midiMappingElement.TillDimensionValue.HasValue;

			return mustScaleDimension;
		}

		private bool MustScaleToneNumber(MidiMappingElement midiMappingElement)
		{
			bool mustScaleToneNumber = midiMappingElement.Scale != null &&
			                           midiMappingElement.FromToneNumber.HasValue &&
			                           midiMappingElement.TillToneNumber.HasValue;

			return mustScaleToneNumber;
		}

		private double GetScaledDimensionValue(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.TillDimensionValue.Value -
			                   midiMappingElement.FromDimensionValue.Value;

			double destValue = ratio * destRange + midiMappingElement.TillDimensionValue.Value;

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
			double destRange = midiMappingElement.TillPosition.Value -
			                   midiMappingElement.FromPosition.Value;

			double destValueDouble = ratio * destRange + midiMappingElement.TillPosition.Value;

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}

		private int GetScaledToneNumber(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.TillToneNumber.Value -
			                   midiMappingElement.FromToneNumber.Value;

			double destValueDouble = ratio * destRange + midiMappingElement.TillPosition.Value;

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