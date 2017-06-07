using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_AggregateFollower : OperatorWrapperBase_WithSignalOutlet
    {
        public OperatorWrapperBase_AggregateFollower(Operator op)
            : base(op)
        { }

        public Outlet SignalInput
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

        public Outlet SliceLength
        {
            get => SliceLengthInlet.InputOutlet;
            set => SliceLengthInlet.LinkTo(value);
        }

        public Inlet SliceLengthInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.SliceLength);

        public Outlet SampleCount
        {
            get => SampleCountInlet.InputOutlet;
            set => SampleCountInlet.LinkTo(value);
        }

        public Inlet SampleCountInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.SampleCount);
    }
}
