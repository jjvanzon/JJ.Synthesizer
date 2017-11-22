using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_Basic : VersatileValidator
	{
		public DocumentValidator_Basic(Document obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.AudioOutput, ResourceFormatter.AudioOutput).NotNull();

			ExecuteValidator(new NameValidator(obj.Name), ValidationHelper.GetMessagePrefix(obj));
		}
	}
}
