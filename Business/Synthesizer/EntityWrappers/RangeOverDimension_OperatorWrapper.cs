using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class RangeOverDimension_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
    {
        public RangeOverDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet From
        {
            get => FromInlet.InputOutlet;
            set => FromInlet.LinkTo(value);
        }

        public Inlet FromInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.From);

        public Outlet Till
        {
            get => TillInlet.InputOutlet;
            set => TillInlet.LinkTo(value);
        }

        public Inlet TillInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Till);

        public Outlet Step
        {
            get => StepInlet.InputOutlet;
            set => StepInlet.LinkTo(value);
        }

        public Inlet StepInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Step);
    }
}