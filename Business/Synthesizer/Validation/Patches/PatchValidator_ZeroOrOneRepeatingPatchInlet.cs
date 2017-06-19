using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_ZeroOrOneRepeatingPatchInlet : VersatileValidator<Patch>
    {
        public PatchValidator_ZeroOrOneRepeatingPatchInlet([NotNull] Patch obj) : base(obj)
        {
            
        }
    }
}
