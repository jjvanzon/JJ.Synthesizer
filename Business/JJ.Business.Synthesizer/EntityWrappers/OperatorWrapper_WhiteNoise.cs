using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_WhiteNoise : OperatorWrapperBase
    {
        public OperatorWrapper_WhiteNoise(Operator op)
            : base(op)
        { }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.WHITE_NOISE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_WhiteNoise wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
