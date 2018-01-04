using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class MidiMappingElementWarningValidator : VersatileValidator
	{
		public MidiMappingElementWarningValidator(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			if (entity.IsRelative)
			{
				For(entity.FromControllerValue, ResourceFormatter.FromControllerValue)
					.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
					.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);

				For(entity.TillControllerValue, ResourceFormatter.TillControllerValue)
					.GreaterThanOrEqual(MidiConstants.MIDI_MIN_VALUE)
					.LessThanOrEqual(MidiConstants.MIDI_MAX_VALUE);
			}

			For(entity.FromPosition, ResourceFormatter.FromPosition).GreaterThanOrEqual(0);
			For(entity.TillPosition, ResourceFormatter.TillPosition).GreaterThanOrEqual(0);
		}
	}
}