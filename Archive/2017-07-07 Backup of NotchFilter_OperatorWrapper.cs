﻿//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class NotchFilter_OperatorWrapper : OperatorWrapperBase_WithSoundOutlet
//    {
//        public NotchFilter_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet SoundInput
//        {
//            get => SoundInlet.InputOutlet;
//            set => SoundInlet.LinkTo(value);
//        }

//        public Inlet SoundInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Sound);

//        public Outlet CenterFrequency
//        {
//            get => CenterFrequencyInlet.InputOutlet;
//            set => CenterFrequencyInlet.LinkTo(value);
//        }

//        public Inlet CenterFrequencyInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Frequency);

//        public Outlet Width
//        {
//            get => WidthInlet.InputOutlet;
//            set => WidthInlet.LinkTo(value);
//        }

//        public Inlet WidthInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Width);

//        public override string GetInletDisplayName(Inlet inlet)
//        {
//            if (inlet == null) throw new NullException(() => inlet);

//            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
//            switch (dimensionEnum)
//            {
//                case DimensionEnum.Frequency:
//                    return ResourceFormatter.CenterFrequency;
//            }

//            return base.GetInletDisplayName(inlet);
//        }
//   }
//}