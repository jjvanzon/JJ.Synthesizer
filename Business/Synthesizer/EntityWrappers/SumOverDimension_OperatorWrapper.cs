using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SumOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public SumOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}