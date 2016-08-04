using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_ShelfFilter : OperatorWrapperBase
    {
        private const int SIGNAL_INDEX = 0;
        private const int TRANSITION_FREQUENCY_INDEX = 1;
        private const int TRANSITION_SLOPE_INDEX = 2;
        private const int DB_GAIN_INDEX = 3;
        private const int RESULT_INDEX = 0;

        public OperatorWrapperBase_ShelfFilter(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX); }
        }

        public Outlet TransitionFrequency
        {
            get { return TransitionFrequencyInlet.InputOutlet; }
            set { TransitionFrequencyInlet.LinkTo(value); }
        }

        public Inlet TransitionFrequencyInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, TRANSITION_FREQUENCY_INDEX); }
        }

        public Outlet TransitionSlope
        {
            get { return TransitionSlopeInlet.InputOutlet; }
            set { TransitionSlopeInlet.LinkTo(value); }
        }

        public Inlet TransitionSlopeInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, TRANSITION_SLOPE_INDEX); }
        }

        public Outlet DBGain
        {
            get { return DBGainInlet.InputOutlet; }
            set { DBGainInlet.LinkTo(value); }
        }

        public Inlet DBGainInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, DB_GAIN_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case TRANSITION_FREQUENCY_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => TransitionFrequency);
                        return name;
                    }

                case TRANSITION_SLOPE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => TransitionSlope);
                        return name;
                    }

                case DB_GAIN_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => DBGain);
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

        public static implicit operator Outlet(OperatorWrapperBase_ShelfFilter wrapper) => wrapper?.Result;
    }
}
