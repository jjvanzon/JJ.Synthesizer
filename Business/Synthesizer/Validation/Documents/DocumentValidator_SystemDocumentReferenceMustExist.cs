using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_SystemDocumentReferenceMustExist : VersatileValidator
	{
		public DocumentValidator_SystemDocumentReferenceMustExist(Document document, RepositoryWrapper repositories)
		{
			if (document == null) throw new NullException(() => document);

			var documentFacade = new DocumentFacade(repositories);

			if (document.IsSystemDocument())
			{
				return;
			}

			bool hasSystemDocumentReference = document.LowerDocumentReferences.Any(x => x.LowerDocument.IsSystemDocument());
			if (!hasSystemDocumentReference)
			{
				string systemDocumentIdentifier = ValidationHelper.GetUserFriendlyIdentifier(documentFacade.GetSystemDocument());

				Messages.AddNotContainsMessage(ResourceFormatter.Libraries, systemDocumentIdentifier);

			}
		}
	}
}
