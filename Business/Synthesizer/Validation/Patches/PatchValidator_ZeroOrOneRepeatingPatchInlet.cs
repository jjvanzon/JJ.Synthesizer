using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_ZeroOrOneRepeatingPatchInlet : VersatileValidator
    {
        public PatchValidator_ZeroOrOneRepeatingPatchInlet(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            int pseudoCount = patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                   .SelectMany(x => x.Inlets)
                                   .Where(x => x.IsRepeating)
                                   .Take(2)
                                   .Count();

            if (pseudoCount > 1)
            {
                Messages.Add(ResourceFormatter.PatchHasMoreThanOneRepeatingInlet);
            }
        }
    }
}
