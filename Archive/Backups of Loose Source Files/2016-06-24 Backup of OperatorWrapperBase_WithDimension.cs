//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Enums;
//using System;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    [Obsolete("Copy-paste dimension property to classes that now inherit from it.", true)]
//    public class OperatorWrapperBase_WithDimension : OperatorWrapperBase
//    {
//        public OperatorWrapperBase_WithDimension(Operator op)
//            : base(op)
//        { }

//        public DimensionEnum Dimension
//        {
//            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
//            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
//        }
//    }
//}