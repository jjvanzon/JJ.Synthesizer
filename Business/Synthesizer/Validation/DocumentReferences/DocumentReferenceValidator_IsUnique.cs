using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_IsUnique : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_IsUnique([NotNull] DocumentReference obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            bool isUnique = ValidationHelper.DocumentReferenceIsUnique(Obj);

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
