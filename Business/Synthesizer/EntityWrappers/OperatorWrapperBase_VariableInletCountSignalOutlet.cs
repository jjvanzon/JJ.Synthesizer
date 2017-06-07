using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableInletCountSignalOutlet : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public OperatorWrapperBase_VariableInletCountSignalOutlet(Operator op)
            : base(op)
        { }

        public Outlet SignalOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Signal);
    }
}