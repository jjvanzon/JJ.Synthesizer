using System;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class MidiMappingGroupWarningValidator_WithRelatedEntities : VersatileValidator
	{
		public MidiMappingGroupWarningValidator_WithRelatedEntities(MidiMappingGroup entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			foreach (MidiMappingElement midiMappingElement in entity.MidiMappingElements)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(midiMappingElement);
				ExecuteValidator(new MidiMappingElementWarningValidator(midiMappingElement), messagePrefix);
			}
		}
	}
}
