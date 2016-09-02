using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OneOverX_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public OneOverX_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.OneOverX, 
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined })
        { }
    }
}
