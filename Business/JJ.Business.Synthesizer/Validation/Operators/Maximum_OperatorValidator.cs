using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Maximum_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Maximum_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Maximum, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
