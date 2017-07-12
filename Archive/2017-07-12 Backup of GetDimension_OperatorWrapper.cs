//using JJ.Business.Synthesizer.Helpers;
//using System;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class GetDimension_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
//    {
//        public GetDimension_OperatorWrapper(Operator op)
//            : base(op)
//        { }

//        public Outlet Number => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Number);

//        public override string GetInletDisplayName(Inlet inlet)
//        {
//            throw new Exception("This operator does not have Inlets, so you cannot get an InletDisplayName.");
//        }
//    }
//}