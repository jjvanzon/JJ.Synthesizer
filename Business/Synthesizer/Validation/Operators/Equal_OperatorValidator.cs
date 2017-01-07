using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Equal_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Equal_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Equal,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
