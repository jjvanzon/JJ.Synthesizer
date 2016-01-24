using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Shift : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Shift(Operator obj)
            : base(obj, OperatorTypeEnum.Shift, expectedInletCount: 2, expectedOutletCount: 1)
        { }
    }
}