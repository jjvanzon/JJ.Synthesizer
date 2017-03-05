using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueOutletNames : VersatileValidator<Patch>
    {
        public PatchValidator_UniqueOutletNames(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool namesAreUnique = ValidationHelper.PatchOutletNamesAreUniqueWithinPatch(Obj);
            if (!namesAreUnique)
            {
                string message = ResourceFormatter.Outlets + ": " + ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names);
                ValidationMessages.Add(PropertyNames.PatchOutlet, message);
            }
        }
    }
}
