//using System;
//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer
//{
//	public class MidiMappingConverter
//	{
//		private readonly IPatchCalculator _destPatchCalculator;

//		/// <summary> For now these are assumed to have been validated. </summary>
//		private readonly IList<MidiMappingElement> _midiMappingElements;

//		public MidiMappingConverter(
//			IPatchCalculator destPatchCalculator,
//			IList<MidiMappingElement> midiMappingElements)
//		{
//			_destPatchCalculator = destPatchCalculator ?? throw new ArgumentNullException(nameof(destPatchCalculator));
//			_midiMappingElements = midiMappingElements ?? throw new ArgumentNullException(nameof(midiMappingElements));
//			_midiMappingElements.ForEach(x => new MidiMappingElementValidator(x).Assert());
//		}

//		// TODO: Tuple is too complicated? Better off making a custom tuple type?

//		public IList<(DimensionEnum standardDimensionEnum,
//				string customDimensionName,
//				double? dimensionValue,
//				int? position,
//				Scale scale,
//				int? toneNumber)>
//			Convert(
//				int? controllerCode,
//				int? controllerValue,
//				int? noteNumber,
//				int? velocity)
//		{
//			var list = new List<
//				(DimensionEnum standardDimensionEnum,
//				string customDimensionName,
//				double? dimensionValue,
//				int? position,
//				Scale scale,
//				int? toneNumber)>();

//			foreach (MidiMappingElement midiMappingElement in _midiMappingElements)
//			{
//				if (!midiMappingElement.IsActive)
//				{
//					continue;
//				}

//				double controllerRatio = 1.0;
//				if (MustScaleByController(midiMappingElement, controllerCode, controllerValue))
//				{
//					controllerRatio = (controllerValue.Value - midiMappingElement.FromControllerValue.Value) /
//					                  (midiMappingElement.TillControllerValue.Value - midiMappingElement.FromControllerValue.Value);
//				}

//				double noteNumberRatio = 1.0;
//				if (MustScaleByNoteNumber(midiMappingElement, noteNumber))
//				{
//					noteNumberRatio = (noteNumber.Value - midiMappingElement.FromNoteNumber.Value) /
//					                  (midiMappingElement.TillNoteNumber.Value - midiMappingElement.FromNoteNumber.Value);
//				}

//				double velocityRatio = 1.0;
//				if (MustScaleByVelocity(midiMappingElement, velocity))
//				{
//					velocityRatio = (velocity.Value - midiMappingElement.FromVelocity.Value) /
//					                (midiMappingElement.TillVelocity.Value - midiMappingElement.FromVelocity.Value);
//				}

//				double ratio = controllerRatio * noteNumberRatio * velocityRatio;

//				// TODO: Refactor so the scale functions take the ratio instead of the controllerValue.

//				if (midiMappingElement.FromDimensionValue.HasValue &&
//				    midiMappingElement.TillDimensionValue.HasValue)
//				{
//					double destDimensionValue = ScaleDimensionValue(controllerValue.Value, midiMappingElement);
//				}

//				if (midiMappingElement.FromPosition.HasValue &&
//				    midiMappingElement.TillPosition.HasValue)
//				{
//					double destPosition = ScalePosition(controllerValue.Value, midiMappingElement);
//				}

//				if (midiMappingElement.Scale != null &&
//				    midiMappingElement.FromToneNumber.HasValue &&
//				    midiMappingElement.TillToneNumber.HasValue)
//				{
//					double destToneNumber = ScaleToneNumber(controllerValue.Value, midiMappingElement);
//				}

//				// TODO: Add a result to the list.
//			}

//			return list;
//		}

//		private bool MustScaleByController(MidiMappingElement midiMappingElement, int? controllerCode, int? controllerValue)
//		{
//			if (!controllerCode.HasValue)
//			{
//				return false;
//			}

//			if (!controllerValue.HasValue)
//			{
//				return false;
//			}

//			if (midiMappingElement.ControllerCode != controllerCode)
//			{
//				return false;
//			}

//			if (!midiMappingElement.FromControllerValue.HasValue ||
//			    !midiMappingElement.TillControllerValue.HasValue)
//			{
//				return false;
//			}

//			return true;
//		}

//		private bool MustScaleByNoteNumber(MidiMappingElement midiMappingElement, int? noteNumber)
//		{
//			throw new NotImplementedException();
//		}

//		private bool MustScaleByVelocity(MidiMappingElement midiMappingElement, int? velocity)
//		{
//			throw new NotImplementedException();
//		}

//		// TODO: These methods only work for controller value based scaling,
//		// not for note number or velocity ased scaling.
//		private static double ScaleDimensionValue(int sourceControllerValue, MidiMappingElement midiMappingElement)
//		{
//			double destDimensionValue = MathHelper.ScaleLinearly(
//				sourceControllerValue,
//				midiMappingElement.FromControllerValue.Value,
//				midiMappingElement.TillControllerValue.Value,
//				midiMappingElement.FromDimensionValue.Value,
//				midiMappingElement.TillDimensionValue.Value);

//			// TODO: Work with relative deltas too.

//			if (destDimensionValue < midiMappingElement.MinDimensionValue)
//			{
//				destDimensionValue = midiMappingElement.MinDimensionValue.Value;
//			}

//			if (destDimensionValue > midiMappingElement.MaxDimensionValue)
//			{
//				destDimensionValue = midiMappingElement.MaxDimensionValue.Value;
//			}

//			return destDimensionValue;
//		}

//		private static int ScalePosition(int sourceControllerValue, MidiMappingElement midiMappingElement)
//		{
//			double destPositionDouble = MathHelper.ScaleLinearly(
//				sourceControllerValue,
//				midiMappingElement.FromControllerValue.Value,
//				midiMappingElement.TillControllerValue.Value,
//				midiMappingElement.FromPosition.Value,
//				midiMappingElement.TillPosition.Value);

//			int destPosition = (int)Math.Round(destPositionDouble, MidpointRounding.AwayFromZero);

//			return destPosition;
//		}

//		private static int ScaleToneNumber(int sourceControllerValue, MidiMappingElement midiMappingElement)
//		{
//			double destToneNumberDouble = MathHelper.ScaleLinearly(
//				sourceControllerValue,
//				midiMappingElement.FromControllerValue.Value,
//				midiMappingElement.TillControllerValue.Value,
//				midiMappingElement.FromToneNumber.Value,
//				midiMappingElement.TillToneNumber.Value);

//			if (destToneNumberDouble < 1)
//			{
//				destToneNumberDouble = 1;
//			}

//			if (destToneNumberDouble > midiMappingElement.Scale.Tones.Count)
//			{
//				destToneNumberDouble = midiMappingElement.Scale.Tones.Count;
//			}

//			int destToneNumber = (int)Math.Round(destToneNumberDouble, MidpointRounding.AwayFromZero);

//			return destToneNumber;
//		}
//	}
//}