using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class GetDimension_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public GetDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.GetDimension, expectedInletCount: 0, expectedOutletCount: 1)
        { }
    }
}
