using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MaxFollower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MaxFollower_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
