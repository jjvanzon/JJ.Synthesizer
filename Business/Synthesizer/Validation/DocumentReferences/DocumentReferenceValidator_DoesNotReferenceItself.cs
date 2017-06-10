using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_DoesNotReferenceItself : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_DoesNotReferenceItself([NotNull] DocumentReference entity)
            : base(entity)
        { 
            // ReSharper disable once InvertIf
            if (MustValidate(entity))
            {
                DocumentReference documentReference = entity;
                bool referencesItself = documentReference.HigherDocument.ID == documentReference.LowerDocument.ID;
                if (referencesItself)
                {
                    ValidationMessages.Add(nameof(DocumentReference), ResourceFormatter.DocumentCannotReferenceItself);
                }
            }
        }

        private static bool MustValidate(DocumentReference documentReference)
        {
            return documentReference.HigherDocument != null &&
                   documentReference.LowerDocument != null;
        }
    }
}
