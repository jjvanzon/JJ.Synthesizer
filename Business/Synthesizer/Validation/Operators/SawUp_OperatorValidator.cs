using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SawUp_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public SawUp_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.SawUp,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Signal })
        { }
    }
}