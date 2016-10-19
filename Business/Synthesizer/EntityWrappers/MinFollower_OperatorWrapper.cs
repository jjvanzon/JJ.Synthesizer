using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MinFollower_OperatorWrapper(Operator op)
            : base(op)
        { }
    }
}
