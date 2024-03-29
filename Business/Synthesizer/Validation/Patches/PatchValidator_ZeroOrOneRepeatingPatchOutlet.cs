﻿using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_ZeroOrOneRepeatingPatchOutlet : VersatileValidator
    {
        public PatchValidator_ZeroOrOneRepeatingPatchOutlet(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            int pseudoCount = patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                   .SelectMany(x => x.Outlets)
                                   .Where(x => x.IsRepeating)
                                   .Take(2)
                                   .Count();

            if (pseudoCount > 1)
            {
                Messages.Add(ResourceFormatter.PatchHasMoreThanOneRepeatingOutlet);
            }
        }
    }
}
