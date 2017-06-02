using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    /// <summary> Not abstract, so we can create the same wrapper for several types of Operator. </summary>
    public class OperatorWrapperBase_AggregateOverDimension : OperatorWrapperBase_WithSignalOutlet
    {
        public OperatorWrapperBase_AggregateOverDimension(Operator op)
            : base(op)
        { }

        public Outlet SignalInput
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Signal);

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

        public CollectionRecalculationEnum CollectionRecalculation
        {
            get => DataPropertyParser.GetEnum<CollectionRecalculationEnum>(WrappedOperator, nameof(CollectionRecalculation));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(CollectionRecalculation), value);
        }
    }
}