using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reverse_OperatorValidator : OperatorValidator_Base
    {
        public Reverse_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Reverse,
                  expectedInletCount: 2,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}