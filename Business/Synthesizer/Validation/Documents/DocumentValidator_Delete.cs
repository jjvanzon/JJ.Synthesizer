using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Delete : VersatileValidator
    {
        public DocumentValidator_Delete([NotNull] Document document)
        {
            if (document == null) throw new NullException(() => document);

            string lowerDocumentIdentifier = ResourceFormatter.Document + " " + ValidationHelper.GetUserFriendlyIdentifier(document);

            foreach (DocumentReference higherDocumentReference in document.HigherDocumentReferences)
            {
                string higherDocumentReferenceIdentifier = ResourceFormatter.HigherDocument + " " + ValidationHelper.GetUserFriendlyIdentifier_ForHigherDocumentReference(higherDocumentReference);
                string message = CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(lowerDocumentIdentifier, higherDocumentReferenceIdentifier);
                Messages.Add(message);
            }
        }
    }
}
