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
            if (MustValidate())
            {
                Execute(new NameValidator(Object.Name));
            }
        }

        private bool MustValidate()
        {
            return Object.Document != null;
        }
    }
}
