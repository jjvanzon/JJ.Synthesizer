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

			For(entity.MidiMapping, ResourceFormatter.MidiMapping).NotNull();

			ValidateMidiControllerProperties(entity);
			ValidateMidiNoteNumberProperties(entity);
			ValidateMidiVelocityProperties(entity);
			ValidateDimensionProperties(entity);
			ValidateToneNumberProperties(entity);
			ValidatePositionProperties(entity);
		}

		private void ValidateMidiControllerProperties(MidiMappingElement entity)
		{
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
			else
			{
				For(entity.FromMidiControllerValue, ResourceFormatter.FromMidiControllerValue).IsNull();
				For(entity.TillMidiControllerValue, ResourceFormatter.TillMidiControllerValue).IsNull();
			}
		}

		private void ValidateMidiNoteNumberProperties(MidiMappingElement entity)
		{
			For(entity.FromMidiNoteNumber, ResourceFormatter.FromMidiNoteNumber)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiNoteNumber, ResourceFormatter.TillMidiNoteNumber)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			bool fromNotEmptyButTillIsEmpty = entity.FromMidiNoteNumber.HasValue && !entity.TillMidiNoteNumber.HasValue;
			if (fromNotEmptyButTillIsEmpty)
			{
				For(entity.TillMidiNoteNumber, ResourceFormatter.TillMidiNoteNumber).NotNull();
			}

			bool tillNotEmptyButFromIsEmpty = entity.TillMidiNoteNumber.HasValue && !entity.FromMidiNoteNumber.HasValue;
			if (tillNotEmptyButFromIsEmpty)
			{
				For(entity.FromMidiNoteNumber, ResourceFormatter.FromMidiNoteNumber).NotNull();
			}
		}

		private void ValidateMidiVelocityProperties(MidiMappingElement entity)
		{
			For(entity.FromMidiVelocity, ResourceFormatter.FromMidiVelocity)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			For(entity.TillMidiVelocity, ResourceFormatter.TillMidiVelocity)
				.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
				.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

			bool fromNotEmptyButTillIsEmpty = entity.FromMidiVelocity.HasValue && !entity.TillMidiVelocity.HasValue;
			if (fromNotEmptyButTillIsEmpty)
			{
				For(entity.TillMidiVelocity, ResourceFormatter.TillMidiVelocity).NotNull();
			}

			bool tillNotEmptyButFromIsEmpty = entity.TillMidiVelocity.HasValue && !entity.FromMidiVelocity.HasValue;
			if (tillNotEmptyButFromIsEmpty)
			{
				For(entity.FromMidiVelocity, ResourceFormatter.FromMidiVelocity).NotNull();
			}
		}

		private void ValidateDimensionProperties(MidiMappingElement entity)
		{
			For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNaN().NotInfinity();
			For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNaN().NotInfinity();
			For(entity.MinDimensionValue, ResourceFormatter.MinDimensionValue).NotNaN().NotInfinity();
			For(entity.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).NotNaN().NotInfinity();

			bool fromNotEmptyButTillIsEmpty = entity.FromDimensionValue.HasValue && !entity.TillDimensionValue.HasValue;
			if (fromNotEmptyButTillIsEmpty)
			{
				For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNull();
			}

			bool tillNotEmptyButFromIsEmpty = entity.TillDimensionValue.HasValue && !entity.FromDimensionValue.HasValue;
			if (tillNotEmptyButFromIsEmpty)
			{
				For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNull();
			}

			bool dimensionHasMinOrMaxButNoFromOrTill = (entity.MinDimensionValue.HasValue || entity.MaxDimensionValue.HasValue) &&
			                                           !(entity.FromDimensionValue.HasValue || entity.TillDimensionValue.HasValue);
			if (dimensionHasMinOrMaxButNoFromOrTill)
			{
				Messages.Add(ResourceFormatter.HasDimensionMinMaxButNoFromOrTill);
			}
		}

		private void ValidateToneNumberProperties(MidiMappingElement entity)
		{
			For(entity.FromToneNumber, ResourceFormatter.FromToneNumber).GreaterThan(0);
			For(entity.TillToneNumber, ResourceFormatter.TillToneNumber).GreaterThan(0);

			bool fromNotEmptyButTillIsEmpty = entity.FromToneNumber.HasValue && !entity.TillToneNumber.HasValue;
			if (fromNotEmptyButTillIsEmpty)
			{
				For(entity.TillToneNumber, ResourceFormatter.TillToneNumber).NotNull();
			}

			bool tillNotEmptyButFromIsEmpty = entity.TillToneNumber.HasValue && !entity.FromToneNumber.HasValue;
			if (tillNotEmptyButFromIsEmpty)
			{
				For(entity.FromToneNumber, ResourceFormatter.FromToneNumber).NotNull();
			}

			// Do not validate inconsistently filled in Scale and FromToneNumber/TillToneNumber,
			// because the plan is to fall back to a default.
		}

		private void ValidatePositionProperties(MidiMappingElement entity)
		{
			bool fromNotEmptyButTillIsEmpty = entity.FromPosition.HasValue && !entity.TillPosition.HasValue;
			if (fromNotEmptyButTillIsEmpty)
			{
				For(entity.TillPosition, ResourceFormatter.TillPosition).NotNull();
			}

			bool tillNotEmptyButFromIsEmpty = entity.TillPosition.HasValue && !entity.FromPosition.HasValue;
			if (tillNotEmptyButFromIsEmpty)
			{
				For(entity.FromPosition, ResourceFormatter.FromPosition).NotNull();
			}

			// The ranges of these are validated in warning validators:
			// - FromPosition
			// - TillPosition 
		}
	}
}