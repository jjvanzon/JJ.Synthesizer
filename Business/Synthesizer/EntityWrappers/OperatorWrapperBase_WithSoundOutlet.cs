using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithSoundOutlet : OperatorWrapperBase_WithOneOutlet
    {
        private const int SOUND_OUTLET_INDEX = 0;

        public OperatorWrapperBase_WithSoundOutlet(Operator op)
            : base(op)
        { }

        public Outlet SoundOutlet => OperatorHelper.GetOutlet(WrappedOperator, SOUND_OUTLET_INDEX);

        public override string GetOutletDisplayName(Outlet outlet)
        {
            return ResourceFormatter.Sound;
        }
    }
}
