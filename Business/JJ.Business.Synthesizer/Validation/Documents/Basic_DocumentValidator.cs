using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class Basic_DocumentValidator : FluentValidator<Document>
    {
        public Basic_DocumentValidator(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Document document = Object;

            Execute(new NameValidator(document.Name), ValidationHelper.GetMessagePrefix(document));
        }
    }
}
