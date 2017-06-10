using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_SystemDocumentReferenceMustExist : VersatileValidator<Document>
    {
        public DocumentValidator_SystemDocumentReferenceMustExist([NotNull] Document document, RepositoryWrapper repositories)
            : base(document)
        {
            var documentManager = new DocumentManager(repositories);

            if (documentManager.IsSystemDocument(document))
            {
                return;
            }

            bool hasSystemDocumentReference = document.LowerDocumentReferences.Any(x => documentManager.IsSystemDocument(x.LowerDocument));
            if (!hasSystemDocumentReference)
            {
                string systemDocumentIdentifier = ValidationHelper.GetUserFriendlyIdentifier(documentManager.GetSystemDocument());

                ValidationMessages.AddNotContainsMessage(nameof(DocumentReference), ResourceFormatter.Libraries, systemDocumentIdentifier);

            }
        }
    }
}
