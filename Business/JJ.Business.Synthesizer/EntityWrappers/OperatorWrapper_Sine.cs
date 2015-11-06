using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Sine : OperatorWrapperBase
    {
        public OperatorWrapper_Sine(Operator op)
            : base(op)
        { }

        public Outlet Pitch
        {
            get { return GetInlet(OperatorConstants.SINE_PITCH_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_PITCH_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return GetInlet(OperatorConstants.SINE_PHASE_SHIFT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SINE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Sine wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}