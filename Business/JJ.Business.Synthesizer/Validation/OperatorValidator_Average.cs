using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Average : OperatorValidator_Base_Aggregate
    {
        public OperatorValidator_Average(Operator obj)
            : base(obj, OperatorTypeEnum.Average)
        { }
    }
}
