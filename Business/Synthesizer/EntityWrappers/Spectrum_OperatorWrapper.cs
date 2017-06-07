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

        public Inlet SoundInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Sound);

        public Outlet Start
        {
            get => StartInlet.InputOutlet;
            set => StartInlet.LinkTo(value);
        }

        public Inlet StartInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Start);

        public Outlet End
        {
            get => EndInlet.InputOutlet;
            set => EndInlet.LinkTo(value);
        }

        public Inlet EndInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.End);

        public Outlet FrequencyCount
        {
            get => FrequencyCountInlet.InputOutlet;
            set => FrequencyCountInlet.LinkTo(value);
        }

        public Inlet FrequencyCountInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.FrequencyCount);

        public Outlet VolumeOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Volume);
    }
}
