using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class NotchFilter_OperatorWrapper : OperatorWrapperBase_FilterWithFrequencyAndBandWidth
    {
        public NotchFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(NotchFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}