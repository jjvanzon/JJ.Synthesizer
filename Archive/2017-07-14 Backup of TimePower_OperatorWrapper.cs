//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class TimePower_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
//    {
//        public TimePower_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get => SignalInlet.InputOutlet;
//            set => SignalInlet.LinkTo(value);
//        }

//        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

//        public Outlet Exponent
//        {
//            get => ExponentInlet.InputOutlet;
//            set => ExponentInlet.LinkTo(value);
//        }

//        public Inlet ExponentInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Exponent);

//        public Outlet Origin
//        {
//            get => OriginInlet.InputOutlet;
//            set => OriginInlet.LinkTo(value);
//        }

//        public Inlet OriginInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Origin);
//    }
//}