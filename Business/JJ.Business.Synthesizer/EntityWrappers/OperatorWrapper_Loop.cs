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

        public Outlet Start
        {
            get { return GetInlet(OperatorConstants.LOOP_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_START_INDEX).LinkTo(value); }
        }

        public Outlet LoopStart
        {
            get { return GetInlet(OperatorConstants.LOOP_LOOP_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_LOOP_START_INDEX).LinkTo(value); }
        }

        public Outlet LoopEnd
        {
            get { return GetInlet(OperatorConstants.LOOP_LOOP_END_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_LOOP_END_INDEX).LinkTo(value); }
        }

        public Outlet End
        {
            get { return GetInlet(OperatorConstants.LOOP_END_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.LOOP_END_INDEX).LinkTo(value); }
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