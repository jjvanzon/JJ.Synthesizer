using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SumContinuous_OperatorWrapper : OperatorWrapperBase_ContinuousAggregate
    {
        public SumContinuous_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(SumContinuous_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}