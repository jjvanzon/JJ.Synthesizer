using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Resample : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Resample(Operator obj)
            : base(obj, 
                OperatorTypeEnum.Resample, 2, 
                PropertyNames.Signal, PropertyNames.SamplingRate, 
                PropertyNames.Result)
        { }
    }
}
