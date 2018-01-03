using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingElementValidator : VersatileValidator
	{
		private const int MIDI_MIN_VALUE = 0;
		private const int MIDI_MAX_VALUE = 127;

		public MidiMappingElementValidator(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new NameValidator(entity.CustomDimensionName, ResourceFormatter.CustomDimensionName));

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);
			For(entity.FromNoteNumber, ResourceFormatter.FromNoteNumber).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.TillNoteNumber, ResourceFormatter.TillNoteNumber).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.ControllerCode, ResourceFormatter.FromControllerValue).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.ControllerCode, ResourceFormatter.TillControllerValue).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.FromVelocity, ResourceFormatter.FromVelocity).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.TillVelocity, ResourceFormatter.TillVelocity).GreaterThanOrEqual(MIDI_MIN_VALUE).LessThanOrEqual(MIDI_MAX_VALUE);
			For(entity.FromDimensionValue, ResourceFormatter.FromDimensionValue).NotNaN().NotInfinity();
			For(entity.TillDimensionValue, ResourceFormatter.TillDimensionValue).NotNaN().NotInfinity();
			For(entity.MinDimensionValue, ResourceFormatter.MinDimensionValue).NotNaN().NotInfinity();
			For(entity.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).NotNaN().NotInfinity();
			For(entity.FromToneNumber, ResourceFormatter.FromToneNumber).GreaterThan(0);
			For(entity.TillToneNumber, ResourceFormatter.TillToneNumber).GreaterThan(0);
			For(entity.MidiMapping, ResourceFormatter.MidiMapping).NotNull();

			// FromPosition and TillPosition bounds are not very important to be strict about.
		}
	}
}