using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PeakingEQFilter_OperatorWrapper : OperatorWrapperBase_WithSoundOutlet
    {
        public PeakingEQFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Sound
        {
            get => SoundInlet.InputOutlet;
            set => SoundInlet.LinkTo(value);
        }

        public Inlet SoundInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Sound);

        public Outlet CenterFrequency
        {
            get => CenterFrequencyInlet.InputOutlet;
            set => CenterFrequencyInlet.LinkTo(value);
        }

        public Inlet CenterFrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Frequency);

        public Outlet Width
        {
            get => WidthInlet.InputOutlet;
            set => WidthInlet.LinkTo(value);
        }

        public Inlet WidthInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Width);

        public Outlet DBGain
        {
            get => DBGainInlet.InputOutlet;
            set => DBGainInlet.LinkTo(value);
        }

        public Inlet DBGainInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Decibel);

        public override string GetInletDisplayName([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
            switch (dimensionEnum)
            {
                case DimensionEnum.Frequency:
                    return ResourceFormatter.CenterFrequency;

                case DimensionEnum.Decibel:
                    return ResourceFormatter.DBGain;
            }

            return base.GetInletDisplayName(inlet);
        }
    }
}
