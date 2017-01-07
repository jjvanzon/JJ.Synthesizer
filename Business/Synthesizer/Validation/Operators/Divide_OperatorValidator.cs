using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Divide_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Divide_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Divide,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
