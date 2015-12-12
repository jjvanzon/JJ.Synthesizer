using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Resample_OperatorWrapper : OperatorWrapperBase
    {
        public Resample_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.RESAMPLE_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.RESAMPLE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet SamplingRate
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.RESAMPLE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Resample_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
