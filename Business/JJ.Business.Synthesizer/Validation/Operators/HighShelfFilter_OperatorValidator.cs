using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class HighShelfFilter_OperatorValidator : OperatorValidator_Base
    {
        public HighShelfFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.HighShelfFilter,
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}