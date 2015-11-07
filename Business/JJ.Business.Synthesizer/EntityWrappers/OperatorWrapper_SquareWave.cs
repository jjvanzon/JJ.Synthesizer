using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_SquareWave : OperatorWrapperBase
    {
        public OperatorWrapper_SquareWave(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get { return GetInlet(OperatorConstants.SQUARE_WAVE_FREQUENCY_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SQUARE_WAVE_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return GetInlet(OperatorConstants.SQUARE_WAVE_PHASE_SHIFT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SQUARE_WAVE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SQUARE_WAVE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_SquareWave wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}