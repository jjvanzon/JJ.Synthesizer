using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_WithUnderlyingPatch : VersatileValidator
    {
        public OutletValidator_WithUnderlyingPatch([NotNull] Outlet outlet)
        {
            if (outlet == null) throw new NullException(() => outlet);

            ExecuteValidator(
                new RepeatInfoValidator(
                    outlet.Operator.GetOperatorTypeEnum(),
                    outlet.IsRepeating,
                    outlet.RepetitionPosition));
        }
    }
}
