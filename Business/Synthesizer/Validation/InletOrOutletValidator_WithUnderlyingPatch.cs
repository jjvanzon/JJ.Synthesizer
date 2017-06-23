using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletOrOutletValidator_WithUnderlyingPatch : VersatileValidator
    {
        public InletOrOutletValidator_WithUnderlyingPatch(IInletOrOutlet inletOrOutlet)
        {
            if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

            ExecuteValidator(
                new RepeatInfoValidator(
                    inletOrOutlet.Operator.GetOperatorTypeEnum(),
                    inletOrOutlet.IsRepeating,
                    inletOrOutlet.RepetitionPosition));
        }
    }
}
