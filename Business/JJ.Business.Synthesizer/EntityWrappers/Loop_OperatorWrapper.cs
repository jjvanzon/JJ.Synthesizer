using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Loop_OperatorWrapper : OperatorWrapperBase
    {
        public Loop_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX); }
        }

        public Outlet Skip
        {
            get { return SkipInlet.InputOutlet; }
            set { SkipInlet.LinkTo(value); }
        }

        public Inlet SkipInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_SKIP_INDEX); }
        }

        public Outlet LoopStartMarker
        {
            get { return LoopStartMarkerInlet.InputOutlet; }
            set { LoopStartMarkerInlet.LinkTo(value); }
        }

        public Inlet LoopStartMarkerInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX); }
        }

        public Outlet LoopEndMarker
        {
            get { return LoopEndMarkerInlet.InputOutlet; }
            set { LoopEndMarkerInlet.LinkTo(value); }
        }

        public Inlet LoopEndMarkerInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX); }
        }

        public Outlet ReleaseEndMarker
        {
            get { return ReleaseEndMarkerInlet.InputOutlet; }
            set { ReleaseEndMarkerInlet.LinkTo(value); }
        }

        public Inlet ReleaseEndMarkerInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX); }
        }

        public Outlet NoteDuration
        {
            get { return NoteDurationInlet.InputOutlet; }
            set { NoteDurationInlet.LinkTo(value); }
        }

        public Inlet NoteDurationInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.LOOP_NOTE_DURATION_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.LOOP_RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.LOOP_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.LOOP_SKIP_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Skip);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_START_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => LoopStartMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_LOOP_END_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => LoopEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => ReleaseEndMarker);
                        return name;
                    }

                case OperatorConstants.LOOP_NOTE_DURATION_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => NoteDuration);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Loop_OperatorWrapper wrapper) => wrapper?.Result;
    }
}