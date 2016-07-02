using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PeakingEQFilter_OperatorValidator : OperatorValidator_Base
    {
        public PeakingEQFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.PeakingEQFilter,
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[0])
        { }
    }
}