using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Triangle : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Triangle(Operator obj)
            : base(obj, OperatorTypeEnum.Triangle, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}