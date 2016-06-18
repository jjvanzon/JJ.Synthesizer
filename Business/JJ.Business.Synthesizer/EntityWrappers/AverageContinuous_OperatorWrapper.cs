using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageContinuous_OperatorWrapper : OperatorWrapperBase_ContinuousAggregate
    {
        public AverageContinuous_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AverageContinuous_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}