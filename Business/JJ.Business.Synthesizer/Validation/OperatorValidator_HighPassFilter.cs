using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_HighPassFilter : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_HighPassFilter(Operator obj)
            : base(obj, OperatorTypeEnum.HighPassFilter, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
