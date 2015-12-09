using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class WhiteNoise_OperatorWrapper : OperatorWrapperBase
    {
        public WhiteNoise_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.WHITE_NOISE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(WhiteNoise_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
