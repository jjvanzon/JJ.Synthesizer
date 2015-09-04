using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Divide : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Divide(Operator obj)
            : base(obj,
                OperatorTypeEnum.Divide, 3, 
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin, 
                PropertyNames.Result)
        { }
    }
}
