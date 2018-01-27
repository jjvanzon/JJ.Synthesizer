using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingElementValidator : VersatileValidator
	{
		public MidiMappingElementValidator(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.CustomDimensionName, ResourceFormatter.CustomDimensionName, required: false));

			For(entity.EntityPosition, ResourceFormatter.EntityPosition).NotNull();
			if (entity.EntityPosition != null)
			{
				ExecuteValidator(new EntityPositionValidator(entity.EntityPosition), ValidationHelper.GetMessagePrefix(entity.EntityPosition));
			}

			For(entity.MidiControllerCode, ResourceFormatter.MidiControllerCode)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.FromMidiControllerValue, ResourceFormatter.FromMidiControllerValue)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiControllerValue, ResourceFormatter.TillMidiControllerValue)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			if (entity.MidiControllerCode.HasValue)
			{
				For(entity.FromMidiControllerValue, ResourceFormatter.FromMidiControllerValue).NotNull();
				For(entity.TillMidiControllerValue, ResourceFormatter.TillMidiControllerValue).NotNull();
			}

			For(entity.FromMidiNoteNumber, ResourceFormatter.FromMidiNoteNumber)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiNoteNumber, ResourceFormatter.TillMidiNoteNumber)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.FromMidiVelocity, ResourceFormatter.FromMidiVelocity)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiVelocity, ResourceFormatter.TillMidiVelocity)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNaN().NotInfinity();
			For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNaN().NotInfinity();
			For(entity.MinDimensionValue, ResourceFormatter.MinDimensionValue).NotNaN().NotInfinity();
			For(entity.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).NotNaN().NotInfinity();
			For(entity.FromToneNumber, ResourceFormatter.FromToneNumber).GreaterThan(0);
			For(entity.TillToneNumber, ResourceFormatter.TillToneNumber).GreaterThan(0);
			For(entity.MidiMapping, ResourceFormatter.MidiMapping).NotNull();

			bool hasControllerCodeButNoValue =
				entity.MidiControllerCode.HasValue && !entity.FromMidiControllerValue.HasValue && !entity.TillMidiControllerValue.HasValue;

			if (hasControllerCodeButNoValue)
			{
				Messages.Add(ResourceFormatter.HasMidiControllerCodeButNoMidiControllerValue);
			}

			bool hasControllerValueButNoCode =
				(entity.FromMidiControllerValue.HasValue || entity.TillMidiControllerValue.HasValue) && !entity.MidiControllerCode.HasValue;

			if (hasControllerValueButNoCode)
			{
				Messages.Add(ResourceFormatter.HasMidiControllerValueButNoMidiControllerCode);
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
			// - FromPosition
			// - TillPosition 
		}
	}
}