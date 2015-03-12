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

                IOperatorCalculator calculator = new OperatorCalculator_WithoutWrappersOrNullChecks();
                double value = calculator.CalculateValue(add, 0);
                Assert.AreEqual(5, value, 0.0001);
                value = calculator.CalculateValue(substract, 0);
                Assert.AreEqual(4, value, 0.0001);

                // Test recursive validator
                CultureHelper.SetThreadCulture("nl-NL");

                add.OperandA = null;
                substract.OperandB.Operator.AsValueOperator.Value = 0;
                substract.Operator.Inlets[0].Name = "134";

                IValidator validator2 = new RecursiveOperatorValidator(substract.Operator);
                IValidator warningValidator = new RecursiveOperatorWarningValidator(substract.Operator);
            }
        }

        private class PerformanceResult
        {
            public IOperatorCalculator Calculator { get; set; }
            public long Milliseconds { get; set; }
        }

        [TestMethod]
        public void Test_Synthesizer_Performance()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = EntityFactory.CreateMockOperatorStructure(factory);

                IList<PerformanceResult> results = new PerformanceResult[] 
                {
                    new PerformanceResult { Calculator = new OperatorCalculator_WithWrappersAndNullChecks() },
                    new PerformanceResult { Calculator = new OperatorCalculator_WithWrappersAndNullChecks_MoreOperators(0) },
                    new PerformanceResult { Calculator = new OperatorCalculator_WithoutWrappers() },
                    new PerformanceResult { Calculator = new OperatorCalculator_WithoutWrappers_MoreOperators(0) },
                    new PerformanceResult { Calculator = new OperatorCalculator_WithoutWrappersOrNullChecks() }
                };

                int repeats = 88200;

                foreach (PerformanceResult result in results)
                {
			        IOperatorCalculator calculator = result.Calculator;

                    Stopwatch sw = Stopwatch.StartNew();
                    for (int i = 0; i < repeats; i++)
                    {
                        double value = calculator.CalculateValue(outlet, 0);
                    }
                    sw.Stop();
                    long ms = sw.ElapsedMilliseconds;
                    result.Milliseconds = ms;
                }

                string message = String.Join("," + Environment.NewLine, results.Select(x => String.Format("{0}: {1}ms", x.Calculator.GetType().Name, x.Milliseconds)));
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

                ValueOperatorWrapper val1 = factory.Value(1);
                ValueOperatorWrapper val2 = factory.Value(2);
                ValueOperatorWrapper val3 = factory.Value(3);
                Adder adder = factory.Adder(val1, val2, val3);

                IValidator validator = new AdderValidator(adder.Operator);
                validator.Verify();

                var calculator = new OperatorCalculator(channelIndex: 0);
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
                Curve curve = curveFactory.CreateCurve(1, 0, 1, 0.8, null, null, 0.8, 0);

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

                var calculator = new OperatorCalculator(channelIndex: 0);
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

        [TestMethod]
        public void Test_Synthesizer_TimePowerWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                OperatorFactory operatorFactory = TestHelper.CreateOperatorFactory(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperator = operatorFactory.Sample(sample);
                Outlet effect = EntityFactory.CreateTimePowerEffectWithEcho(operatorFactory, sampleOperator);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_TimePowerWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputCalculator.Execute();
                sw.Stop();

                string message = String.Format("{0}ms", sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_MultiplyWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                OperatorFactory operatorFactory = TestHelper.CreateOperatorFactory(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperator = operatorFactory.Sample(sample);
                Outlet effect = EntityFactory.CreateMultiplyWithEcho(operatorFactory, sampleOperator);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_TimePowerWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputCalculator.Execute();
                sw.Stop();

                string message = String.Format("{0}ms", sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_HardCodedTimePowerWithEcho()
        {
            const double seconds = 6.5;
            const double samplingRate = 44100.0;

            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample);

                Stopwatch sw = Stopwatch.StartNew();
                using (Stream destStream = new FileStream("HardCodedTimePowerWithEchoCalculator.raw", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (BinaryWriter writer = new BinaryWriter(destStream))
                    {
                        int destSampleCount = (int)(samplingRate * seconds);

                        double t = 0;
                        double dt = 1.0 / samplingRate;

                        for (int i = 0; i < destSampleCount; i++)
                        {
                            double value = hardCodedCalculator.CalculateTimePowerWithEcho(t);
                            short convertedValue = (short)value;

                            writer.Write(convertedValue);

                            t += dt;
                        }
                    }
                }
                sw.Stop();

                string message = String.Format("{0}ms", sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_HardCodedMultiplyWithEcho()
        {
            const double seconds = 6.5;
            const double samplingRate = 44100.0;

            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample);

                Stopwatch sw = Stopwatch.StartNew();
                using (Stream destStream = new FileStream("HardCodedTimePowerWithEchoCalculator.raw", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (BinaryWriter writer = new BinaryWriter(destStream))
                    {
                        int destSampleCount = (int)(samplingRate * seconds);

                        double t = 0;
                        double dt = 1.0 / samplingRate;

                        for (int i = 0; i < destSampleCount; i++)
                        {
                            double value = hardCodedCalculator.CalculateMultiplyWithEcho(t);
                            short convertedValue = (short)value;

                            writer.Write(convertedValue);

                            t += dt;
                        }
                    }
                }
                sw.Stop();

                string message = String.Format("{0}ms", sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OperatorCalculatorNew()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(x.Value(1), x.Value(2));
                var calculator = new OperatorCalculatorNew(outlet.Operator);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(3.0, result, 0.0001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OperatorCalculatorNew_WithNullInlet()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(null, x.Value(2));
                var calculator = new OperatorCalculatorNew(outlet.Operator);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(0.0, result, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OperatorCalculatorNew_NestedOperators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(x.Add(x.Value(1), x.Value(2)), x.Value(4));
                var calculator = new OperatorCalculatorNew(outlet.Operator);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(7.0, result, 0.000000001);
            }
        }

        //[TestMethod]
        //public void CalculateValue()
        //{
        //    double time = 0;

        //    // TimePower
        //    double signal = 10;
        //    double exponent = 1.5;

        //    double timeAbs = Math.Abs(time);
        //    double timeSign = Math.Sign(time);

        //    time = timeSign * Math.Pow(timeAbs, 1 / exponent);

        //    // Echo
        //    double value = 0;
        //    double cumulativeDenominator = 1;
        //    double cumulativeDelay = 0;

        //    for (int i = 0; i < 15; i++)
        //    {
        //        double time2 = time - cumulativeDelay;
        //        double value2 = 8;
        //        value2 /= cumulativeDenominator;

        //        value += value2;

        //        cumulativeDenominator *= 1.5;
        //        cumulativeDelay += 0.25;
        //    }
        //}
    }
}
