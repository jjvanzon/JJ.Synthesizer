using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Loop_OperatorWrapper : OperatorWrapperBase
    {
        public Loop_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Skip
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_SKIP_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_SKIP_INDEX).LinkTo(value); }
        }

        public Outlet LoopStartMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_LOOP_START_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet SustainDuration
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_SUSTAIN_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_SUSTAIN_DURATION_INDEX).LinkTo(value); }
        }

        public Outlet LoopEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_LOOP_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet ReleaseEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_RELEASE_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.LOOP_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Loop_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}