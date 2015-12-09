using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SawTooth_OperatorWrapper : OperatorWrapperBase
    {
        public SawTooth_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get { return GetInlet(OperatorConstants.SAW_TOOTH_FREQUENCY_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SAW_TOOTH_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return GetInlet(OperatorConstants.SAW_TOOTH_PHASE_SHIFT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SAW_TOOTH_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SAW_TOOTH_RESULT_INDEX); }
        }

        public static implicit operator Outlet(SawTooth_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}