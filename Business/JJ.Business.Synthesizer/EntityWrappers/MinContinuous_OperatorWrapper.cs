using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinContinuous_OperatorWrapper : OperatorWrapperBase_ContinuousAggregate
    {
        public MinContinuous_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MinContinuous_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}