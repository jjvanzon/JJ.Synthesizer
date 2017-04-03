using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Spectrum_OperatorWrapper : OperatorWrapperBase_WithResult
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

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.SPECTRUM_SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_START_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Start);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_END_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => End);
                        return name;
                    }

                case OperatorConstants.SPECTRUM_FREQUENCY_COUNT_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => FrequencyCount);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
