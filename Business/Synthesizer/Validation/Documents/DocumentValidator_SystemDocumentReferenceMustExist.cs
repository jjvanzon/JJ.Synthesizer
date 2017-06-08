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
        private readonly DocumentManager _documentManager;

        public DocumentValidator_SystemDocumentReferenceMustExist([NotNull] Document obj, RepositoryWrapper repositories)
            : base(obj, postponeExecute: true)
        {
            _documentManager = new DocumentManager(repositories);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            Document document = Obj;

            if (_documentManager.IsSystemDocument(document))
            {
                return;
            }

            bool hasSystemDocumentReference = document.LowerDocumentReferences.Any(x => _documentManager.IsSystemDocument(x.LowerDocument));
            if (!hasSystemDocumentReference)
            {
                string systemDocumentIdentifier = ValidationHelper.GetUserFriendlyIdentifier(_documentManager.GetSystemDocument());

                ValidationMessages.AddNotContainsMessage(nameof(DocumentReference), ResourceFormatter.Libraries, systemDocumentIdentifier);

            }
        }
    }
}
