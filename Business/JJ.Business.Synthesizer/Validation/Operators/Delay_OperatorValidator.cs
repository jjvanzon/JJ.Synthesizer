using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Delay_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Delay_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Delay, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}