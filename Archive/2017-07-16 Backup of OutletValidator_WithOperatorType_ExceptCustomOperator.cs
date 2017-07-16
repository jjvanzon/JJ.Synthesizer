//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class OutletValidator_WithOperatorType_ExceptCustomOperator : VersatileValidator
//    {
//        public OutletValidator_WithOperatorType_ExceptCustomOperator(Outlet obj, DimensionEnum expectedDimensionEnum)
//        {
//            if (obj == null) throw new NullException(() => obj);

//            For(obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(expectedDimensionEnum);
//        }
//    }
//}