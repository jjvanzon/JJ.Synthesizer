using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Reset : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Reset(Operator obj)
            : base(obj, OperatorTypeEnum.Reset, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}