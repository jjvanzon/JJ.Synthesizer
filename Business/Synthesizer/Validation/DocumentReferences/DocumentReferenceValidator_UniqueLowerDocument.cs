using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_UniqueLowerDocument : VersatileValidator
    {
        public DocumentReferenceValidator_UniqueLowerDocument(DocumentReference obj) 
        {
            if (obj == null) throw new NullException(() => obj);

            bool isUnique = ValidationHelper.DocumentReference_LowerDocument_IsUnique(obj);

            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                string lowerDocumentReferenceIdentifier = ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(obj);
                string message = ResourceFormatter.LibraryAlreadyAdded_WithName(lowerDocumentReferenceIdentifier);
                Messages.Add(message);
            }
        }
    }
}
