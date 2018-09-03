using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_LogN_Tests
    {
        private static readonly double[] _xValues = MathHelper.SpreadDoubles(1, 1000, 20);

        [TestMethod]
        public void Test_Synthesizer_LogN_Roslyn()
        {
            // TODO: Using a const base is basically naive about the need for 2 in 1 out tests.
            // It might make code that simplifies consts go off instead of the 'main' case with variables only.

            const double @base = 10;

            TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.LogN), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM), x.Number(@base)),
                x => Math.Log(x, @base),
                _xValues,
                CalculationMethodEnum.Roslyn);
        }
    }
}