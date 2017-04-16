using JetBrains.Annotations;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
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

            string lowerDocumentIdentifier = ResourceFormatter.Document + " " + ValidationHelper.GetUserFriendlyIdentifier(document);

            foreach (DocumentReference higherDocumentReference in document.HigherDocumentReferences)
            {
                string higherDocumentReferenceIdentifier = ResourceFormatter.HigherDocument + " " + ValidationHelper.GetUserFriendlyIdentifier_ForHigherDocumentReference(higherDocumentReference);
                string message = CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(lowerDocumentIdentifier, higherDocumentReferenceIdentifier);
                ValidationMessages.Add(PropertyNames.DocumentReference, message);
            }
        }
    }
}
