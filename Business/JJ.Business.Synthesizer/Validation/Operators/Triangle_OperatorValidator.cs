using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Triangle_OperatorValidator : OperatorValidator_Base
    {
        public Triangle_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Triangle, expectedInletCount: 2, expectedOutletCount: 1, expectedDataKeys: new string[0])
        { }
    }
}