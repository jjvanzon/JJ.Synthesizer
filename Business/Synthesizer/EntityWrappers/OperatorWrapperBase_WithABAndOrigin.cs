using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithABAndOrigin : OperatorWrapperBase_WithAAndB
    {
        public OperatorWrapperBase_WithABAndOrigin(Operator op)
            : base(op)
        { }

        public Outlet Origin
        {
            get => OriginInlet.InputOutlet;
            set => OriginInlet.LinkTo(value);
        }

        public Inlet OriginInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Origin);
    }
}
