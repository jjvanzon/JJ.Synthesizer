//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class ClosestOverDimension_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
//    {
//        public ClosestOverDimension_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Input
//        {
//            get => InputInlet.InputOutlet;
//            set => InputInlet.LinkTo(value);
//        }

//        public Inlet InputInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Input);

//        public Outlet Collection
//        {
//            get => CollectionInlet.InputOutlet;
//            set => CollectionInlet.LinkTo(value);
//        }

//        public Inlet CollectionInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Collection);

//        public Outlet From
//        {
//            get => FromInlet.InputOutlet;
//            set => FromInlet.LinkTo(value);
//        }

//        public Inlet FromInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.From);

//        public Outlet Till
//        {
//            get => TillInlet.InputOutlet;
//            set => TillInlet.LinkTo(value);
//        }

//        public Inlet TillInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Till);

//        public Outlet Step
//        {
//            get => StepInlet.InputOutlet;
//            set => StepInlet.LinkTo(value);
//        }

//        public Inlet StepInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Step);

//        public CollectionRecalculationEnum CollectionRecalculation
//        {
//            get => DataPropertyParser.GetEnum<CollectionRecalculationEnum>(WrappedOperator, nameof(CollectionRecalculation));
//            set => DataPropertyParser.SetValue(WrappedOperator, nameof(CollectionRecalculation), value);
//        }
//    }
//}