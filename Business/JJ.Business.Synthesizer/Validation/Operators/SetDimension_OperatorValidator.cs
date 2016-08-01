using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SetDimension_OperatorValidator : OperatorValidator_Base
    {
        public SetDimension_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SetDimension, expectedDataKeys: new string[0], expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
