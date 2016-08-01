using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Loop : OperatorValidator_Base
    {
        public OperatorValidator_Loop(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.Loop,
                expectedDataKeys: new string[0],
                expectedInletCount: 6,
                expectedOutletCount: 1)
        { }
    }
}