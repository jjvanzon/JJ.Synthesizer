//using System;
//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.CopiedCode.FromFramework;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Collections;

//namespace JJ.Business.Synthesizer
//{
//	public class MidiToDimensionConverter
//	{
//		private readonly IPatchCalculator _destPatchCalculator;

//		/// <summary> For now these are assumed to have been validated. </summary>
//		private readonly IList<MidiMappingElement> _midiMappingElements;

//		public MidiToDimensionConverter(
//			IPatchCalculator destPatchCalculator, 
//			IList<MidiMappingElement> midiMappingElements)
//		{
//			_destPatchCalculator = destPatchCalculator ?? throw new ArgumentNullException(nameof(destPatchCalculator));
//			_midiMappingElements = midiMappingElements ?? throw new ArgumentNullException(nameof(midiMappingElements));
//			_midiMappingElements.ForEach(x => new MidiMappingElementValidator(x).Assert());
//		}

//		public void Convert(int sourceControllerCode, int sourceControllerValue)
//		{
//			foreach(MidiMappingElement midiMappingElement in _midiMappingElements)
//			{
//				if (!midiMappingElement.IsActive)
//				{
//					continue;
//				}

//				if (midiMappingElement.ControllerCode != sourceControllerCode)
//				{
//					continue;
//				}

//				if (!midiMappingElement.FromControllerValue.HasValue ||
//				    !midiMappingElement.TillControllerValue.HasValue)
//				{
//					continue;
//				}

//				if (midiMappingElement.FromDimensionValue.HasValue && 
//				    midiMappingElement.TillDimensionValue.HasValue)
//				{
//					double destDimensionValue = ScaleDimensionValue(sourceControllerValue, midiMappingElement);
//				}

//				if (midiMappingElement.FromPosition.HasValue &&
//				    midiMappingElement.TillPosition.HasValue)
//				{
//					double destPosition = ScalePosition(sourceControllerValue, midiMappingElement);
//				}

//				if (midiMappingElement.Scale != null &&
//				    midiMappingElement.FromToneNumber.HasValue &&
//				    midiMappingElement.TillToneNumber.HasValue)
//				{
//					double destToneNumber = ScaleToneNumber(sourceControllerValue, midiMappingElement);
//				}
//			}
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
