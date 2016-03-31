using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SpeedUp_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SpeedUp_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SpeedUp, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
