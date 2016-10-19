using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Reset_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Reset_OperatorWarningValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Name, CommonTitles.Name).NotNullOrWhiteSpace();
        }
    }
}
