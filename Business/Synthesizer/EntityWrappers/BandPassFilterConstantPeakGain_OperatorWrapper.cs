﻿using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class BandPassFilterConstantPeakGain_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        public BandPassFilterConstantPeakGain_OperatorWrapper(Operator op)
            : base(op)
        { }

        private const int SIGNAL_INDEX = 0;
        private const int CENTER_FREQUENCY_INDEX = 1;
        private const int BAND_WIDTH_INDEX = 2;

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

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case CENTER_FREQUENCY_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => CenterFrequency);
                        return name;
                    }

                case BAND_WIDTH_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => BandWidth);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}