using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class TriangleWave_OperatorWrapper : OperatorWrapperBase
    {
        public TriangleWave_OperatorWrapper(Operator op)
            : base(op)
        { }

        // Original:
        public Outlet Frequency
        {
            get { return GetInlet(OperatorConstants.TRIANGLE_WAVE_FREQUENCY_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TRIANGLE_WAVE_FREQUENCY_INDEX).LinkTo(value); }
        }

        // Tryout 1:
        //public Outlet Frequency
        //{
        //    get { return FrequencyInlet.InputOutlet; }
        //    set { FrequencyInlet.LinkTo(value); }
        //}

        //public Inlet FrequencyInlet
        //{
        //    get { return GetInlet(OperatorConstants.TRIANGLE_WAVE_FREQUENCY_INDEX); }
        //}

        // Tryout 2:
        //public Outlet Frequency
        //{
        //    get { return GetInputOutletOrDefault(OperatorConstants.TRIANGLE_WAVE_FREQUENCY_INDEX); }
        //    set { GetInlet(OperatorConstants.TRIANGLE_WAVE_FREQUENCY_INDEX).LinkTo(value); }
        //}

        public Outlet PhaseShift
        {
            get { return GetInlet(OperatorConstants.TRIANGLE_WAVE_PHASE_SHIFT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.TRIANGLE_WAVE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.TRIANGLE_WAVE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(TriangleWave_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}