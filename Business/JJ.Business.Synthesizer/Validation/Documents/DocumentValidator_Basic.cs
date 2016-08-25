using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class DocumentValidator_Basic : FluentValidator<Document>
    {
        public DocumentValidator_Basic(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.AudioOutput, PropertyDisplayNames.AudioOutput).NotNull();

            ExecuteValidator(new NameValidator(Object.Name), ValidationHelper.GetMessagePrefix(Object));
        }
    }
}
