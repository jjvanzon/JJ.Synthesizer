using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public AverageFollower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AverageFollower_OperatorWrapper wrapper) => wrapper?.Result;
    }
}
