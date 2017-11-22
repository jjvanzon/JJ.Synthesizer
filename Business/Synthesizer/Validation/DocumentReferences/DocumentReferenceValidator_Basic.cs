using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
	internal class DocumentReferenceValidator_Basic : VersatileValidator
	{
		public DocumentReferenceValidator_Basic(DocumentReference documentReference) 
		{
			if (documentReference == null) throw new NullException(() => documentReference);

			ExecuteValidator(new NameValidator(documentReference.Alias, ResourceFormatter.Alias, required: false));

			For(documentReference.HigherDocument, ResourceFormatter.HigherDocument).NotNull();
			For(documentReference.LowerDocument, ResourceFormatter.Library).NotNull();
		}
	}
}
