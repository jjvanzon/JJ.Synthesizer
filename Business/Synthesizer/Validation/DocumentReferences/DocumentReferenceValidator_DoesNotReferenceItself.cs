using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_DoesNotReferenceItself : VersatileValidator
    {
        public DocumentReferenceValidator_DoesNotReferenceItself(DocumentReference entity)
        {
            if (entity == null) throw new NullException(() => entity);

            // ReSharper disable once InvertIf
            if (MustValidate(entity))
            {
                DocumentReference documentReference = entity;
                bool referencesItself = documentReference.HigherDocument.ID == documentReference.LowerDocument.ID;
                if (referencesItself)
                {
                    Messages.Add(ResourceFormatter.DocumentCannotReferenceItself);
                }
            }
        }

        private static bool MustValidate(DocumentReference documentReference) => documentReference.HigherDocument != null &&
                                                                                 documentReference.LowerDocument != null;
    }
}
