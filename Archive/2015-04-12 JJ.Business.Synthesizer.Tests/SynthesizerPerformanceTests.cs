using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Validation;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using System.IO;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Framework.Testing;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Infos;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerPerformanceTests
    {
        private class PerformanceResult
        {
            public ITestOperatorCalculator Calculator { get; set; }
            public long Milliseconds { get; set; }
        }

        [TestMethod]
        public void Test_Synthesizer_Performance()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = EntityFactory.CreateMockOperatorStructure(factory);

                IList<PerformanceResult> results = new PerformanceResult[] 
                {
                    new PerformanceResult { Calculator = new TestOperatorCalculator_WithWrappersAndNullChecks() },
                    new PerformanceResult { Calculator = new TestOperatorCalculator_WithWrappersAndNullChecks_MoreOperators(0) },
                    new PerformanceResult { Calculator = new TestOperatorCalculator_WithoutWrappers() },
                    new PerformanceResult { Calculator = new TestOperatorCalculator_WithoutWrappers_MoreOperators(0) },
                    new PerformanceResult { Calculator = new TestOperatorCalculator_WithoutWrappersOrNullChecks() }
                };

                int repeats = 88200;

                foreach (PerformanceResult result in results)
                {
			        ITestOperatorCalculator calculator = result.Calculator;

                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < repeats; i++)
                    {
                        double value = calculator.Calculate(outlet, 0);
                    }
                    sw.Stop();
                    long ms = sw.ElapsedMilliseconds;
                    result.Milliseconds = ms;
                }

                string message = String.Join("," + Environment.NewLine, results.Select(x => String.Format("{0}: {1}ms", x.Calculator.GetType().Name, x.Milliseconds)));
                Assert.Inconclusive(message);
            }
        }
    }
}
