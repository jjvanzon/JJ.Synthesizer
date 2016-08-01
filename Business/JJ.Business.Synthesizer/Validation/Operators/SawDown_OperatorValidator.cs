using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawDown_OperatorValidator : OperatorValidator_Base
    {
        public SawDown_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.SawDown,
                expectedDataKeys: new string[0],
                expectedInletCount: 2,
                expectedOutletCount: 1)
        { }
    }
}