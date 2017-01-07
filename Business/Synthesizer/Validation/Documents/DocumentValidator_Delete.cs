using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Delete : VersatileValidator<Document>
    {
        private IPatchRepository _patchRepository;

        public DocumentValidator_Delete(Document obj, IPatchRepository patchRepository)
            : base(obj, postponeExecute: true)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Execute();
        }

        protected override void Execute()
        {
            Document document = Object;

            foreach (DocumentReference dependentDocument in document.DependentDocuments)
            {
                string message = MessageFormatter.DocumentIsDependentOnDocument(dependentDocument.DependentDocument.Name, dependentDocument.DependentOnDocument.Name);
                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
