using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Select_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Select_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Select, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}