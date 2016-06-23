using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MinOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public MinOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MinOverDimension_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}