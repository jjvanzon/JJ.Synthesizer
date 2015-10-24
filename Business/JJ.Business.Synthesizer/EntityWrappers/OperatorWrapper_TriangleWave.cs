using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_TriangleWave : OperatorWrapperBase
    {
        public OperatorWrapper_TriangleWave(Operator op)
            : base(op)
        { }

        public Outlet Pitch
        {
            get { return GetInlet(OperatorConstants.TRIANGLE_WAVE_PITCH_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TRIANGLE_WAVE_PITCH_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return GetInlet(OperatorConstants.TRIANGLE_WAVE_PHASE_SHIFT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TRIANGLE_WAVE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TRIANGLE_WAVE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_TriangleWave wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}