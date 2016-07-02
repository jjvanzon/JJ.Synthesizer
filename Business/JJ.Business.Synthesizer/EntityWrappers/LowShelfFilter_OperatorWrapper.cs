using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class LowShelfFilter_OperatorWrapper : OperatorWrapperBase_ShelfFilter
    {
        public LowShelfFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(LowShelfFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}