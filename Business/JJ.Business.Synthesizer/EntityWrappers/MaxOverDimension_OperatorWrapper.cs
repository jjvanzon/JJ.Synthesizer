using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class MaxOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public MaxOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(MaxOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}