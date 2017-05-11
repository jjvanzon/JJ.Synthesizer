//using JJ.Business.Synthesizer.Calculation.Arrays;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Calculation
//{
//    internal static class CurveArrayCalculatorFactory
//    {
//        public static ICalculatorWithPosition CreateCurveArrayCalculator(Curve curve)
//        {
//            if (curve == null) throw new NullException(() => curve);

//            ArrayDto arrayDto = CurveArrayHelper.ConvertToArrayDto(curve);

//            return CreateCurveArrayCalculator(arrayDto);
//        }

//        private static ICalculatorWithPosition CreateCurveArrayCalculator(ArrayDto arrayDto)
//        {
//            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            if (arrayDto.MinPosition == 0.0)
//            {
//                var arrayCalculator = new ArrayCalculator_MinPositionZero_Line(
//                    arrayDto.Array,
//                    arrayDto.Rate,
//                    arrayDto.ValueBefore,
//                    arrayDto.ValueAfter);

//                return arrayCalculator;
//            }
//            else
//            {
//                var arrayCalculator = new ArrayCalculator_MinPosition_Line(
//                    arrayDto.Array,
//                    arrayDto.Rate,
//                    arrayDto.MinPosition,
//                    arrayDto.ValueBefore,
//                    arrayDto.ValueAfter);

//                return arrayCalculator;
//            }
//        }
//    }
//}
