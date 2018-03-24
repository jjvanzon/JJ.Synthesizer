using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_Basic : VersatileValidator
	{
		public DocumentValidator_Basic(Document entity)
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Name), ValidationHelper.GetMessagePrefix(entity));

			For(entity.AudioOutput, ResourceFormatter.AudioOutput).NotNull();
		}
	}
}
