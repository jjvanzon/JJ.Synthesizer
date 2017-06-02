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
    public class HighPassFilter_OperatorWrapper : OperatorWrapperBase_WithSoundOutlet
    {
        public HighPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet SoundInput
        {
            get => SoundInlet.InputOutlet;
            set => SoundInlet.LinkTo(value);
        }

        public Inlet SoundInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Sound);

        public Outlet MinFrequency
        {
            get => MinFrequencyInlet.InputOutlet;
            set => MinFrequencyInlet.LinkTo(value);
        }

        public Inlet MinFrequencyInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Frequency);

        public Outlet BlobVolume
        {
            get => BlobVolumeInlet.InputOutlet;
            set => BlobVolumeInlet.LinkTo(value);
        }

        public Inlet BlobVolumeInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.BlobVolume);

        public override string GetInletDisplayName([NotNull] Inlet inlet)
        {
            if (inlet == null) throw new NullException(() => inlet);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (inlet.GetDimensionEnum() == DimensionEnum.Frequency)
            {
                return ResourceFormatter.MinFrequency;
            }

            return base.GetInletDisplayName(inlet);
        }

    }
}