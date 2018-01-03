using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_Basic : VersatileValidator
	{
		public DocumentValidator_Basic(Document entity)
		{
			if (entity == null) throw new NullException(() => entity);

			For(entity.ID, CommonResourceFormatter.ID).GreaterThan(0);
			For(entity.AudioOutput, ResourceFormatter.AudioOutput).NotNull();

			ExecuteValidator(new NameValidator(entity.Name), ValidationHelper.GetMessagePrefix(entity));
		}
	}
}
