using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_WhiteNoise : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_WhiteNoise(Operator obj)
            : base(obj, OperatorTypeEnum.WhiteNoise, 0, PropertyNames.Result)
        { }
    }
}
