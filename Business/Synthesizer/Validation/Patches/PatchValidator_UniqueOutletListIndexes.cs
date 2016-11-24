using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueOutletListIndexes : VersatileValidator<Patch>
    {
        public PatchValidator_UniqueOutletListIndexes(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool listIndexesAreUnique = ValidationHelper.PatchOutletListIndexesAreUniqueWithinPatch(Object);
            if (!listIndexesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletListIndexesAreNotUnique);
            }
        }
    }
}