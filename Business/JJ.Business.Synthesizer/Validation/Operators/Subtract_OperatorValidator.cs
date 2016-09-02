using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Subtract_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Subtract_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Subtract, 
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
