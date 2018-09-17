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
//            => ExecuteTest_VarA_VarB(nameof(SystemPatchNames.Divide), (a, b) => a / b, calculationMethodEnum);

//        // Power

//        [TestMethod]
//        public void Test_Synthesizer_Power_WithRoslyn() => Test_Synthesizer_Power(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Power_WithCalculatorClasses() => Test_Synthesizer_Power(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Power(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_VarA_VarB(nameof(SystemPatchNames.Power), Math.Pow, calculationMethodEnum);

//        // Remainder

//        [TestMethod]
//        public void Test_Synthesizer_Remainder_WithRoslyn() => Test_Synthesizer_Remainder(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Remainder_WithCalculatorClasses() => Test_Synthesizer_Remainder(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Remainder(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_VarA_VarB(nameof(SystemPatchNames.Remainder), (a, b) => a % b, calculationMethodEnum);

//        // Subtract

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_VarB_WithRoslyn() 
//            => Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_VarB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Subtract_VarA_VarB(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_VarA_VarB(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_VarB_WithRoslyn()
//            => Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_VarB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Subtract_ConstA_VarB(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_ConstA_VarB(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_ConstB_WithRoslyn()
//            => Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_VarA_ConstB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Subtract_VarA_ConstB(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_VarA_ConstB(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_ConstB_WithRoslyn()
//            => Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum.Roslyn);

//        [TestMethod]
//        public void Test_Synthesizer_Subtract_ConstA_ConstB_WithCalculatorClasses()
//            => Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum.CalculatorClasses);

//        private void Test_Synthesizer_Subtract_ConstA_ConstB(CalculationMethodEnum calculationMethodEnum)
//            => ExecuteTest_ConstA_ConstB(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum);

//        // Generalized Method

//        private void ExecuteTest_VarA_VarB(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
//            => TestExecutor.TestWith2Inputs(
//                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
//                func,
//                DimensionEnum.A,
//                _values,
//                DimensionEnum.B,
//                _values,
//                calculationMethodEnum);

//        private void ExecuteTest_ConstA_VarB(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
//            => TestExecutor.TestWith1Input(
//                x => x.New(systemPatchName, x.Number(123), x.PatchInlet(DimensionEnum.B)),
//                x => func(123, x),
//                DimensionEnum.B,
//                _values,
//                calculationMethodEnum);

//        private void ExecuteTest_VarA_ConstB(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
//            => TestExecutor.TestWith1Input(
//                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.Number(123)),
//                x => func(x, 123),
//                DimensionEnum.B,
//                _values,
//                calculationMethodEnum);

//        private void ExecuteTest_ConstA_ConstB(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
//            => TestExecutor.TestWithoutInputs(
//                x => x.New(systemPatchName, x.Number(321), x.Number(123)),
//                func(321, 123),
//                calculationMethodEnum);
//    }
//}