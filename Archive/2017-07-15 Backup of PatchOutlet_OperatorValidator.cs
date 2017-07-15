//using JJ.Business.Synthesizer.Enums;
//using System.Linq;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Presentation.Resources;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal class PatchOutlet_OperatorValidator : OperatorValidator_Base_WithoutData
//    {
//        public PatchOutlet_OperatorValidator(Operator op)
//            : base(
//                op,
//                OperatorTypeEnum.PatchOutlet,
//                new[] { DimensionEnum.Undefined },
//                new[] { GetOutletDimensionEnum(op) })
//        {
//            For(op.Name, CommonResourceFormatter.Name).IsNullOrEmpty();
//        }

//        private static DimensionEnum GetOutletDimensionEnum(Operator op)
//        {
//            return op?.Outlets.FirstOrDefault()?.GetDimensionEnum() ?? DimensionEnum.Undefined;
//        }
//    }
//}
