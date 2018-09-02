//using System;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Tests.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace JJ.Business.Synthesizer.Tests
//{
//    [TestClass]
//    public class TrigonometryTests
//    {
//        private const DimensionEnum DIMENSION_ENUM = DimensionEnum.Number;

//        private static readonly double[] _xValues =
//        {
//            Math.PI * 0.00,
//            Math.PI * 0.25,
//            Math.PI * 0.50,
//            Math.PI * 0.75,
//            Math.PI * 1.00,
//            Math.PI * 1.25,
//            Math.PI * 1.50,
//            Math.PI * 1.75,
//            Math.PI * 2.00
//        };

//        [TestMethod]
//        public void Test_Synthesizer_Sin()
//            => ExecuteTrigonometryTest(Math.Sin, x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(DIMENSION_ENUM)));

//        [TestMethod]
//        public void Test_Synthesizer_Cos()
//            => ExecuteTrigonometryTest(Math.Cos, x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(DIMENSION_ENUM)));

//        private void ExecuteTrigonometryTest(Func<double, double> func, Func<OperatorFactory, Outlet> operatorCreationDelegate)
//            => TestHelper.ExecuteTest(DIMENSION_ENUM, func, operatorCreationDelegate, _xValues);
//    }
//}