using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Add : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Add(Operator obj)
            : base(obj, OperatorTypeEnum.Add, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}
