using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Reset : OperatorWarningValidator_Base_AllInletsFilled
    {
        public OperatorWarningValidator_Reset(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Name, CommonTitles.Name).NotNullOrWhiteSpace();
        }
    }
}
