using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_UniqueLowerDocument : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_UniqueLowerDocument([NotNull] DocumentReference obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            bool isUnique = ValidationHelper.DocumentReference_LowerDocument_IsUnique(Obj);

            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                string lowerDocumentReferenceIdentifier = ValidationHelper.GetUserFriendlyIdentifier_ForLowerDocumentReference(Obj);
                string message = ResourceFormatter.LibraryAlreadyAdded_WithName(lowerDocumentReferenceIdentifier);
                ValidationMessages.Add(nameof(DocumentReference.LowerDocument), message);
            }
        }
    }
}
