//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Shift_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
//    {
//        public Shift_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get => SignalInlet.InputOutlet;
//            set => SignalInlet.LinkTo(value);
//        }

//        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

//        public Outlet Difference
//        {
//            get => DifferenceInlet.InputOutlet;
//            set => DifferenceInlet.LinkTo(value);
//        }

//        public Inlet DifferenceInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Difference);
//    }
//}