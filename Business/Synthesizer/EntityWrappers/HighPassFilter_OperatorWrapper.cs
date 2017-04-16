using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class HighPassFilter_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int MIN_FREQUENCY_INDEX = 1;
        private const int BAND_WIDTH_INDEX = 2;

        public HighPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet MinFrequency
        {
            get { return MinFrequencyInlet.InputOutlet; }
            set { MinFrequencyInlet.LinkTo(value); }
        }

        public Inlet MinFrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, MIN_FREQUENCY_INDEX);

        public Outlet BandWidth
        {
            get { return BandWidthInlet.InputOutlet; }
            set { BandWidthInlet.LinkTo(value); }
        }

        public Inlet BandWidthInlet => OperatorHelper.GetInlet(WrappedOperator, BAND_WIDTH_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case MIN_FREQUENCY_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => MinFrequency);
                        return name;
                    }

                case BAND_WIDTH_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => BandWidth);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}