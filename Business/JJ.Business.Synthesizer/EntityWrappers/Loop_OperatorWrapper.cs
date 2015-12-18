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

        public Outlet Attack
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_ATTACK_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_ATTACK_INDEX).LinkTo(value); }
        }

        public Outlet Start
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_START_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_START_INDEX).LinkTo(value); }
        }

        public Outlet Sustain
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_SUSTAIN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_SUSTAIN_INDEX).LinkTo(value); }
        }

        public Outlet End
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_END_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_END_INDEX).LinkTo(value); }
        }

        public Outlet Release
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOOP_RELEASE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOOP_RELEASE_INDEX).LinkTo(value); }
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