using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MinFollower_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}
