//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public abstract class StretchOrSquash_OperatorWrapperBase : OperatorWrapperBase_WithSignalOutlet
//    {
//        public StretchOrSquash_OperatorWrapperBase(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get => SignalInlet.InputOutlet;
//            set => SignalInlet.LinkTo(value);
//        }

//        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

//        public Outlet Factor
//        {
//            get => FactorInlet.InputOutlet;
//            set => FactorInlet.LinkTo(value);
//        }

//        public Inlet FactorInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Factor);

//        public Outlet Origin
//        {
//            get => OriginInlet.InputOutlet;
//            set => OriginInlet.LinkTo(value);
//        }

//        public Inlet OriginInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Origin);
//    }
//}
