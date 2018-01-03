using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class MidiMappingValidator_Basic : VersatileValidator
	{
		public MidiMappingValidator_Basic(MidiMapping entity)
		{
			ExecuteValidator(new NameValidator(entity.Name, required: true));

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);
			For(entity.Document, ResourceFormatter.Document).NotNull();
		}
	}
}
