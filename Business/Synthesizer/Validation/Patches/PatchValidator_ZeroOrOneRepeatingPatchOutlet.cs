using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_ZeroOrOneRepeatingPatchOutlet : VersatileValidator
    {
        public PatchValidator_ZeroOrOneRepeatingPatchOutlet([NotNull] Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            int pseudoCount = patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                   .SelectMany(x => x.Outlets)
                                   .Where(x => x.IsRepeating)
                                   .Take(2)
                                   .Count();

            if (pseudoCount > 1)
            {
                ValidationMessages.Add(nameof(Patch), ResourceFormatter.PatchHasMoreThanOneRepeatingOutlet);
            }
        }
    }
}
