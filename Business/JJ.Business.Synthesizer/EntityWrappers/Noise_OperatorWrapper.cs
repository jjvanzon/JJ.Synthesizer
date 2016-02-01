using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Noise_OperatorWrapper : OperatorWrapperBase
    {
        public Noise_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.WHITE_NOISE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Noise_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
