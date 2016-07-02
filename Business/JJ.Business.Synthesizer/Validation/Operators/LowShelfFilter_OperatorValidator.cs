using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LowShelfFilter_OperatorValidator : OperatorValidator_Base
    {
        public LowShelfFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.LowShelfFilter,
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}