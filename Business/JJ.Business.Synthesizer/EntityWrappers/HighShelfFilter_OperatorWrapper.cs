using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class HighShelfFilter_OperatorWrapper : OperatorWrapperBase_ShelfFilter
    {
        public HighShelfFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(HighShelfFilter_OperatorWrapper wrapper) => wrapper?.Result;
    }
}