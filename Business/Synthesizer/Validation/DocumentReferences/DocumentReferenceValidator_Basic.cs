using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
	internal class DocumentReferenceValidator_Basic : VersatileValidator
	{
		public DocumentReferenceValidator_Basic(DocumentReference entity) 
		{
			if (entity == null) throw new NullException(() => entity);

			ExecuteValidator(new IDValidator(entity.ID));
			ExecuteValidator(new NameValidator(entity.Alias, ResourceFormatter.Alias, required: false));

			For(entity.HigherDocument, ResourceFormatter.HigherDocument).NotNull();
			For(entity.LowerDocument, ResourceFormatter.Library).NotNull();
		}
	}
}
