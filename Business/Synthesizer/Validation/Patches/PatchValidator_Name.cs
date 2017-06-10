using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Name : VersatileValidator<Patch>
    {
        public PatchValidator_Name(Patch obj)
            : base(obj)
        {
            bool mustValidate = obj.Document != null;
            if (mustValidate)
            {
                ExecuteValidator(new NameValidator(obj.Name));
            }
        }
    }
}
