using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Resample : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Resample(Operator obj)
            : base(obj, OperatorTypeEnum.Resample, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
