using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Common;
using JJ.Framework.Data;
using JJ.Framework.Validation;
using JJ.Framework.IO;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;
using System.Linq;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        static SynthesizerTests()
        {
            TestHelper.SetConfigurationSections();
        }

        [TestMethod]
        public void Test_Synthesizer()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                var add = x.Add(x.Number(2), x.Number(3));
                var subtract = x.Subtract(add, x.Number(1));

                IPatchCalculator calculator1 = x.CreateCalculator(new CalculatorCache(), add);
                double value = calculator1.Calculate(0, 0);
                Assert.AreEqual(5, value, 0.0001);

                IPatchCalculator calculator2 = x.CreateCalculator(new CalculatorCache(), subtract);
                value = calculator2.Calculate(0, 0);
                Assert.AreEqual(4, value, 0.0001);

                // Test recursive validator
                CultureHelper.SetThreadCultureName("nl-NL");

                add.OperandA = null;
                var valueOperatorWrapper = new Number_OperatorWrapper(subtract.OperandB.Operator);
                valueOperatorWrapper.Number = 0;
                subtract.WrappedOperator.Inlets[0].Name = "134";

                //IValidator validator2 = new OperatorValidator_Recursive(subtract.Operator, repositories.CurveRepository, repositories.SampleRepository, repositories.DocumentRepository, alreadyDone: new HashSet<object>());
                //IValidator warningValidator = new OperatorWarningValidator_Recursive(subtract.Operator, repositories.SampleRepository);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddValidator_IsValidTrue()
        {
            //var op = new Operator
            //{
            //    Inlets = new Inlet[]
            //    { 
            //        new Inlet { Name = "qwer"},
            //        new Inlet { Name = "asdf" },
            //    },
            //    Outlets = new Outlet[]
            //    {
            //        new Outlet { Name = "zxcv" }
            //    }
            //});

            //var op2 = new Operator();

            //IValidator validator1 = new OperatorValidator_Add(
            //IValidator validator2 = new OperatorValidator_Add(new Operator());

            //bool isValid = validator1.IsValid &&
            //               validator2.IsValid;

            //Assert.IsTrue(isValid);

            Assert.Inconclusive("Test method was outcommented");
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_WarningValidators()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager patchManager = new PatchManager(new PatchRepositories(repositories));

                IValidator validator1 = new OperatorWarningValidator_Add(patchManager.Add().WrappedOperator);
                IValidator validator2 = new OperatorWarningValidator_Number(patchManager.Number().WrappedOperator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }

        [TestMethod]
        public void Test_Synthesizer_InterpretedPatchCalculator_Adder()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager patchManager = new PatchManager(new PatchRepositories(repositories));

                Number_OperatorWrapper val1 = patchManager.Number(1);
                Number_OperatorWrapper val2 = patchManager.Number(2);
                Number_OperatorWrapper val3 = patchManager.Number(3);
                Adder_OperatorWrapper adder = patchManager.Adder(val1, val2, val3);

                //IValidator validator = new OperatorValidator_Adder(adder.Operator);
                //validator.Verify();

                IPatchCalculator calculator = patchManager.CreateCalculator(new CalculatorCache(), adder);
                double value = calculator.Calculate(0, 0);

                //adder.Operator.Inlets[0].Name = "qwer";
                //IValidator validator2 = new OperatorValidator_Adder(adder.Operator);
                //validator2.Verify();
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ShorterCodeNotation()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                Subtract_OperatorWrapper subtract = x.Subtract(x.Add(x.Number(2), x.Number(3)), x.Number(1));

                Subtract_OperatorWrapper subtract2 = x.Subtract
                (
                    x.Add
                    (
                        x.Number(2),
                        x.Number(3)
                    ),
                    x.Number(1)
                );
            }
        }

        [TestMethod]
        public void Test_Synthesizer_InterpretedPatchCalculator_SineWithCurve_InterpretedMode()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                CurveManager curveManager = new CurveManager(new CurveRepositories(repositories));
                Curve curve = curveManager.Create(1, 0, 1, 0.8, null, null, 0.8, 0);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                var outlet = x.Multiply(x.Curve(curve), x.Sine(x.Number(440)));

                CultureHelper.SetThreadCultureName("nl-NL");

                //IValidator[] validators = 
                //{
                //    new OperatorValidator_Versatile(outlet.Operator, repositories.DocumentRepository),
                //    new OperatorWarningValidator_Versatile(outlet.Operator)
                //};
                //validators.ForEach(y => y.Verify());

                VoidResult result = curveManager.Validate(curve);
                if (!result.Successful)
                {
                    string messages = String.Join(", ", result.Messages.Select(y => y.Text));
                    throw new Exception(messages);
                }

                PatchManager patchManager = new PatchManager(new PatchRepositories(repositories));

                var calculator = patchManager.CreateCalculator(new CalculatorCache(), outlet);
                var values = new double[]
                {
                    calculator.Calculate(0.00, 0),
                    calculator.Calculate(0.05, 0),
                    calculator.Calculate(0.10, 0),
                    calculator.Calculate(0.15, 0),
                    calculator.Calculate(0.20, 0),
                    calculator.Calculate(0.25, 0),
                    calculator.Calculate(0.30, 0),
                    calculator.Calculate(0.35, 0),
                    calculator.Calculate(0.40, 0),
                    calculator.Calculate(0.45, 0),
                    calculator.Calculate(0.50, 0),
                    calculator.Calculate(0.55, 0),
                    calculator.Calculate(0.60, 0),
                    calculator.Calculate(0.65, 0),
                    calculator.Calculate(0.70, 0),
                    calculator.Calculate(0.75, 0),
                    calculator.Calculate(0.80, 0),
                    calculator.Calculate(0.85, 0),
                    calculator.Calculate(0.90, 0),
                    calculator.Calculate(0.95, 0),
                    calculator.Calculate(1.00, 0)
                };
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_TimePowerWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                var sampleManager = new SampleManager(new SampleRepositories(repositories));
                var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                var patchManager = new PatchManager(new PatchRepositories(repositories));

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = patchManager.Sample(sample);
                Outlet effect = EntityFactory.CreateTimePowerEffectWithEcho(patchManager, sampleOperatorOutlet);

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(new CalculatorCache(), effect);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_TimePowerWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_MultiplyWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                var sampleManager =  new SampleManager(new SampleRepositories(repositories));
                var audioFileOutputManager = new AudioFileOutputManager (new AudioFileOutputRepositories(repositories));
                var patchManager = new PatchManager(new PatchRepositories(repositories));

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = patchManager.Sample(sample);
                Outlet effect = EntityFactory.CreateMultiplyWithEcho(patchManager, sampleOperatorOutlet);

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(new CalculatorCache(), effect);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_MultiplyWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_HardCodedTimePowerWithEcho()
        {
            const double seconds = 6.5;
            const double samplingRate = 44100.0;

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample, StreamHelper.StreamToBytes(sampleStream));

                Stopwatch sw = Stopwatch.StartNew();
                using (Stream destStream = new FileStream("Test_Synthesizer_HardCodedTimePowerWithEcho.wav", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (BinaryWriter writer = new BinaryWriter(destStream))
                    {
                        int destSampleCount = (int)(samplingRate * seconds);

                        // Write header
                        var audioFileInfo = new AudioFileInfo
                        {
                            BytesPerValue = 2,
                            ChannelCount = 1,
                            SamplingRate = 44100,
                            SampleCount = destSampleCount
                        };
                        WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
                        writer.WriteStruct(wavHeaderStruct);

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

                double ratio = sw.Elapsed.TotalSeconds / seconds;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_HardCodedMultiplyWithEcho()
        {
            const double seconds = 6.5;
            const double samplingRate = 44100.0;

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample, StreamHelper.StreamToBytes(sampleStream));

                Stopwatch sw = Stopwatch.StartNew();
                using (Stream destStream = new FileStream("Test_Synthesizer_HardCodedMultiplyWithEcho.wav", FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (BinaryWriter writer = new BinaryWriter(destStream))
                    {
                        int destSampleCount = (int)(samplingRate * seconds);

                        // Write header
                        var audioFileInfo = new AudioFileInfo
                        {
                            BytesPerValue = 2,
                            ChannelCount = 1,
                            SamplingRate = 44100,
                            SampleCount = destSampleCount
                        };
                        WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
                        writer.WriteStruct(wavHeaderStruct);

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

                double ratio = sw.Elapsed.TotalSeconds / seconds;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                Outlet outlet = x.Add(x.Number(1), x.Number(2));
                var calculator =  x.CreateCalculator(new CalculatorCache(), outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(3.0, result, 0.0001);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator_WithNullInlet()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                Outlet outlet = x.Add(null, x.Number(2));
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(2.0, result, 0.000000001);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator_Nulls()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                Outlet outlet = x.Add(x.Number(1), x.Add(x.Number(2), null));
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(3.0, result, 0.000000001);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator_NestedOperators()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                Outlet outlet = x.Add(x.Add(x.Number(1), x.Number(2)), x.Number(4));
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(7.0, result, 0.000000001);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator_TwoChannels()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                Outlet outlet1 = x.Add(x.Add(x.Number(1), x.Number(2)), x.Number(4));
                Outlet outlet2 = x.Add(x.Number(5), x.Number(6));
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet1, outlet2);
                double result1 = calculator.Calculate(0, 0);
                double result2 = calculator.Calculate(0, 1);
                Assert.AreEqual(7.0, result1, 0.000000001);
                Assert.AreEqual(11.0, result2, 0.000000001);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_OptimizedPatchCalculator_InstanceIntegrity()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                Outlet sharedOutlet = x.Number(1);
                Outlet outlet1 = x.Add(sharedOutlet, x.Number(2));
                Outlet outlet2 = x.Add(sharedOutlet, x.Number(3));
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet1, outlet2);
                double result1 = calculator.Calculate(0, 0);
                double result2 = calculator.Calculate(0, 1);
                Assert.AreEqual(3.0, result1, 0.000000001);
                Assert.AreEqual(4.0, result2, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_WhiteNoiseOperator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                PatchManager patchManager = new PatchManager(new PatchRepositories(repositories));

                Outlet outlet = x.Multiply(x.WhiteNoise(), x.Number(Int16.MaxValue));

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(new CalculatorCache(), outlet);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.FilePath = "Test_Synthesizer_WhiteNoiseOperator.wav";
                audioFileOutput.Duration = 20;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                // Execute once to fill cache(s).
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                // Also test interpreted calculator
                IPatchCalculator calculator = patchManager.CreateCalculator(new CalculatorCache(), outlet);
                double value = calculator.Calculate(0.2, 0);
                value = calculator.Calculate(0.2, 0);
                value = calculator.Calculate(0.3, 0);
                value = calculator.Calculate(0.3, 0);

                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Noise()
        {
            double duration = 2;
            int outputSamplingRate = 100;
            //int alternativeSamplingRate = outputSamplingRate / 32;
            int alternativeSamplingRate = 25;
            int amplification = 20000;

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));

                Outlet whiteNoise = x.Multiply(x.WhiteNoise(), x.Number(amplification));
                Outlet resampledWhiteNoise = x.Resample(whiteNoise, x.Number(alternativeSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_WhiteNoise_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = whiteNoise;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), whiteNoise);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_WhiteNoise_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = whiteNoise;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), whiteNoise);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_WhiteNoise_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = resampledWhiteNoise;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), resampledWhiteNoise);

                // Only test performance here and not in the other tests.

                // Execute once to fill cache(s).
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                //// Also test interpreted calculator
                //IPatchCalculator calculator = patchManager.CreateCalculator(false, outlet);
                //double value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.3, 0);
                //value = calculator.Calculate(0.3, 0);

                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));

                double duration = 2;
                int outputSamplingRate = 44100;
                int alternativeSamplingRate = outputSamplingRate / 8;

                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(stream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 10000;
                sample.BytesToSkip = 200;

                Outlet input = x.Sample(sample);
                Outlet resampled = x.Resample(input, x.Number(alternativeSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = input;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), input);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = input;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), input);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = resampled;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), resampled);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve()
        {
            // DO NOT JUST CHANGE THIS TEST. THIS GAVE VERY GOOD ERRORS TO DEBUG!
            double duration = 2;
            int outputSamplingRate = 200;
            int alternativeSamplingRate = 5;
            int amplification = 30000;

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                CurveManager curveManager = new CurveManager(new CurveRepositories(repositories));

                Curve curve = curveManager.Create
                (
                    duration,
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(-1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(-1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(-1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line),
                    new NodeInfo(-1, NodeTypeEnum.Line),
                    new NodeInfo(0, NodeTypeEnum.Line)
                );

                Outlet curveIn = x.Multiply(x.Curve(curve), x.Number(amplification));
                Outlet resampled = x.Resample(curveIn, x.Number(alternativeSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = curveIn;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), curveIn);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = curveIn;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), curveIn);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = resampled;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), resampled);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_VariableSamplingRate_Noise()
        {
            double duration = 6;
            int outputSamplingRate = 44100;
            int samplingRate1 = 11025;
            int samplingRate2 = samplingRate1 / 1024;
            int amplification = 20000;

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                CurveManager curveManager = new CurveManager(new CurveRepositories(repositories));

                Curve curve = curveManager.Create(duration, samplingRate1, samplingRate2);

                Outlet input = x.Multiply(x.WhiteNoise(), x.Number(amplification));
                Outlet outlet = x.Resample(input, x.Curve(curve));

                IPatchCalculator patchCalculator = x.CreateCalculator(new CalculatorCache(), outlet);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WithVariableSamplingRate_Noise.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_VariableSamplingRate_Sample()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                CurveManager curveManager = new CurveManager(new CurveRepositories(repositories));

                double duration = 2;

                int outputSamplingRate = 44100;
                int samplingRate1 = 10000;
                int samplingRate2 = 20;
                Curve curve = curveManager.Create(duration, samplingRate2, samplingRate1);

                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(stream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 10000;
                sample.BytesToSkip = 200;

                Outlet input = x.Sample(sample);
                Outlet outlet = x.Resample(input, x.Curve(curve));

                IPatchCalculator patchCalculator = x.CreateCalculator(new CalculatorCache(), outlet);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WithVariableSamplingRate_Sample.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_Sine()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));

                double volume = 1;
                double frequency = 1.0;
                //double phase = 0.128;
                double phase = 0;
                Outlet sine = x.Multiply(x.Number(volume), x.Sine(x.Number(frequency), x.Number(phase)));

                double newSamplingRate = 4;
                Outlet resampled = x.Resample(sine, x.Number(newSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = 2;
                audioFileOutput.SamplingRate = 44100;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_Sine_Input.wav";
                audioFileOutput.AudioFileOutputChannels[0].Outlet = sine;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), sine);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_Sine_Resampled.wav";
                audioFileOutput.AudioFileOutputChannels[0].Outlet = resampled;
                patchCalculator = x.CreateCalculator(new CalculatorCache(), resampled);
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_CustomOperator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                // Create Business Logic Objects
                DocumentManager documentManager = new DocumentManager(repositories);
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));

                // Create Reusable Patch
                PatchManager underlyingPatchManager = new PatchManager(new PatchRepositories(repositories));
                underlyingPatchManager.CreatePatch();
                Patch underlyingPatch = underlyingPatchManager.Patch;

                PatchManager x;

                // Build up Reusable Patch
                x = underlyingPatchManager;
                var patchInlet = x.PatchInlet();
                var effect = EntityFactory.CreateTimePowerEffectWithEcho(x, patchInlet);
                var patchOutlet = x.PatchOutlet(effect);

                // Build up Consuming Patch
                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(stream);
                Sample sample = sampleInfo.Sample;

                PatchManager consumingPatchManager = new PatchManager(new PatchRepositories(repositories));
                x = consumingPatchManager;
                var sampleOperator = x.Sample(sample);
                var customOperator = x.CustomOperator(underlyingPatch, sampleOperator);

                // Check out that Custom_OperatorWrapper API
                Inlet inlet = customOperator.Inlets[patchInlet.Name];
                Outlet outlet = customOperator.Outlets[patchOutlet.Name];
                Outlet operand = customOperator.Operands[patchInlet.Name];

                customOperator.Operands[patchInlet.Name] = operand;

                Patch underlyingPatch2 = customOperator.UnderlyingPatch;
                int? underlyingPatchID = customOperator.UnderlyingPatchID;

                foreach (Inlet inlet2 in customOperator.Inlets)
                {
                }

                foreach (Outlet outlet2 in customOperator.Outlets)
                {
                }

                foreach (Outlet operand2 in customOperator.Operands)
                {
                }

                // Calculator
                IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), customOperator.WrappedOperator.Outlets[0]);
                double result = calculator.Calculate(0, 0);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_SawTooth()
        {
            var x = new PatchApi();
            var saw = x.SawTooth(x.Number(0.5));

            IPatchCalculator patchCalculator = x.CreateCalculator(new CalculatorCache(), saw);

            double value1 = patchCalculator.Calculate(0.00, 0);
            double value2 = patchCalculator.Calculate(0.25, 0);
            double value3 = patchCalculator.Calculate(0.50, 0);
            double value4 = patchCalculator.Calculate(0.75, 0);
            double value5 = patchCalculator.Calculate(1.00, 0);
            double value6 = patchCalculator.Calculate(1.25, 0);
            double value7 = patchCalculator.Calculate(1.50, 0);
            double value8 = patchCalculator.Calculate(1.75, 0);
            double value9 = patchCalculator.Calculate(2.00, 0);
        }

        [TestMethod]
        public void Test_Synthesizer_SawTooth_WithPhaseShift()
        {
            // With a phase shift of 0.25 I would expect it to start counting at -0.5
            var x = new PatchApi();
            var saw = x.SawTooth(x.Number(1), x.Number(0.25));

            IPatchCalculator patchCalculator = x.CreateCalculator(new CalculatorCache(), saw);

            double value1 = patchCalculator.Calculate(0.00, 0);
            double value2 = patchCalculator.Calculate(0.25, 0);
            double value3 = patchCalculator.Calculate(0.50, 0);
            double value4 = patchCalculator.Calculate(0.75, 0);
            double value5 = patchCalculator.Calculate(1.00, 0);
            double value6 = patchCalculator.Calculate(1.25, 0);
            double value7 = patchCalculator.Calculate(1.50, 0);
            double value8 = patchCalculator.Calculate(1.75, 0);
            double value9 = patchCalculator.Calculate(2.00, 0);
        }

        [TestMethod]
        public void Test_Synthesizer_TriangleWave()
        {
            var patcher = new PatchApi();
            var outlet = patcher.TriangleWave(patcher.Number(1));

            IPatchCalculator patchCalculator = patcher.CreateCalculator(new CalculatorCache(), outlet);

            double[] values =
            {
                patchCalculator.Calculate(0.000, 0),
                patchCalculator.Calculate(0.125, 0),
                patchCalculator.Calculate(0.250, 0),
                patchCalculator.Calculate(0.375, 0),
                patchCalculator.Calculate(0.500, 0),
                patchCalculator.Calculate(0.625, 0),
                patchCalculator.Calculate(0.750, 0),
                patchCalculator.Calculate(0.875, 0),
                patchCalculator.Calculate(1.000, 0),
                patchCalculator.Calculate(1.125, 0),
                patchCalculator.Calculate(1.250, 0),
                patchCalculator.Calculate(1.375, 0),
                patchCalculator.Calculate(1.500, 0),
                patchCalculator.Calculate(1.625, 0),
                patchCalculator.Calculate(1.750, 0),
                patchCalculator.Calculate(1.875, 0),
                patchCalculator.Calculate(2.000, 0)
            };
        }

        [TestMethod]
        public void Test_Synthesizer_ValidateAllRootDocuments()
        {
            using (IContext context = PersistenceHelper.CreateDatabaseContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);
                var documentManager = new DocumentManager(repositories);

                IList<Message> messages = new List<Message>();

                IEnumerable<Document> rootDocuments = repositories.DocumentRepository.GetAll().Where(x => x.ParentDocument == null);
                foreach (Document rootDocument in rootDocuments)
                {
                    VoidResult result = documentManager.ValidateRecursive(rootDocument);
                    messages.AddRange(result.Messages);
                }

                if (messages.Count > 0)
                {
                    string formattedMessages = String.Join(" ", messages.Select(x => x.Text));
                    throw new Exception(formattedMessages);
                }
            }
        }
    }
}
