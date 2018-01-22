using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer
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

		public IList<Result> Calculate(int? controllerCode, int? controllerValue, int? noteNumber, int? velocity)
		{
			var list = new List<Result>();

			foreach (MidiMappingElement midiMappingElement in _midiMappingElements)
			{
				if (!midiMappingElement.IsActive)
				{
					continue;
				}

				double controllerRatio = 1.0;
				if (MustScaleByController(midiMappingElement, controllerCode, controllerValue))
				{
					controllerRatio = (controllerValue.Value - midiMappingElement.FromControllerValue.Value) /
					                  (double)(midiMappingElement.TillControllerValue.Value - midiMappingElement.FromControllerValue.Value);
				}

				double noteNumberRatio = 1.0;
				if (MustScaleByNoteNumber(midiMappingElement, noteNumber))
				{
					noteNumberRatio = (noteNumber.Value - midiMappingElement.FromNoteNumber.Value) /
					                  (double)(midiMappingElement.TillNoteNumber.Value - midiMappingElement.FromNoteNumber.Value);
				}

				double velocityRatio = 1.0;
				if (MustScaleByVelocity(midiMappingElement, velocity))
				{
					velocityRatio = (velocity.Value - midiMappingElement.FromVelocity.Value) /
					                (double)(midiMappingElement.TillVelocity.Value - midiMappingElement.FromVelocity.Value);
				}

				double ratio = controllerRatio * noteNumberRatio * velocityRatio;

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

		private bool MustScaleByController(MidiMappingElement midiMappingElement, int? controllerCode, int? controllerValue)
		{
			if (!controllerCode.HasValue)
			{
				return false;
			}

			if (!controllerValue.HasValue)
			{
				return false;
			}

			if (midiMappingElement.ControllerCode != controllerCode)
			{
				return false;
			}

			if (!midiMappingElement.FromControllerValue.HasValue ||
			    !midiMappingElement.TillControllerValue.HasValue)
			{
				return false;
			}

			return true;
		}

		private bool MustScaleByNoteNumber(MidiMappingElement midiMappingElement, int? noteNumber)
		{
			return midiMappingElement.Scale != null &&
			       midiMappingElement.FromNoteNumber.HasValue &&
			       midiMappingElement.TillNoteNumber.HasValue &&
			       noteNumber.HasValue;
		}

		private bool MustScaleByVelocity(MidiMappingElement midiMappingElement, int? velocity)
		{
			return midiMappingElement.FromVelocity.HasValue &&
			       midiMappingElement.TillVelocity.HasValue &&
			       velocity.HasValue;
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

			// TODO: Work with relative deltas too.

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

			// TODO: Work with relative deltas too.

			int destValue = (int)Math.Round(destValueDouble, MidpointRounding.AwayFromZero);

			return destValue;
		}

		private int GetScaledToneNumber(MidiMappingElement midiMappingElement, double ratio)
		{
			double destRange = midiMappingElement.TillToneNumber.Value -
			                   midiMappingElement.FromToneNumber.Value;

			double destValueDouble = ratio * destRange + midiMappingElement.TillPosition.Value;

			// TODO: Work with relative deltas too.

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