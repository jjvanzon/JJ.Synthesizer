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

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_SIGNAL_INDEX);

        public Outlet Start
        {
            get { return StartInlet.InputOutlet; }
            set { StartInlet.LinkTo(value); }
        }

        public Inlet StartInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_START_INDEX);

        public Outlet End
        {
            get { return EndInlet.InputOutlet; }
            set { EndInlet.LinkTo(value); }
        }

        public Inlet EndInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_END_INDEX);

        public Outlet FrequencyCount
        {
            get { return FrequencyCountInlet.InputOutlet; }
            set { FrequencyCountInlet.LinkTo(value); }
        }

        public Inlet FrequencyCountInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.SPECTRUM_RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_START_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Start);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_END_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => End);
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

        public static implicit operator Outlet(Spectrum_OperatorWrapper wrapper) => wrapper?.Result;
    }
}
