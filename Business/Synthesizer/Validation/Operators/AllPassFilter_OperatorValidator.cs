using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AllPassFilter_OperatorValidator : OperatorValidator_Base
    {
        public AllPassFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.AllPassFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound },
                expectedDataKeys: new string[0])
        { }
    }
}
