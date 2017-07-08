//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;
//using JJ.Business.Synthesizer.Enums;
//using System.Linq;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal class DimensionToOutlets_OperatorValidator : OperatorValidator_Base_WithoutData
//    {
//        public DimensionToOutlets_OperatorValidator(Operator obj)
//            : base(
//                obj,
//                OperatorTypeEnum.DimensionToOutlets,
//                new[] { DimensionEnum.Signal },
//                Enumerable.Repeat(DimensionEnum.Item, obj?.Outlets.Count ?? 0).ToArray())
//        { 
//            For(() => obj.Outlets.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets)).GreaterThan(0);
//        }
//    }
//}