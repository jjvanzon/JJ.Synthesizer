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
    public abstract class OperatorWrapperBase_BandPassFilter : OperatorWrapperBase_WithSoundOutlet
    {
        public OperatorWrapperBase_BandPassFilter(Operator op)
            : base(op)
        { }

        public Outlet SoundInput
        {
            get => SoundInlet.InputOutlet;
            set => SoundInlet.LinkTo(value);
        }

        public Inlet SoundInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Sound);

        public Outlet CenterFrequency
        {
            get => CenterFrequencyInlet.InputOutlet;
            set => CenterFrequencyInlet.LinkTo(value);
        }

        public Inlet CenterFrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);

        public Outlet Width
        {
            get => WidthInlet.InputOutlet;
            set => WidthInlet.LinkTo(value);
        }

        public Inlet WidthInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Width);

        public override string GetInletDisplayName([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (inlet.GetDimensionEnum() == DimensionEnum.Frequency)
            {
                return ResourceFormatter.CenterFrequency;
            }

            return base.GetInletDisplayName(inlet);
        }
    }
}