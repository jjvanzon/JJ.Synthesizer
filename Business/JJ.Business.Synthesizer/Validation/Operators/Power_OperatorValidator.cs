using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Power_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Power_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Power, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
