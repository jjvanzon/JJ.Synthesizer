using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingGroupValidator_Basic : VersatileValidator
	{
		public MidiMappingGroupValidator_Basic(MidiMappingGroup entity)
		{
			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Name));

			For(entity.Document, ResourceFormatter.Document).NotNull();
		}
	}
}
