using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class TimePower_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public TimePower_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.TimePower, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
