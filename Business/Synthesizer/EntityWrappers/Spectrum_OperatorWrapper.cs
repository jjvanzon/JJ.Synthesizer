using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Spectrum_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
    {
        public Spectrum_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Sound
        {
            get => SoundInlet.InputOutlet;
            set => SoundInlet.LinkTo(value);
        }

        public Inlet SoundInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Sound);

        public Outlet Start
        {
            get => StartInlet.InputOutlet;
            set => StartInlet.LinkTo(value);
        }

        public Inlet StartInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Start);

        public Outlet End
        {
            get => EndInlet.InputOutlet;
            set => EndInlet.LinkTo(value);
        }

        public Inlet EndInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.End);

        public Outlet FrequencyCount
        {
            get => FrequencyCountInlet.InputOutlet;
            set => FrequencyCountInlet.LinkTo(value);
        }

        public Inlet FrequencyCountInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.FrequencyCount);

        public Outlet VolumeOutlet => OperatorHelper.GetOutlet(WrappedOperator, DimensionEnum.Volume);
    }
}
