//using System;
//using JJ.Business.Synthesizer.Configuration;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Tests.Helpers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace JJ.Business.Synthesizer.Tests
//{
//    [TestClass]
//    public class Synthesizer_SimpleArithmeticWith2Inputs_Tests
//    {
//        private static readonly double[] _values = { -31, -6.75, 0, 17.5, 41.75, 66 };

//        // Divide

//        [TestMethod]
//        public void Test_Synthesizer_Divide_WithRoslyn() => Test_Synthesizer_Divide(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Divide_WithCalculatorClasses() => Test_Synthesizer_Divide(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Divide(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest(nameof(SystemPatchNames.Divide), (a, b) => a / b, calculationMethodEnum);

//        // Power

//        [TestMethod]
//        public void Test_Synthesizer_Power_WithRoslyn() => Test_Synthesizer_Power(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Power_WithCalculatorClasses() => Test_Synthesizer_Power(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Power(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest(nameof(SystemPatchNames.Power), Math.Pow, calculationMethodEnum);

//        // Remainder

//        [TestMethod]
//        public void Test_Synthesizer_Remainder_WithRoslyn() => Test_Synthesizer_Remainder(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Remainder_WithCalculatorClasses() => Test_Synthesizer_Remainder(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Remainder(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest(nameof(SystemPatchNames.Remainder), (a, b) => a % b, calculationMethodEnum);

//        // Subtract

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_VarB_WithRoslyn() 
//            => Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_VarB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum.CalculatorClasses);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_VarB_WithRoslyn() 
//            => Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_VarB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum.CalculatorClasses);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_ConstB_WithRoslyn() 
//            => Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_ConstB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum.CalculatorClasses);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_ConstB_WithRoslyn() 
//            => Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_ConstB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum calculationMethodEnum)
//            => Test_Synthesizer_Subtract(calculationMethodEnum);

//        private void Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum calculationMethodEnum)
//            => Test_Synthesizer_Subtract(calculationMethodEnum, constB: 321);

//        private void Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum calculationMethodEnum)
//            => Test_Synthesizer_Subtract(calculationMethodEnum, constA: 123);

//        private void Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum calculationMethodEnum)
//            => Test_Synthesizer_Subtract(calculationMethodEnum, 123, 321);

//        private void Test_Synthesizer_Subtract(CalculationMethodEnum calculationMethodEnum, double? constA = null, double? constB = null)
//            => ExecuteTest(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum, constA, constB);

//        // Generalized Method

//        private void ExecuteTest(
//            string systemPatchName,
//            Func<double, double, double> func,
//            CalculationMethodEnum calculationMethodEnum,
//            double? constA = null,
//            double? constB = null)
//            => TestExecutor.TestWith2Inputs(
//                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
//                func,
//                DimensionEnum.A,
//                _values,
//                DimensionEnum.B,
//                _values,
//                calculationMethodEnum,
//                constA,
//                constB);
//    }
//}