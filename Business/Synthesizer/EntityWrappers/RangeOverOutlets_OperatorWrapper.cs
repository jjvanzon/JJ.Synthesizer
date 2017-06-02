using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class RangeOverOutlets_OperatorWrapper : OperatorWrapperBase_VariableOutletCount
    {
        public RangeOverOutlets_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get => FromInlet.InputOutlet;
            set => FromInlet.LinkTo(value);
        }

        public Inlet FromInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.From);

        public Outlet Step
        {
            get => StepInlet.InputOutlet;
            set => StepInlet.LinkTo(value);
        }

        public Inlet StepInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Step);
    }
}
