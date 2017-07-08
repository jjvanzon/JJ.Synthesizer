//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class DimensionToOutlets_OperatorWrapper : OperatorWrapperBase_VariableOutletCount
//    {
//        public DimensionToOutlets_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Input
//        {
//            get => Inlet.InputOutlet;
//            set => Inlet.LinkTo(value);
//        }

//        public Inlet Inlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);
//    }
//}