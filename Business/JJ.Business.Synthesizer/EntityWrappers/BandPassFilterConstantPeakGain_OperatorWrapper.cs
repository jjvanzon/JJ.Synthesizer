using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class BandPassFilterConstantPeakGain_OperatorWrapper : OperatorWrapperBase_FilterWithFrequencyAndBandWidth
    {
        public BandPassFilterConstantPeakGain_OperatorWrapper(Operator op)
            : base(op)
        { }

        public static implicit operator Outlet(BandPassFilterConstantPeakGain_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}