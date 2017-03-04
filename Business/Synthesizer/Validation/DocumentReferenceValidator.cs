using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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

            ExecuteValidator(new NameValidator(documentReference.Alias, PropertyDisplayNames.Alias, required: false));

            For(() => documentReference.HigherDocument, PropertyDisplayNames.HigherDocument).NotNull();
            For(() => documentReference.LowerDocument, PropertyDisplayNames.LowerDocument).NotNull();
        }
    }
}
