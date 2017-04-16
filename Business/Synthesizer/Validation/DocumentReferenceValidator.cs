using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentReferenceValidator : VersatileValidator<DocumentReference>
    {
        public DocumentReferenceValidator(DocumentReference obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            DocumentReference documentReference = Obj;

            ExecuteValidator(new NameValidator(documentReference.Alias, ResourceFormatter.Alias, required: false));

            For(() => documentReference.HigherDocument, ResourceFormatter.HigherDocument).NotNull();
            For(() => documentReference.LowerDocument, ResourceFormatter.LowerDocument).NotNull();
        }
    }
}
