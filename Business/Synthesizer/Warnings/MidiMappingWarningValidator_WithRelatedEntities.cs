using System;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class MidiMappingWarningValidator_WithRelatedEntities : VersatileValidator
	{
		public MidiMappingWarningValidator_WithRelatedEntities(MidiMapping entity)
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
