using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
	internal class DocumentValidator_SystemDocumentReferenceMustExist : VersatileValidator
	{
		public DocumentValidator_SystemDocumentReferenceMustExist(Document document, IDocumentRepository documentRepository)
		{
			if (document == null) throw new NullException(() => document);

			var systemFacade = new SystemFacade(documentRepository);

			if (document.IsSystemDocument())
			{
				return;
			}

			bool hasSystemDocumentReference = document.LowerDocumentReferences.Any(x => x.LowerDocument.IsSystemDocument());
			if (!hasSystemDocumentReference)
			{
				string systemDocumentIdentifier = ValidationHelper.GetUserFriendlyIdentifier(systemFacade.GetSystemDocument());

				Messages.AddNotContainsMessage(ResourceFormatter.Libraries, systemDocumentIdentifier);
			}
		}
	}
}