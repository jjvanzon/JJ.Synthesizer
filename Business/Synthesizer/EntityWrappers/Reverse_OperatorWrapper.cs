using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Reverse_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
    {
        public Reverse_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Signal);

        public Outlet Factor
        {
            get => FactorInlet.InputOutlet;
            set => FactorInlet.LinkTo(value);
        }

        public Inlet FactorInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Factor);
    }
}
