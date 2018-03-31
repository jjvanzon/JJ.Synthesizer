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

			foreach (MidiMappingElement midiMappingElement in entity.MidiMappingElements)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(midiMappingElement);
				ExecuteValidator(new MidiMappingElementValidator(midiMappingElement), messagePrefix);
			}
		}
	}
}
