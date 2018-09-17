//using System;
//using System.Runtime.CompilerServices;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Tests.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//// ReSharper disable LocalizableElement

//namespace JJ.Business.Synthesizer.Tests
//{
//    [TestClass]
//    public class TrigonometryTests
//    {
//        private const DimensionEnum DIMENSION_ENUM = DimensionEnum.Number;

//        [TestMethod]
//        public void Test_Synthesizer_Sin()
//            => ExecuteTrigonometryTest(Math.Sin, x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_Cos()
//            => ExecuteTrigonometryTest(Math.Cos, x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_Tan()
//            => ExecuteTrigonometryTest(Math.Tan, x => x.New(nameof(SystemPatchNames.Tan), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_SinH()
//            => ExecuteTrigonometryTest(Math.Sinh, x => x.New(nameof(SystemPatchNames.SinH), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_CosH()
//            => ExecuteTrigonometryTest(Math.Cosh, x => x.New(nameof(SystemPatchNames.CosH), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_TanH()
//            => ExecuteTrigonometryTest(Math.Tanh, x => x.New(nameof(SystemPatchNames.TanH), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_ArcSin()
//            => ExecuteTrigonometryTest(Math.Asin, x => x.New(nameof(SystemPatchNames.ArcSin), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_ArcCos()
//            => ExecuteTrigonometryTest(Math.Acos, x => x.New(nameof(SystemPatchNames.ArcCos), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_ArcTan()
//            => ExecuteTrigonometryTest(Math.Atan, x => x.New(nameof(SystemPatchNames.ArcTan), x.PatchInlet(DIMENSION_ENUM)));

//        private void ExecuteTrigonometryTest(
//            Func<double, double> func,
//            Func<OperatorFactory, Outlet> operatorCreationDelegate,
//            [CallerMemberName] string callerMemberName = null)
//        {
//            Console.WriteLine($"Executing test {callerMemberName}.");

//            TestHelper.ExecuteTest(
//                DIMENSION_ENUM,
//                func,
//                operatorCreationDelegate,
//                new[]
//                {
//                    Math.PI * 0.00,
//                    Math.PI * 0.25,
//                    Math.PI * 0.50,
//                    Math.PI * 0.75,
//                    Math.PI * 1.00,
//                    Math.PI * 1.25,
//                    Math.PI * 1.50,
//                    Math.PI * 1.75,
//                    Math.PI * 2.00
//                });
//        }
//    }
//}