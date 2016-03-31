using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Not_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Not_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Not, expectedInletCount: 1, expectedOutletCount: 1)
        { }
    }
}
