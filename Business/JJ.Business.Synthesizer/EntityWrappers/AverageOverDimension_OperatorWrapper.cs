using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AverageOverDimension_OperatorWrapper : OperatorWrapperBase_AggregateOverDimension
    {
        public AverageOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AverageOverDimension_OperatorWrapper wrapper) => wrapper?.Result;
    }
}