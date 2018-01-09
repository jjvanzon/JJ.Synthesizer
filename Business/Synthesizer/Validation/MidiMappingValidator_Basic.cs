using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingValidator_Basic : VersatileValidator
	{
		public MidiMappingValidator_Basic(MidiMapping entity)
		{
			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Name));

			For(entity.Document, ResourceFormatter.Document).NotNull();
		}
	}
}
