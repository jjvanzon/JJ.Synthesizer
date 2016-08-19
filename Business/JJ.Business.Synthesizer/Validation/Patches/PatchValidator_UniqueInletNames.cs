using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueInletNames : FluentValidator<Patch>
    {
        public PatchValidator_UniqueInletNames(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool namesAreUnique = ValidationHelper.PatchInletNamesAreUniqueWithinPatch(Object);
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchInlet, Messages.InletNamesAreNotUnique);
            }
        }
    }
}
