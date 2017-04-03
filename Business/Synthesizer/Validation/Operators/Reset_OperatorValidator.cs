using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reset_OperatorValidator : OperatorValidator_Base
    {
        public Reset_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Reset,
                new[] { DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined },
                expectedDataKeys: new[] { PropertyNames.ListIndex })
        { }
    }
}