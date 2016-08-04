using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SumOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public SumOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(SumOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}