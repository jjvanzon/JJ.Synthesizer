using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Range_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Range_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Range, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
