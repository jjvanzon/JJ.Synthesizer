using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_UniqueAlias : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_UniqueAlias(DocumentReference obj)
            : base(obj)
        { 
            bool isUnique = ValidationHelper.DocumentReferenceAliasIsUnique(obj);

            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(DocumentReference.Alias), ResourceFormatter.Alias, obj.Alias);
            }
        }
    }
}
