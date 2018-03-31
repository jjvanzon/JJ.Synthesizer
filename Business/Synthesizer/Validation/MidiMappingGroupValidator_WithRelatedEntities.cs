using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingGroupValidator_WithRelatedEntities : VersatileValidator
	{
		public MidiMappingGroupValidator_WithRelatedEntities(MidiMappingGroup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new MidiMappingGroupValidator_Basic(entity));

			foreach (MidiMapping midiMapping in entity.MidiMappings)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(midiMapping);
				ExecuteValidator(new MidiMappingValidator(midiMapping), messagePrefix);
			}
		}
	}
}
