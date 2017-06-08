using System;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_SystemDocumentReferenceMustExist : VersatileValidator<Document>
    {
        private readonly SystemDocumentManager _systemDocumentManager;

        public DocumentValidator_SystemDocumentReferenceMustExist([NotNull] Document obj, IDocumentRepository documentRepository, bool postponeExecute = true)
            : base(obj, postponeExecute)
        {
            _systemDocumentManager = new SystemDocumentManager(documentRepository);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            Document document = Obj;

            if (_systemDocumentManager.IsSystemDocument(document))
            {
                return;
            }

            bool hasSystemDocumentReference = document.LowerDocumentReferences.Any(x => _systemDocumentManager.IsSystemDocument(x.LowerDocument));
            if (!hasSystemDocumentReference)
            {
                string systemDocumentIdentifier = ValidationHelper.GetUserFriendlyIdentifier(_systemDocumentManager.GetSystemDocument());

                ValidationMessages.AddNotContainsMessage(nameof(DocumentReference), ResourceFormatter.Libraries, systemDocumentIdentifier);

            }
        }
    }
}
