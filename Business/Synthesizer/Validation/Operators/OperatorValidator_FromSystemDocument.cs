using JetBrains.Annotations;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_FromSystemDocument : OperatorValidator_Base_WithUnderlyingPatch
    {
        public OperatorValidator_FromSystemDocument([NotNull] Operator op)
            : base(op)
        {
            For(() => op.OperatorType, ResourceFormatter.OperatorType).IsNull();
        }
    }
}
