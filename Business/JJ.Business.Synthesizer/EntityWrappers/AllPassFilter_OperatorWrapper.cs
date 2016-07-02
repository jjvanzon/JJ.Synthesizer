using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class AllPassFilter_OperatorWrapper : OperatorWrapperBase_FilterWithFrequencyAndBandWidth
    {
        public AllPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(AllPassFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}