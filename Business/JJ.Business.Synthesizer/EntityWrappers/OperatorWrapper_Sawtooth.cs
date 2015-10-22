using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_SawTooth : OperatorWrapperBase
    {
        public OperatorWrapper_SawTooth(Operator op)
            : base(op)
        { }

        public Outlet Pitch
        {
            get { return GetInlet(OperatorConstants.SAW_TOOTH_PITCH_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SAW_TOOTH_PITCH_INDEX).LinkTo(value); }
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

        public static implicit operator Outlet(OperatorWrapper_SawTooth wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}