//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class If_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
//    {
//        public If_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Condition
//        {
//            get => ConditionInlet.InputOutlet;
//            set => ConditionInlet.LinkTo(value);
//        }

//        public Inlet ConditionInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Condition);

//        public Outlet Then
//        {
//            get => ThenInlet.InputOutlet;
//            set => ThenInlet.LinkTo(value);
//        }

//        public Inlet ThenInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Then);

//        public Outlet Else
//        {
//            get => ElseInlet.InputOutlet;
//            set => ElseInlet.LinkTo(value);
//        }

//        public Inlet ElseInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Else);
//    }
//}