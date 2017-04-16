using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_AggregateFollower : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int SLICE_LENGTH_INDEX = 1;
        private const int SAMPLE_COUNT_INDEX = 2;

        public OperatorWrapperBase_AggregateFollower(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet SliceLength
        {
            get { return SliceLengthInlet.InputOutlet; }
            set { SliceLengthInlet.LinkTo(value); }
        }

        public Inlet SliceLengthInlet => OperatorHelper.GetInlet(WrappedOperator, SLICE_LENGTH_INDEX);

        public Outlet SampleCount
        {
            get { return SampleCountInlet.InputOutlet; }
            set { SampleCountInlet.LinkTo(value); }
        }

        public Inlet SampleCountInlet => OperatorHelper.GetInlet(WrappedOperator, SAMPLE_COUNT_INDEX); 

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case SLICE_LENGTH_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => SliceLength);
                        return name;
                    }

                case SAMPLE_COUNT_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => SampleCount);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
