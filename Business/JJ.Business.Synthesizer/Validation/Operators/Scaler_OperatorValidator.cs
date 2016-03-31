using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Scaler_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Scaler_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Scaler, expectedInletCount: 5, expectedOutletCount: 1)
        { }
    }
}
