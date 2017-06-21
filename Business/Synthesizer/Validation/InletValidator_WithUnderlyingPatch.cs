using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletValidator_WithUnderlyingPatch : VersatileValidator
    {
        public InletValidator_WithUnderlyingPatch([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            ExecuteValidator(
                new RepeatInfoValidator(
                    inlet.Operator.GetOperatorTypeEnum(),
                    inlet.IsRepeating,
                    inlet.RepetitionPosition));
        }
    }
}
