//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class InletValidator_WithOperatorType_ExceptCustomOperator : VersatileValidator
//    {
//        public InletValidator_WithOperatorType_ExceptCustomOperator(Inlet inlet, DimensionEnum expectedDimensionEnum)
//        {
//            if (inlet == null) throw new NullException(() => inlet);

//            For(inlet.GetDimensionEnum(), ResourceFormatter.Dimension).Is(expectedDimensionEnum);
//        }
//    }
//}