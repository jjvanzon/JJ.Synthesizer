using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Exponent_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Exponent_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Exponent,
                  new[] { DimensionEnum.Low, DimensionEnum.High, DimensionEnum.Ratio },
                  new[] { DimensionEnum.Result })
        { }
    }
}
