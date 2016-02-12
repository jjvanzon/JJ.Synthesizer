using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Triangle_OperatorWrapper : OperatorWrapperBase
    {
        public Triangle_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Frequency
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.TRIANGLE_FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.TRIANGLE_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet PhaseShift
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.TRIANGLE_PHASE_SHIFT_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.TRIANGLE_PHASE_SHIFT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.TRIANGLE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Triangle_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}