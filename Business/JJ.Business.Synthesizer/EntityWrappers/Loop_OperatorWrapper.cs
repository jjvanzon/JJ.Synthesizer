using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Loop_OperatorWrapper : OperatorWrapperBase
    {
        private const int LOOP_SIGNAL_INDEX = 0;
        private const int LOOP_ATTACK_DURATION_INDEX = 1;
        private const int LOOP_LOOP_START_MARKER_INDEX = 2;
        private const int LOOP_SUSTAIN_DURATION_INDEX = 3;
        private const int LOOP_END_MARKER_INDEX = 4;
        private const int LOOP_RELEASE_END_MARKER_INDEX = 5;
        private const int LOOP_RESULT_INDEX = 0;

        public Loop_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet AttackDuration
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_ATTACK_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_ATTACK_DURATION_INDEX).LinkTo(value); }
        }

        public Outlet LoopStartMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_LOOP_START_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_LOOP_START_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet SustainDuration
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_SUSTAIN_DURATION_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_SUSTAIN_DURATION_INDEX).LinkTo(value); }
        }

        public Outlet LoopEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet ReleaseEndMarker
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, LOOP_RELEASE_END_MARKER_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, LOOP_RELEASE_END_MARKER_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, LOOP_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Loop_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}