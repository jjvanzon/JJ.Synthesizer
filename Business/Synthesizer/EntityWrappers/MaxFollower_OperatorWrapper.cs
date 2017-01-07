using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MaxFollower_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}
