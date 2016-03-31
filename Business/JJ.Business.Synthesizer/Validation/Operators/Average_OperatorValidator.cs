using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Average_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Average_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Average, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
