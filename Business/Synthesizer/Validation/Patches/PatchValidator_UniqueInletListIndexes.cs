using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueInletListIndexes : VersatileValidator<Patch>
    {
        public PatchValidator_UniqueInletListIndexes(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            bool listIndexesAreUnique = ValidationHelper.PatchInletListIndexesAreUniqueWithinPatch(Object);
            if (!listIndexesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchInlet, Messages.InletListIndexesAreNotUnique);
            }
        }
    }
}
