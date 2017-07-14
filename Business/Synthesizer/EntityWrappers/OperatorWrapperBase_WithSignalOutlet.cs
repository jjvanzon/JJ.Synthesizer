using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithSignalOutlet : OperatorWrapperBase_WithOneOutlet
    {
        public OperatorWrapperBase_WithSignalOutlet(Operator op)
            : base(op)
        { }

        public Outlet SignalOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Signal);

        public override string GetOutletDisplayName(Outlet outlet)
        {
            return ResourceFormatter.Signal;
        }
    }
}
