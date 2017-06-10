using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Reset_OperatorWarningValidator : OperatorWarningValidator_Base_AllInletsFilledInOrHaveDefaults
    {
        public Reset_OperatorWarningValidator(Operator obj)
            : base(obj)
        { 
            For(() => obj.Name, CommonResourceFormatter.Name).NotNullOrWhiteSpace();
        }
    }
}
