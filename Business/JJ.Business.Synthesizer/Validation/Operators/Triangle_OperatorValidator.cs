using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Triangle_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Triangle_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Triangle, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}