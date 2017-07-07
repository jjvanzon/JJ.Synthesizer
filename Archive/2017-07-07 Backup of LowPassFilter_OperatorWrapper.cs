//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class LowPassFilter_OperatorWrapper : OperatorWrapperBase_WithSoundOutlet
//    {
//        public LowPassFilter_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet SoundInput
//        {
//            get => SoundInlet.InputOutlet;
//            set => SoundInlet.LinkTo(value);
//        }

//        public Inlet SoundInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Sound);

//        public Outlet MaxFrequency
//        {
//            get => MaxFrequencyInlet.InputOutlet;
//            set => MaxFrequencyInlet.LinkTo(value);
//        }

//        public Inlet MaxFrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);

//        public Outlet BlobVolume
//        {
//            get => BlobVolumeInlet.InputOutlet;
//            set => BlobVolumeInlet.LinkTo(value);
//        }

//        public Inlet BlobVolumeInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.BlobVolume);

//        public override string GetInletDisplayName(Inlet inlet)
//        {
//            if (inlet == null) throw new NullException(() => inlet);

//            switch (inlet.GetDimensionEnum())
//            {
//                case DimensionEnum.Frequency:
//                    return ResourceFormatter.MaxFrequency;
//            }

//            return base.GetInletDisplayName(inlet);
//        }
//    }
//}
