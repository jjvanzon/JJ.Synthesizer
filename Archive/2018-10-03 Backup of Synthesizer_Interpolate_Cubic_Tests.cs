//using System;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Framework.Mathematics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//// ReSharper disable UnusedVariable
//// ReSharper disable AccessToModifiedClosure
//// ReSharper disable SuggestVarOrType_SimpleTypes

//namespace JJ.Business.Synthesizer.Tests
//{
//    [TestClass]
//    public class Synthesizer_Interpolate_Cubic_Tests : Synthesizer_Interpolate_Tests_Base
//    {
//        // LookAhead Forward

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Cubic_LookAhead_Forward_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Cubic_LookAhead_Forward(CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Cubic_LookAhead_Forward(CalculationEngineEnum calculationEngineEnum)
//        {
//            const double xMinus1 = Math.PI * -12 / 12;
//            const double x0 = Math.PI * -09 / 12;
//            const double x1 = Math.PI * -06 / 12;
//            const double x2 = Math.PI * -03 / 12;
//            const double x3 = Math.PI * 00 / 12;
//            const double x4 = Math.PI * 03 / 12;
//            const double x5 = Math.PI * 06 / 12;
//            const double x6 = Math.PI * 09 / 12;
//            const double x7 = Math.PI * 12 / 12;
//            const double x8 = Math.PI * 15 / 12;
//            const double x9 = Math.PI * 18 / 12;

//            double yMinus1 = Math.Sin(xMinus1);
//            double y0 = Math.Sin(x0);
//            double y1 = Math.Sin(x1);
//            double y2 = Math.Sin(x2);
//            double y3 = Math.Sin(x3);
//            double y4 = Math.Sin(x4);
//            double y5 = Math.Sin(x5);
//            double y6 = Math.Sin(x6);
//            double y7 = Math.Sin(x7);
//            double y8 = Math.Sin(x8);
//            double y9 = Math.Sin(x9);

//            Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Cubic,
//                FollowingModeEnum.LookAhead,
//                slowRate: 4.0 / Math.PI,
//                new[]
//                {
//                    (x0, y0),
//                    (Math.PI * -08 / 12, Interpolator.CubicSmoothSlope(xMinus1, x0, x1, x2, yMinus1, y0, y1, y2, (2 * x0 + x1) / 3)),
//                    (Math.PI * -07 / 12, Interpolator.CubicSmoothSlope(xMinus1, x0, x1, x2, yMinus1, y0, y1, y2, (x0 + 2 * x1) / 3)),
//                    (x1, y1),
//                    (Math.PI * -05 / 12, Interpolator.CubicSmoothSlope(x0, x1, x2, x3, y0, y1, y2, y3, (2 * x1 + x2) / 3)),
//                    (Math.PI * -04 / 12, Interpolator.CubicSmoothSlope(x0, x1, x2, x3, y0, y1, y2, y3, (x1 + 2 * x2) / 3)),
//                    (x2, y2),
//                    (Math.PI * -02 / 12, Interpolator.CubicSmoothSlope(x1, x2, x3, x4, y1, y2, y3, y4, (2 * x2 + x3) / 3)),
//                    (Math.PI * -01 / 12, Interpolator.CubicSmoothSlope(x1, x2, x3, x4, y1, y2, y3, y4, (x2 + 2 * x3) / 3)),
//                    (x3, y3),
//                    (Math.PI * 01 / 12, Interpolator.CubicSmoothSlope(x2, x3, x4, x5, y2, y3, y4, y5, (2 * x3 + x4) / 3)),
//                    (Math.PI * 02 / 12, Interpolator.CubicSmoothSlope(x2, x3, x4, x5, y2, y3, y4, y5, (x3 + 2 * x4) / 3)),
//                    (x4, y4),
//                    (Math.PI * 04 / 12, Interpolator.CubicSmoothSlope(x3, x4, x5, x6, y3, y4, y5, y6, (2 * x4 + x5) / 3)),
//                    (Math.PI * 05 / 12, Interpolator.CubicSmoothSlope(x3, x4, x5, x6, y3, y4, y5, y6, (x4 + 2 * x5) / 3)),
//                    (x5, y5),
//                    (Math.PI * 07 / 12, Interpolator.CubicSmoothSlope(x4, x5, x6, x7, y4, y5, y6, y7, (2 * x5 + x6) / 3)),
//                    (Math.PI * 08 / 12, Interpolator.CubicSmoothSlope(x4, x5, x6, x7, y4, y5, y6, y7, (x5 + 2 * x6) / 3)),
//                    (x6, y6),
//                    (Math.PI * 10 / 12, Interpolator.CubicSmoothSlope(x5, x6, x7, x8, y5, y6, y7, y8, (2 * x6 + x7) / 3)),
//                    (Math.PI * 11 / 12, Interpolator.CubicSmoothSlope(x5, x6, x7, x8, y5, y6, y7, y8, (x6 + 2 * x7) / 3)),
//                    (x7, y7),
//                    (Math.PI * 13 / 12, Interpolator.CubicSmoothSlope(x6, x7, x8, x9, y6, y7, y8, y9, (2 * x7 + x8) / 3)),
//                    (Math.PI * 14 / 12, Interpolator.CubicSmoothSlope(x6, x7, x8, x9, y6, y7, y8, y9, (x7 + 2 * x8) / 3)),
//                    (x8, y8)
//                },
//                plotLineCount: 29);
//        }

//        // LookAhead Backward

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Cubic_LookAhead_Backward_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Cubic_LookAhead_Backward(CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Cubic_LookAhead_Backward(CalculationEngineEnum calculationEngineEnum)
//        {
//            double y0 = Math.Sin(Math.PI * 09 / 12);
//            double y1 = Math.Sin(Math.PI * 06 / 12);
//            double y2 = Math.Sin(Math.PI * 03 / 12);
//            double y3 = Math.Sin(Math.PI * 00 / 12);
//            double y4 = Math.Sin(Math.PI * -03 / 12);
//            double y5 = Math.Sin(Math.PI * -06 / 12);
//            double y6 = Math.Sin(Math.PI * -09 / 12);
//            double y7 = Math.Sin(Math.PI * -12 / 12);
//            double y8 = Math.Sin(Math.PI * -15 / 12);

//            Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Cubic,
//                FollowingModeEnum.LookAhead,
//                slowRate: 4.0 / Math.PI,
//                new[]
//                {
//                    (Math.PI * 09 / 12, y0),
//                    (Math.PI * 08 / 12, (2 * y0 + y1) / 3),
//                    (Math.PI * 07 / 12, (y0 + 2 * y1) / 3),
//                    (Math.PI * 06 / 12, y1),
//                    (Math.PI * 05 / 12, (2 * y1 + y2) / 3),
//                    (Math.PI * 04 / 12, (y1 + 2 * y2) / 3),
//                    (Math.PI * 03 / 12, y2),
//                    (Math.PI * 02 / 12, (2 * y2 + y3) / 3),
//                    (Math.PI * 01 / 12, (y2 + 2 * y3) / 3),
//                    (Math.PI * 00 / 12, y3),
//                    (Math.PI * -01 / 12, (2 * y3 + y4) / 3),
//                    (Math.PI * -02 / 12, (y3 + 2 * y4) / 3),
//                    (Math.PI * -03 / 12, y4),
//                    (Math.PI * -04 / 12, (2 * y4 + y5) / 3),
//                    (Math.PI * -05 / 12, (y4 + 2 * y5) / 3),
//                    (Math.PI * -06 / 12, y5),
//                    (Math.PI * -07 / 12, (2 * y5 + y6) / 3),
//                    (Math.PI * -08 / 12, (y5 + 2 * y6) / 3),
//                    (Math.PI * -09 / 12, y6),
//                    (Math.PI * -10 / 12, (2 * y6 + y7) / 3),
//                    (Math.PI * -11 / 12, (y6 + 2 * y7) / 3),
//                    (Math.PI * -12 / 12, y7),
//                    (Math.PI * -13 / 12, (2 * y7 + y8) / 3),
//                    (Math.PI * -14 / 12, (y7 + 2 * y8) / 3),
//                    (Math.PI * -15 / 12, y8)
//                }, plotLineCount: 29);
//        }

//        // LagBehind Forward

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Cubic_LagBehind_Forward_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Cubic_LagBehind_Forward(CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Cubic_LagBehind_Forward(CalculationEngineEnum calculationEngineEnum)
//        {
//            const double xMinus1 = Math.PI * -12 / 12;
//            const double x0 = Math.PI * -09 / 12;
//            const double x1 = Math.PI * -06 / 12;
//            const double x2 = Math.PI * -03 / 12;
//            const double x3 = Math.PI * 00 / 12;
//            const double x4 = Math.PI * 03 / 12;
//            const double x5 = Math.PI * 06 / 12;
//            const double x6 = Math.PI * 09 / 12;
//            const double x7 = Math.PI * 12 / 12;
//            const double x8 = Math.PI * 15 / 12;
//            const double x9 = Math.PI * 18 / 12;

//            double yMinus1 = Math.Sin(Math.PI * -12 / 12);
//            double y0 = Math.Sin(Math.PI * -12 / 12);
//            double y1 = Math.Sin(Math.PI * -12 / 12);
//            double y2 = Math.Sin(Math.PI * -12 / 12);
//            double y3 = Math.Sin(Math.PI * -05 / 12);
//            double y4 = Math.Sin(Math.PI * -02 / 12);
//            double y5 = Math.Sin(Math.PI * 01 / 12);
//            double y6 = Math.Sin(Math.PI * 04 / 12);
//            double y7 = Math.Sin(Math.PI * 07 / 12);
//            double y8 = Math.Sin(Math.PI * 10 / 12);
//            double y9 = Math.Sin(Math.PI * 13 / 12);

//            Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Cubic,
//                FollowingModeEnum.LagBehind,
//                slowRate: 4.0 / Math.PI,
//                new[]
//                {
//                    (x0, y0),
//                    (Math.PI * -08 / 12, Interpolator.CubicSmoothSlope(xMinus1, x0, x1, x2, yMinus1, y0, y1, y2, (2 * x0 + x1) / 3)),
//                    (Math.PI * -07 / 12, Interpolator.CubicSmoothSlope(xMinus1, x0, x1, x2, yMinus1, y0, y1, y2, (x0 + 2 * x1) / 3)),
//                    (x1, y1),
//                    (Math.PI * -05 / 12, Interpolator.CubicSmoothSlope(x0, x1, x2, x3, y0, y1, y2, y3, (2 * x1 + x2) / 3)),
//                    (Math.PI * -04 / 12, Interpolator.CubicSmoothSlope(x0, x1, x2, x3, y0, y1, y2, y3, (x1 + 2 * x2) / 3)),
//                    (x2, y2),
//                    (Math.PI * -02 / 12, Interpolator.CubicSmoothSlope(x1, x2, x3, x4, y1, y2, y3, y4, (2 * x2 + x3) / 3)),
//                    (Math.PI * -01 / 12, Interpolator.CubicSmoothSlope(x1, x2, x3, x4, y1, y2, y3, y4, (x2 + 2 * x3) / 3)),
//                    (x3, y3),
//                    (Math.PI * 01 / 12, Interpolator.CubicSmoothSlope(x2, x3, x4, x5, y2, y3, y4, y5, (2 * x3 + x4) / 3)),
//                    (Math.PI * 02 / 12, Interpolator.CubicSmoothSlope(x2, x3, x4, x5, y2, y3, y4, y5, (x3 + 2 * x4) / 3)),
//                    (x4, y4),
//                    (Math.PI * 04 / 12, Interpolator.CubicSmoothSlope(x3, x4, x5, x6, y3, y4, y5, y6, (2 * x4 + x5) / 3)),
//                    (Math.PI * 05 / 12, Interpolator.CubicSmoothSlope(x3, x4, x5, x6, y3, y4, y5, y6, (x4 + 2 * x5) / 3)),
//                    (x5, y5),
//                    (Math.PI * 07 / 12, Interpolator.CubicSmoothSlope(x4, x5, x6, x7, y4, y5, y6, y7, (2 * x5 + x6) / 3)),
//                    (Math.PI * 08 / 12, Interpolator.CubicSmoothSlope(x4, x5, x6, x7, y4, y5, y6, y7, (x5 + 2 * x6) / 3)),
//                    (x6, y6),
//                    (Math.PI * 10 / 12, Interpolator.CubicSmoothSlope(x5, x6, x7, x8, y5, y6, y7, y8, (2 * x6 + x7) / 3)),
//                    (Math.PI * 11 / 12, Interpolator.CubicSmoothSlope(x5, x6, x7, x8, y5, y6, y7, y8, (x6 + 2 * x7) / 3)),
//                    (x7, y7),
//                    (Math.PI * 13 / 12, Interpolator.CubicSmoothSlope(x6, x7, x8, x9, y6, y7, y8, y9, (2 * x7 + x8) / 3)),
//                    (Math.PI * 14 / 12, Interpolator.CubicSmoothSlope(x6, x7, x8, x9, y6, y7, y8, y9, (x7 + 2 * x8) / 3)),
//                    (x8, y8)
//                }, plotLineCount: 29);
//        }

//        // LagBehind Backward

//        [TestMethod]
//        public void Test_Synthesizer_Interpolate_Cubic_LagBehind_Backward_WithCalculatorClasses()
//            => Test_Synthesizer_Interpolate_Cubic_LagBehind_Backward(CalculationEngineEnum.CalculatorClasses);

//        private void Test_Synthesizer_Interpolate_Cubic_LagBehind_Backward(CalculationEngineEnum calculationEngineEnum)
//        {
//            double y0 = Math.Sin(Math.PI * 09 / 12);
//            double y1 = Math.Sin(Math.PI * 09 / 12);
//            double y2 = Math.Sin(Math.PI * 05 / 12);
//            double y3 = Math.Sin(Math.PI * 02 / 12);
//            double y4 = Math.Sin(Math.PI * -01 / 12);
//            double y5 = Math.Sin(Math.PI * -04 / 12);
//            double y6 = Math.Sin(Math.PI * -07 / 12);
//            double y7 = Math.Sin(Math.PI * -10 / 12);
//            double y8 = Math.Sin(Math.PI * -13 / 12);

//            Test_Synthesizer_Interpolate_Base(
//                calculationEngineEnum,
//                InterpolationTypeEnum.Cubic,
//                FollowingModeEnum.LagBehind,
//                slowRate: 4.0 / Math.PI,
//                new[]
//                {
//                    (Math.PI * 09 / 12, y0),
//                    (Math.PI * 08 / 12, (2 * y0 + y1) / 3),
//                    (Math.PI * 07 / 12, (y0 + 2 * y1) / 3),
//                    (Math.PI * 06 / 12, y1),
//                    (Math.PI * 05 / 12, (2 * y1 + y2) / 3),
//                    (Math.PI * 04 / 12, (y1 + 2 * y2) / 3),
//                    (Math.PI * 03 / 12, y2),
//                    (Math.PI * 02 / 12, (2 * y2 + y3) / 3),
//                    (Math.PI * 01 / 12, (y2 + 2 * y3) / 3),
//                    (Math.PI * 00 / 12, y3),
//                    (Math.PI * -01 / 12, (2 * y3 + y4) / 3),
//                    (Math.PI * -02 / 12, (y3 + 2 * y4) / 3),
//                    (Math.PI * -03 / 12, y4),
//                    (Math.PI * -04 / 12, (2 * y4 + y5) / 3),
//                    (Math.PI * -05 / 12, (y4 + 2 * y5) / 3),
//                    (Math.PI * -06 / 12, y5),
//                    (Math.PI * -07 / 12, (2 * y5 + y6) / 3),
//                    (Math.PI * -08 / 12, (y5 + 2 * y6) / 3),
//                    (Math.PI * -09 / 12, y6),
//                    (Math.PI * -10 / 12, (2 * y6 + y7) / 3),
//                    (Math.PI * -11 / 12, (y6 + 2 * y7) / 3),
//                    (Math.PI * -12 / 12, y7),
//                    (Math.PI * -13 / 12, (2 * y7 + y8) / 3),
//                    (Math.PI * -14 / 12, (y7 + 2 * y8) / 3),
//                    (Math.PI * -15 / 12, y8)
//                }, plotLineCount: 29);
//        }
//    }
//}