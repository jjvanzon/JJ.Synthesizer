using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reset_OperatorValidator : OperatorValidator_Base_WithOperatorType
    {
        public Reset_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Reset,
                new[] { DimensionEnum.PassThrough },
                new[] { DimensionEnum.PassThrough },
                expectedDataKeys: new[] { nameof(Reset_OperatorWrapper.ListIndex) })
        { }
    }
}