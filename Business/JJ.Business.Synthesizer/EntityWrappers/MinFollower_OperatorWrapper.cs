using System;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinFollower_OperatorWrapper : OperatorWrapperBase_AggregateFollower
    {
        public MinFollower_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MinFollower_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
