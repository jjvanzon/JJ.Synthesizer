using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Resample : OperatorWrapperBase
    {
        public OperatorWrapper_Resample(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.RESAMPLE_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.RESAMPLE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet SamplingRate
        {
            get { return GetInlet(OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.RESAMPLE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Resample wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
