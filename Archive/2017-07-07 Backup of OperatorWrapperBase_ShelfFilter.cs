//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public abstract class OperatorWrapperBase_ShelfFilter : OperatorWrapperBase_WithSoundOutlet
//    {
//        public OperatorWrapperBase_ShelfFilter(Operator op)
//            : base(op)
//        { }

//        public Outlet SoundInput
//        {
//            get => SoundInlet.InputOutlet;
//            set => SoundInlet.LinkTo(value);
//        }

//        public Inlet SoundInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Sound);

//        public Outlet TransitionFrequency
//        {
//            get => TransitionFrequencyInlet.InputOutlet;
//            set => TransitionFrequencyInlet.LinkTo(value);
//        }

//        public Inlet TransitionFrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);

//        public Outlet TransitionSlope
//        {
//            get => TransitionSlopeInlet.InputOutlet;
//            set => TransitionSlopeInlet.LinkTo(value);
//        }

//        public Inlet TransitionSlopeInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Slope);

//        public Outlet DBGain
//        {
//            get => DBGainInlet.InputOutlet;
//            set => DBGainInlet.LinkTo(value);
//        }

//        public Inlet DBGainInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Decibel);

//        public override string GetInletDisplayName([NotNull] Inlet inlet)
//        {
//            if (inlet == null) throw new NullException(() => inlet);

//            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
//            switch (dimensionEnum)
//            {
//                case DimensionEnum.Frequency:
//                    return ResourceFormatter.TransitionFrequency;

//                case DimensionEnum.Slope:
//                    return ResourceFormatter.TransitionSlope;

//                case DimensionEnum.Decibel:
//                    return ResourceFormatter.DBGain;
//            }

//            return base.GetInletDisplayName(inlet);
//        }
//    }
//}
