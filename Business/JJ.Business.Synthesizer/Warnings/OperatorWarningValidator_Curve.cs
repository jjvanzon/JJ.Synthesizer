using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Curve : OperatorWarningValidator_Base
    {
        public OperatorWarningValidator_Curve(Operator op)
            : base(op)
        { }

        protected override void Execute()
        {
            For(() => Object.Data, PropertyDisplayNames.Curve)
                .NotNull();
        }
    }
}
