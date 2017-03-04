using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Loop_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        public Loop_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX);

        public Outlet Skip
        {
            get { return SkipInlet.InputOutlet; }
            set { SkipInlet.LinkTo(value); }
        }

        public Inlet SkipInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SKIP_INDEX);

        public Outlet LoopStartMarker
        {
            get { return LoopStartMarkerInlet.InputOutlet; }
            set { LoopStartMarkerInlet.LinkTo(value); }
        }

        public Inlet LoopStartMarkerInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX);

        public Outlet LoopEndMarker
        {
            get { return LoopEndMarkerInlet.InputOutlet; }
            set { LoopEndMarkerInlet.LinkTo(value); }
        }

        public Inlet LoopEndMarkerInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX);

        public Outlet ReleaseEndMarker
        {
            get { return ReleaseEndMarkerInlet.InputOutlet; }
            set { ReleaseEndMarkerInlet.LinkTo(value); }
        }

        public Inlet ReleaseEndMarkerInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX);

        public Outlet NoteDuration
        {
            get { return NoteDurationInlet.InputOutlet; }
            set { NoteDurationInlet.LinkTo(value); }
        }

        public Inlet NoteDurationInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_NOTE_DURATION_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.LOOP_SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => Signal);
                        return name;
                    }

                case OperatorConstants.LOOP_SKIP_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => Skip);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_START_MARKER_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => LoopStartMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_END_MARKER_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => LoopEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => ReleaseEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_NOTE_DURATION_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => NoteDuration);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}