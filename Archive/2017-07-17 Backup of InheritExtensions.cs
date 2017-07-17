//using System;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.Extensions
//{
//    internal static class InheritExtensions
//    {
//        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
//        public static DimensionEnum GetStandardDimensionEnumWithFallback(this Operator op)
//        {
//            if (op == null) throw new ArgumentNullException(nameof(op));

//            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

//            bool isPatchInletOrPatchOutlet = operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
//                                             operatorTypeEnum == OperatorTypeEnum.PatchOutlet;
//            if (isPatchInletOrPatchOutlet)
//            {
//                return GetStandardDimensionEnumWithFallback_ForPatchInletAndPatchOutlet(op);
//            }
//            else
//            {
//                return GetStandardDimensionEnumWithFallback_ForOtherOperators(op);
//            }
//        }

//        private static DimensionEnum GetStandardDimensionEnumWithFallback_ForPatchInletAndPatchOutlet(Operator op)
//        {
//            DimensionEnum dimensionEnum = op.GetStandardDimensionEnum();

//            if (dimensionEnum == DimensionEnum.Inherit)
//            {
//                return op.Patch.GetStandardDimensionEnum();
//            }

//            return dimensionEnum;
//        }

//        private static DimensionEnum GetStandardDimensionEnumWithFallback_ForOtherOperators(Operator op)
//        {
//            DimensionEnum dimensionEnum = op.GetStandardDimensionEnum();

//            if (dimensionEnum == DimensionEnum.Inherit)
//            {
//                return op.Patch.GetStandardDimensionEnum();
//            }

//            return dimensionEnum;
//        }

//        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
//        public static DimensionEnum GetDimensionEnumWithFallback(this Inlet inlet)
//        {
//            if (inlet == null) throw new ArgumentNullException(nameof(inlet));

//            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
//            if (dimensionEnum == DimensionEnum.Inherit)
//            {
//                return inlet.Operator.GetStandardDimensionEnumWithFallback();
//            }

//            return dimensionEnum;
//        }

//        /// <summary>In case dimension is 'Inherit', the dimension is resolved from context.</summary>
//        public static DimensionEnum GetDimensionEnumWithFallback(Outlet outlet)
//        {
//            if (outlet == null) throw new ArgumentNullException(nameof(outlet));

//            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
//            if (dimensionEnum == DimensionEnum.Inherit)
//            {
//                return outlet.Operator.GetStandardDimensionEnumWithFallback();
//            }

//            return dimensionEnum;
//        }

//        // TODO: More overloads?
//    }
//}
