using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OneOverX_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public OneOverX_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.OneOverX, 
                new[] { DimensionEnum.Number },
                new[] { DimensionEnum.Result })
        { }
    }
}
