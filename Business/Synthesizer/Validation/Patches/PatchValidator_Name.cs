using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Name : VersatileValidator<Patch>
    {
        public PatchValidator_Name(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (MustValidate())
            {
                ExecuteValidator(new NameValidator(Object.Name));
            }
        }

        private bool MustValidate()
        {
            return Object.Document != null;
        }
    }
}
