using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueOutletNames : FluentValidator<Patch>
    {
        public PatchValidator_UniqueOutletNames(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool namesAreUnique = ValidationHelper.PatchOutletNamesAreUniqueWithinPatch(Object);
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletNamesAreNotUnique);
            }
        }
    }
}
