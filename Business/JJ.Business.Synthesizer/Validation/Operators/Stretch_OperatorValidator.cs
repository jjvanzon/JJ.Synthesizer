using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Stretch_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Stretch_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Stretch, expectedInletCount: 3, expectedOutletCount: 1)
        { }
    }
}
