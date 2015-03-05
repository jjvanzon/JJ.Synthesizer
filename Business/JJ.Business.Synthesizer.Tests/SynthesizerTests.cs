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
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Tests.Helpers;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        [TestMethod]
        public void Test_Synthesizer()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory x = TestHelper.CreateOperatorFactory(context);

                Add add = x.Add(x.Value(2), x.Value(3));
                Substract substract = x.Substract(add, x.Value(1));

                IValidator validator = new RecursiveOperatorValidator(substract.Operator);
                validator.Verify();

                ISoundCalculator calculator = new SoundCalculator3();
                double value = calculator.CalculateValue(add, 0);
                Assert.AreEqual(5, value, 0.0001);
                value = calculator.CalculateValue(substract, 0);
                Assert.AreEqual(4, value, 0.0001);

                // Test recursive validator
                CultureHelper.SetThreadCulture("nl-NL");

                add.OperandA = null;
                substract.OperandB.Value = 0;
                substract.Operator.Inlets[0].Name = "134";

                IValidator validator2 = new RecursiveOperatorValidator(substract.Operator);
                IValidator warningValidator = new RecursiveOperatorWarningValidator(substract.Operator);
            }
        }

        private class PerformanceResult
        {
            public ISoundCalculator Calculator { get; set; }
            public long Milliseconds { get; set; }
        }

        [TestMethod]
        public void Test_Synthesizer_Performance()
        {
            // TODO: Rename test SoundCalculator* classes to OperatorCalculator*.

            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = MockFactory.CreateMockOperatorStructure(factory);

                IList<PerformanceResult> results = new PerformanceResult[] 
                {
                    new PerformanceResult { Calculator = new SoundCalculator1() },
                    new PerformanceResult { Calculator = new SoundCalculator2() },
                    new PerformanceResult { Calculator = new SoundCalculator3() }
                };

                int repeats = 88200;

                foreach (PerformanceResult result in results)
                {
			        ISoundCalculator calculator = result.Calculator;

                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < repeats; i++)
                    {
                        double value = calculator.CalculateValue(outlet, 0);
                    }
                    sw.Stop();
                    long ms = sw.ElapsedMilliseconds;
                    result.Milliseconds = ms;
                }

                string message = String.Join(", ", results.Select(x => String.Format("{0}: {1}ms", x.Calculator.GetType().Name, x.Milliseconds)));
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddValidator()
        {
            IValidator validator1 = new AddValidator(new Operator
            {
                Inlets = new Inlet[]
                { 
                    new Inlet { Name = "qwer"},
                    new Inlet { Name = "asdf" },
                },
                Outlets = new Outlet[]
                {
                    new Outlet { Name = "zxcv" }
                }
            });

            IValidator validator2 = new AddValidator(new Operator());

            bool isValid = validator1.IsValid &&
                           validator2.IsValid;
        }

        [TestMethod]
        public void Test_Synthesizer_WarningValidators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);

                IValidator validator1 = new AddWarningValidator(factory.Add().Operator);
                IValidator validator2 = new ValueOperatorWarningValidator(factory.Value().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }

        [TestMethod]
        public void Test_Synthesizer_Adder()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);

                ValueOperator val1 = factory.Value(1);
                ValueOperator val2 = factory.Value(2);
                ValueOperator val3 = factory.Value(3);
                Adder adder = factory.Adder(val1, val2, val3);

                IValidator validator = new AdderValidator(adder.Operator);
                validator.Verify();

                var calculator = new OperatorCalculator();
                double value = calculator.CalculateValue(adder, 0);

                adder.Operator.Inlets[0].Name = "qwer";
                IValidator validator2 = new AdderValidator(adder.Operator);
                //validator2.Verify();
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ShorterCodeNotation()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory x = TestHelper.CreateOperatorFactory(context);

                Substract substract = x.Substract(x.Add(x.Value(2), x.Value(3)), x.Value(1));

                Substract substract2 = x.Substract
                (
                    x.Add
                    (
                        x.Value(2),
                        x.Value(3)
                    ),
                    x.Value(1)
                );
            }
        }

        [TestMethod]
        public void Test_Synthesizer_SineWithCurve()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                CurveFactory curveFactory = TestHelper.CreateCurveFactory(context);
                Curve curve = curveFactory.Create(1, 0, 1, 0.8, null, null, 0.8, 0);

                OperatorFactory f = TestHelper.CreateOperatorFactory(context);
                Sine sine = f.Sine(f.CurveIn(curve), f.Value(440));

                CultureHelper.SetThreadCulture("nl-NL");
                IValidator[] validators = 
                {
                    new CurveValidator(curve), 
                    new VersatileOperatorValidator(sine.Operator),
                    new VersatileOperatorWarningValidator(sine.Operator)
                };
                validators.ForEach(x => x.Verify());

                var calculator = new OperatorCalculator();
                var values = new double[]
                {
                    calculator.CalculateValue(sine, 0.00),
                    calculator.CalculateValue(sine, 0.05),
                    calculator.CalculateValue(sine, 0.10),
                    calculator.CalculateValue(sine, 0.15),
                    calculator.CalculateValue(sine, 0.20),
                    calculator.CalculateValue(sine, 0.25),
                    calculator.CalculateValue(sine, 0.30),
                    calculator.CalculateValue(sine, 0.35),
                    calculator.CalculateValue(sine, 0.40),
                    calculator.CalculateValue(sine, 0.45),
                    calculator.CalculateValue(sine, 0.50),
                    calculator.CalculateValue(sine, 0.55),
                    calculator.CalculateValue(sine, 0.60),
                    calculator.CalculateValue(sine, 0.65),
                    calculator.CalculateValue(sine, 0.70),
                    calculator.CalculateValue(sine, 0.75),
                    calculator.CalculateValue(sine, 0.80),
                    calculator.CalculateValue(sine, 0.85),
                    calculator.CalculateValue(sine, 0.90),
                    calculator.CalculateValue(sine, 0.95),
                    calculator.CalculateValue(sine, 1.00)
                };
            }
        }
    }
}
