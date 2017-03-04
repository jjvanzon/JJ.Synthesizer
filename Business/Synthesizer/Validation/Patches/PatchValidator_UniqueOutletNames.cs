using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

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
                ValidationMessages.Add(PropertyNames.PatchOutlet, ResourceFormatter.OutletNamesAreNotUnique);
            }
        }
    }
}
