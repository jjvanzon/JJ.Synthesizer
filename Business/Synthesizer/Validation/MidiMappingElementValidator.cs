using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingElementValidator : VersatileValidator
	{
		public MidiMappingElementValidator(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new NameValidator(entity.CustomDimensionName, ResourceFormatter.CustomDimensionName));

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);
			For(entity.FromNoteNumber, ResourceFormatter.FromNoteNumber).GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE).LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			For(entity.TillNoteNumber, ResourceFormatter.TillNoteNumber).GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE).LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			For(entity.ControllerCode, ResourceFormatter.ControllerCode).GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE).LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			For(entity.FromVelocity, ResourceFormatter.FromVelocity).GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE).LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			For(entity.TillVelocity, ResourceFormatter.TillVelocity).GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE).LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNaN().NotInfinity();
			For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNaN().NotInfinity();
			For(entity.MinDimensionValue, ResourceFormatter.MinDimensionValue).NotNaN().NotInfinity();
			For(entity.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).NotNaN().NotInfinity();
			For(entity.FromToneNumber, ResourceFormatter.FromToneNumber).GreaterThan(0);
			For(entity.TillToneNumber, ResourceFormatter.TillToneNumber).GreaterThan(0);
			For(entity.MidiMapping, ResourceFormatter.MidiMapping).NotNull();

			bool hasControllerCodeButNoValue = entity.ControllerCode.HasValue && !entity.FromControllerValue.HasValue && !entity.TillControllerValue.HasValue;
			if (hasControllerCodeButNoValue)
			{
				Messages.Add(ResourceFormatter.HasControllerCodeButNoControllerValue);
			}

			bool hasControllerValueButNoCode = (entity.FromControllerValue.HasValue || entity.TillControllerValue.HasValue) && !entity.ControllerCode.HasValue;
			if (hasControllerValueButNoCode)
			{
				Messages.Add(ResourceFormatter.HasControllerValueButNoControllerCode);
			}

			bool dimensionHasMinOrMaxButNoFromnOrTill = (entity.MinDimensionValue.HasValue || entity.MaxDimensionValue.HasValue) &&
			                                            !(entity.FromDimensionValue.HasValue || entity.TillDimensionValue.HasValue);
			if (dimensionHasMinOrMaxButNoFromnOrTill)
			{
				Messages.Add(ResourceFormatter.HasDimensionMinMaxButNoFromOrTill);
			}

			// Do not validate inconsistently filled in Scale and FromToneNumber/TillToneNumber,
			// because the plan is to fall back to a default.

			// The ranges of these are validated in warning validators.
			// - FromControllerValue
			// - TillControllerValue
			// - FromPosition
			// - TillPosition 
		}
	}
}