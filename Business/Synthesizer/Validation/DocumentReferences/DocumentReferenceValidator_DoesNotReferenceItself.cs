using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_DoesNotReferenceItself : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_DoesNotReferenceItself([NotNull] DocumentReference obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            // ReSharper disable once InvertIf
            if (MustValidate(Obj))
            {
                DocumentReference documentReference = Obj;
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
