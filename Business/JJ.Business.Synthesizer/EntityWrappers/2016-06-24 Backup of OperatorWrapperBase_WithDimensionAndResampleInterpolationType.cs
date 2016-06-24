//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Enums;
//using System;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class OperatorWrapperBase_WithDimensionAndResampleInterpolationType : OperatorWrapperBase
//    {
//        [Obsolete("", true)]
//        public OperatorWrapperBase_WithDimensionAndResampleInterpolationType(Operator op)
//            : base(op)
//        { }

//        public DimensionEnum Dimension
//        {
//            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
//            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
//        }

//        public ResampleInterpolationTypeEnum InterpolationType
//        {
//            get { return DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
//            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
//        }
//    }
//}