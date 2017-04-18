using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DocumentReferences
{
    internal class DocumentReferenceValidator_UniqueAlias : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator_UniqueAlias(DocumentReference obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool isUnique = ValidationHelper.DocumentReferenceAliasIsUnique(Obj);

            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(DocumentReference.Alias), ResourceFormatter.Alias, Obj.Alias);
            }
        }
    }
}
