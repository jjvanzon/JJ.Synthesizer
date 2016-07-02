using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Spectrum_OperatorWrapper : OperatorWrapperBase
    {
        public Spectrum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX); }
        }

        public Outlet StartTime
        {
            get { return StartTimeInlet.InputOutlet; }
            set { StartTimeInlet.LinkTo(value); }
        }

        public Inlet StartTimeInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_START_TIME_INDEX); }
        }

        public Outlet EndTime
        {
            get { return EndTimeInlet.InputOutlet; }
            set { EndTimeInlet.LinkTo(value); }
        }

        public Inlet EndTimeInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_END_TIME_INDEX); }
        }

        public Outlet FrequencyCount
        {
            get { return FrequencyCountInlet.InputOutlet; }
            set { FrequencyCountInlet.LinkTo(value); }
        }

        public Inlet FrequencyCountInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.SPECTRUM_RESULT_INDEX); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_START_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => StartTime);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_END_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => EndTime);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => FrequencyCount);
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

        public static implicit operator Outlet(Spectrum_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
