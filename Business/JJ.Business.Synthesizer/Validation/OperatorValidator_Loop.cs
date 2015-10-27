using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Loop : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_Loop(Operator obj)
            : base(obj,
                OperatorTypeEnum.Loop, 4,
                PropertyNames.Start, PropertyNames.LoopStart, PropertyNames.LoopEnd, PropertyNames.End, 
                PropertyNames.Result)
        { }
    }
}
