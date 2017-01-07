using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class If_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public If_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.If,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
