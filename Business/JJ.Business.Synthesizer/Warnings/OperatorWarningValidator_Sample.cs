using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Sample : OperatorWarningValidator_Base_FirstXInletsNotFilledIn
    {
        public OperatorWarningValidator_Sample(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Data, PropertyDisplayNames.Sample)
                .NotNull();
        }
    }
}
