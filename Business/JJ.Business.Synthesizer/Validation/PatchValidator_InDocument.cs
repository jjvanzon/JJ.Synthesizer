using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class PatchValidator_InDocument : FluentValidator<Patch>
    {
        public PatchValidator_InDocument(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Document, PropertyDisplayNames.Document).NotNull();

            Execute(new NameValidator(Object.Name));
        }
    }
}
