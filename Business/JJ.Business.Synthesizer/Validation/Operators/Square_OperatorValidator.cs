using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Square_OperatorValidator : OperatorValidator_Base
    {
        public Square_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Square, expectedDataKeys: new string[0], expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}