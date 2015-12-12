using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SquareWave_OperatorWrapper : OperatorWrapperBase
    {
        public SquareWave_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.SQUARE_WAVE_FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.SQUARE_WAVE_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return OperatorHelper.GetInputOutlet(_operator, OperatorConstants.SQUARE_WAVE_PHASE_SHIFT_INDEX); }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.SQUARE_WAVE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.SQUARE_WAVE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(SquareWave_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}