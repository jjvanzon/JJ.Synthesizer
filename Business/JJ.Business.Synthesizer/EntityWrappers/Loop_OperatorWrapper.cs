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
            get { return GetInlet(OperatorConstants.LOOP_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Attack
        {
            get { return GetInlet(OperatorConstants.LOOP_ATTACK_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_ATTACK_INDEX).LinkTo(value); }
        }

        public Outlet Start
        {
            get { return GetInlet(OperatorConstants.LOOP_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_START_INDEX).LinkTo(value); }
        }

        public Outlet Sustain
        {
            get { return GetInlet(OperatorConstants.LOOP_SUSTAIN_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_SUSTAIN_INDEX).LinkTo(value); }
        }

        public Outlet End
        {
            get { return GetInlet(OperatorConstants.LOOP_END_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_END_INDEX).LinkTo(value); }
        }

        public Outlet Release
        {
            get { return GetInlet(OperatorConstants.LOOP_RELEASE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_RELEASE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.LOOP_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Loop_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}