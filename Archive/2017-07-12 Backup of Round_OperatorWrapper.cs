//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class Round_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
//    {
//        public Round_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Signal
//        {
//            get => SignalInlet.InputOutlet;
//            set => SignalInlet.LinkTo(value);
//        }

//        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

//        public Outlet Step
//        {
//            get => StepInlet.InputOutlet;
//            set => StepInlet.LinkTo(value);
//        }

//        public Inlet StepInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Step);

//        public Outlet Offset
//        {
//            get => OffsetInlet.InputOutlet;
//            set => OffsetInlet.LinkTo(value);
//        }

//        public Inlet OffsetInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Offset);
//   }
//}