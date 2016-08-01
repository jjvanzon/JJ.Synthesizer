using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Select_OperatorValidator : OperatorValidator_Base
    {
        public Select_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Select, expectedDataKeys: new string[0], expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}