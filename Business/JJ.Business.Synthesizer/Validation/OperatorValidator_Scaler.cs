using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Scaler : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Scaler(Operator obj)
            : base(obj, OperatorTypeEnum.Scaler, expectedInletCount: 5, expectedOutletCount: 1)
        { }
    }
}
