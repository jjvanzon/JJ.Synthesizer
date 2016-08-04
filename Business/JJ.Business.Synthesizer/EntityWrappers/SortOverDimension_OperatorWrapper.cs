using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SortOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public SortOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(SortOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}