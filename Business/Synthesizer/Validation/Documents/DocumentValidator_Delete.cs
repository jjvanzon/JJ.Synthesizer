using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Delete : VersatileValidator<Document>
    {
        public DocumentValidator_Delete([NotNull] Document obj)
            : base(obj)
        { }

        protected sealed override void Execute()
        {
            Document document = Obj;

            foreach (DocumentReference higherDocumentReference in document.HigherDocumentReferences)
            {
                string message = ResourceFormatter.DocumentIsDependentOnDocument(
                    higherDocumentReference.HigherDocument.Name, 
                    higherDocumentReference.LowerDocument.Name);

                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
