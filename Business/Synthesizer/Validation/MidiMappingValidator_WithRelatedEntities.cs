using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingValidator_WithRelatedEntities : VersatileValidator
	{
		public MidiMappingValidator_WithRelatedEntities(MidiMapping entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			ExecuteValidator(new MidiMappingValidator_Basic(entity));

			foreach (MidiMappingElement midiMappingElement in entity.MidiMappingElements)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(midiMappingElement);
				ExecuteValidator(new MidiMappingElementValidator(midiMappingElement), messagePrefix);
			}
		}
	}
}
