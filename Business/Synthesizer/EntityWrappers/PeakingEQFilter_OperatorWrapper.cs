using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PeakingEQFilter_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private const int SIGNAL_INDEX = 0;
        private const int CENTER_FREQUENCY_INDEX = 1;
        private const int BAND_WIDTH_INDEX = 2;
        private const int DB_GAIN_INDEX = 3;

        public PeakingEQFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX);

        public Outlet CenterFrequency
        {
            get { return CenterFrequencyInlet.InputOutlet; }
            set { CenterFrequencyInlet.LinkTo(value); }
        }

        public Inlet CenterFrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, CENTER_FREQUENCY_INDEX);

        public Outlet BandWidth
        {
            get { return BandWidthInlet.InputOutlet; }
            set { BandWidthInlet.LinkTo(value); }
        }

        public Inlet BandWidthInlet => OperatorHelper.GetInlet(WrappedOperator, BAND_WIDTH_INDEX);

        public Outlet DBGain
        {
            get { return DBGainInlet.InputOutlet; }
            set { DBGainInlet.LinkTo(value); }
        }

        public Inlet DBGainInlet => OperatorHelper.GetInlet(WrappedOperator, DB_GAIN_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => Signal);
                        return name;
                    }

                case CENTER_FREQUENCY_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => CenterFrequency);
                        return name;
                    }

                case BAND_WIDTH_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => BandWidth);
                        return name;
                    }

                case DB_GAIN_INDEX:
                    {
                        string name = ResourceFormatter.GetText(() => DBGain);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}
