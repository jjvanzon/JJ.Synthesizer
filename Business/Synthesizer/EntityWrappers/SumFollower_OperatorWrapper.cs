using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SumFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public SumFollower_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}
