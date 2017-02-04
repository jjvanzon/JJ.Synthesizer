using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Delete : VersatileValidator<Document>
    {
        public DocumentValidator_Delete(Document obj)
            : base(obj, postponeExecute: true)
        {
            Execute();
        }

        protected sealed override void Execute()
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
