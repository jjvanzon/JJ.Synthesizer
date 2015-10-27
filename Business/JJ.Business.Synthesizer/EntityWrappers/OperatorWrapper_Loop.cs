using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Loop : OperatorWrapperBase
    {
        public OperatorWrapper_Loop(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.LOOP_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet AttackStart
        {
            get { return GetInlet(OperatorConstants.LOOP_ATTACK_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_ATTACK_START_INDEX).LinkTo(value); }
        }

        public Outlet LoopStart
        {
            get { return GetInlet(OperatorConstants.LOOP_LOOP_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_LOOP_START_INDEX).LinkTo(value); }
        }

        public Outlet LoopDuration
        {
            get { return GetInlet(OperatorConstants.LOOP_LOOP_DURATION).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_LOOP_DURATION).LinkTo(value); }
        }

        public Outlet LoopEnd
        {
            get { return GetInlet(OperatorConstants.LOOP_LOOP_END_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_LOOP_END_INDEX).LinkTo(value); }
        }

        public Outlet ReleaseEnd
        {
            get { return GetInlet(OperatorConstants.LOOP_RELEASE_END).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_RELEASE_END).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.LOOP_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Loop wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}