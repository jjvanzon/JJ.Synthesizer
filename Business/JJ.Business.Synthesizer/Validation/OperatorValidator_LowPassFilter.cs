using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_LowPassFilter : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_LowPassFilter(Operator obj)
            : base(obj, OperatorTypeEnum.LowPassFilter, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
