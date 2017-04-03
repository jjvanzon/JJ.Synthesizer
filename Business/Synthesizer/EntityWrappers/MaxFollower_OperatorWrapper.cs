using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MaxFollower_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}
