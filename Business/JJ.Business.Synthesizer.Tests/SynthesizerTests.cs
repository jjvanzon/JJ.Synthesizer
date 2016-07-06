using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Common;
using JJ.Framework.Data;
using JJ.Framework.IO;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using System.Linq;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        private const int DEFAULT_SAMPLING_RATE = 44100;
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const int DEFAULT_CHANNEL_INDEX = 0;

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

                IPatchCalculator calculator1 = x.CreateCalculator(add, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                double value = calculator1.Calculate(0, 0);
                Assert.AreEqual(5, value, 0.0001);

                IPatchCalculator calculator2 = x.CreateCalculator(subtract, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                value = calculator2.Calculate(0, 0);
                Assert.AreEqual(4, value, 0.0001);

                // Test recursive validator
                CultureHelper.SetThreadCultureName("nl-NL");

                add.Operands[0] = null;
                var valueOperatorWrapper = new Number_OperatorWrapper(subtract.B.Operator);
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
                Add_OperatorWrapper add = patchManager.Add(val1, val2, val3);

                //IValidator validator = new OperatorValidator_Adder(adder.Operator);
                //validator.Verify();

                IPatchCalculator calculator = patchManager.CreateCalculator(add, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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

                var outlet = x.MultiplyWithOrigin(x.Curve(curve), x.Sine(x.Number(440)));

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

                var calculator = patchManager.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                var times = new double[]
                {
                    0.00,
                    0.05,
                    0.10,
                    0.15,
                    0.20,
                    0.25,
                    0.30,
                    0.35,
                    0.40,
                    0.45,
                    0.50,
                    0.55,
                    0.60,
                    0.65,
                    0.70,
                    0.75,
                    0.80,
                    0.85,
                    0.90,
                    0.95,
                    1.00
                };

                var values = new double[times.Length];

                foreach (double time in times)
                {
                    values[0] = calculator.Calculate(time, 0);
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
                patchManager.CreatePatch();

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = patchManager.Sample(sample);
                Outlet effect = EntityFactory.CreateTimePowerEffectWithEcho(patchManager, sampleOperatorOutlet);

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(effect, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.LinkTo(effect);
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
                patchManager.CreatePatch();

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(sampleStream);
                Sample sample = sampleInfo.Sample;
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = patchManager.Sample(sample);
                Outlet effect = EntityFactory.CreateMultiplyWithEcho(patchManager, sampleOperatorOutlet);

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(effect, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.LinkTo(effect);
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
        public void Test_Synthesizer_TimePowerWithEcho_HardCoded()
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
                        int destFrameCount = (int)(samplingRate * seconds);

                        // Write header
                        var audioFileInfo = new AudioFileInfo
                        {
                            BytesPerValue = 2,
                            ChannelCount = 1,
                            SamplingRate = 44100,
                            FrameCount = destFrameCount
                        };
                        WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
                        writer.WriteStruct(wavHeaderStruct);

                        double t = 0;
                        double dt = 1.0 / samplingRate;

                        for (int i = 0; i < destFrameCount; i++)
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
        public void Test_Synthesizer_MultiplyWithEcho_HardCoded()
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
                        int destFrameCount = (int)(samplingRate * seconds);

                        // Write header
                        var audioFileInfo = new AudioFileInfo
                        {
                            BytesPerValue = 2,
                            ChannelCount = 1,
                            SamplingRate = 44100,
                            FrameCount = destFrameCount
                        };
                        WavHeaderStruct wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);
                        writer.WriteStruct(wavHeaderStruct);

                        double t = 0;
                        double dt = 1.0 / samplingRate;

                        for (int i = 0; i < destFrameCount; i++)
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
                var calculator =  x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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
                IPatchCalculator calculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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
                IPatchCalculator calculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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
                IPatchCalculator calculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(7.0, result, 0.000000001);
            }
        }

        //// Test engine crashes
        //[TestMethod]
        //public void Test_Synthesizer_OptimizedPatchCalculator_TwoChannels()
        //{
        //    using (IContext context = PersistenceHelper.CreateMemoryContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
        //        PatchManager x = new PatchManager(new PatchRepositories(repositories));

        //        Outlet outlet1 = x.Add(x.Add(x.Number(1), x.Number(2)), x.Number(4));
        //        Outlet outlet2 = x.Add(x.Number(5), x.Number(6));
        //        IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet1, outlet2);

        //        dimensionStack.Push(DimensionEnum.Channel, 0);
        //        double result1 = calculator.Calculate(dimensionStack);
        //        dimensionStack.Pop(DimensionEnum.Channel);

        //        dimensionStack.Push(DimensionEnum.Channel, 1);
        //        double result2 = calculator.Calculate(dimensionStack);
        //        dimensionStack.Pop(DimensionEnum.Channel);

        //        Assert.AreEqual(7.0, result1, 0.000000001);
        //        Assert.AreEqual(11.0, result2, 0.000000001);
        //    }
        //}

        //// Test engine crashes
        //[TestMethod]
        //public void Test_Synthesizer_OptimizedPatchCalculator_InstanceIntegrity()
        //{
        //    using (IContext context = PersistenceHelper.CreateMemoryContext())
        //    {
        //        RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
        //        PatchManager x = new PatchManager(new PatchRepositories(repositories));
        //        Outlet sharedOutlet = x.Number(1);
        //        Outlet outlet1 = x.Add(sharedOutlet, x.Number(2));
        //        Outlet outlet2 = x.Add(sharedOutlet, x.Number(3));
        //        IPatchCalculator calculator = x.CreateCalculator(new CalculatorCache(), outlet1, outlet2);

        //        dimensionStack.Push(DimensionEnum.Channel, 0);
        //        double result1 = calculator.Calculate(dimensionStack);
        //        dimensionStack.Pop(DimensionEnum.Channel);

        //        dimensionStack.Push(DimensionEnum.Channel, 1);
        //        double result2 = calculator.Calculate(dimensionStack);
        //        dimensionStack.Pop(DimensionEnum.Channel);

        //        Assert.AreEqual(3.0, result1, 0.000000001);
        //        Assert.AreEqual(4.0, result2, 0.000000001);
        //    }
        //}

        [TestMethod]
        public void Test_Synthesizer_NoiseOperator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                PatchManager patchManager = new PatchManager(new PatchRepositories(repositories));

                Outlet outlet = x.MultiplyWithOrigin(x.Noise(), x.Number(Int16.MaxValue));

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.FilePath = "Test_Synthesizer_NoiseOperator.wav";
                audioFileOutput.Duration = 20;
                audioFileOutput.LinkTo(outlet);

                // Execute once to fill cache(s).
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                // Also test interpreted calculator
                IPatchCalculator calculator = patchManager.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                double value;

                value = calculator.Calculate(0.2, 0);
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

                Outlet noise = x.MultiplyWithOrigin(x.Noise(), x.Number(amplification));
                Outlet resampledNoise = x.Resample(noise, x.Number(alternativeSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Noise_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(noise);
                patchCalculator = x.CreateCalculator(noise, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Noise_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.LinkTo(noise);
                patchCalculator = x.CreateCalculator(noise, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Noise_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(resampledNoise);
                patchCalculator = x.CreateCalculator(resampledNoise, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

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

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(input);
                patchCalculator = x.CreateCalculator(input, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.LinkTo(input);
                patchCalculator = x.CreateCalculator(input, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Sample_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(resampled);
                patchCalculator = x.CreateCalculator(resampled, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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

                Outlet curveIn = x.MultiplyWithOrigin(x.Curve(curve), x.Number(amplification));
                Outlet resampled = x.Resample(curveIn, x.Number(alternativeSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_Input.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(curveIn);
                patchCalculator = x.CreateCalculator(curveIn, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.LinkTo(curveIn);
                patchCalculator = x.CreateCalculator(curveIn, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_ConstantSamplingRate_Curve_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(resampled);
                patchCalculator = x.CreateCalculator(resampled, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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

                Outlet input = x.MultiplyWithOrigin(x.Noise(), x.Number(amplification));
                Outlet outlet = x.Resample(input, x.Curve(curve));

                IPatchCalculator patchCalculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WithVariableSamplingRate_Noise.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(outlet);

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

                IPatchCalculator patchCalculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WithVariableSamplingRate_Sample.wav";
                audioFileOutput.SamplingRate = outputSamplingRate;
                audioFileOutput.LinkTo(outlet);

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
                Outlet sine = x.MultiplyWithOrigin(x.Number(volume), x.Sine(x.Number(frequency), x.Number(phase)));

                double newSamplingRate = 4;
                Outlet resampled = x.Resample(sine, x.Number(newSamplingRate));

                IPatchCalculator patchCalculator;

                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = 2;
                audioFileOutput.SamplingRate = 44100;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_Sine_Input.wav";
                audioFileOutput.LinkTo(sine);
                patchCalculator = x.CreateCalculator(sine, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_Sine_Resampled.wav";
                audioFileOutput.LinkTo(resampled);
                patchCalculator = x.CreateCalculator(resampled, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
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
                IPatchCalculator calculator = x.CreateCalculator(
                    customOperator.WrappedOperator.Outlets[0],
                    DEFAULT_SAMPLING_RATE,
                    DEFAULT_CHANNEL_COUNT, 
                    DEFAULT_CHANNEL_INDEX, 
                    new CalculatorCache());

                double result = calculator.Calculate(0, 0);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_SawUp()
        {
            var x = new PatchApi();
            var saw = x.SawUp(x.Number(0.5));

            IPatchCalculator patchCalculator = x.CreateCalculator(
                saw, 
                DEFAULT_SAMPLING_RATE,
                DEFAULT_CHANNEL_COUNT, 
                DEFAULT_CHANNEL_INDEX, 
                new CalculatorCache());

            var times = new double[]
            {
                0.00,
                0.25,
                0.50,
                0.75,
                1.00,
                1.25,
                1.50,
                1.75,
                2.00
            };

            var values = new List<double>(times.Length);

            foreach (double time in times)
            {
                double value = patchCalculator.Calculate(0,0);
                values.Add(value);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_SawUp_WithPhaseShift()
        {
            // With a phase shift of 0.25 I would expect it to start counting at -0.5
            var x = new PatchApi();
            var saw = x.SawUp(x.Number(1), x.Number(0.25));

            IPatchCalculator patchCalculator = x.CreateCalculator(
                saw, 
                DEFAULT_SAMPLING_RATE,
                DEFAULT_CHANNEL_COUNT, 
                DEFAULT_CHANNEL_INDEX, 
                new CalculatorCache());

            var times = new double[]
            {
                0.00,
                0.25,
                0.50,
                0.75,
                1.00,
                1.25,
                1.50,
                1.75,
                2.00
            };

            var values = new List<double>(times.Length);

            foreach (double time in times)
            {
                double value = patchCalculator.Calculate(time, 0);
                values.Add(value);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_Triangle()
        {
            var patcher = new PatchApi();
            var outlet = patcher.Triangle(patcher.Number(1));

            IPatchCalculator patchCalculator = patcher.CreateCalculator(
                outlet, 
                DEFAULT_SAMPLING_RATE,
                DEFAULT_CHANNEL_COUNT, 
                DEFAULT_CHANNEL_INDEX, 
                new CalculatorCache());

            double[] times =
            {
                0.000,
                0.125,
                0.250,
                0.375,
                0.500,
                0.625,
                0.750,
                0.875,
                1.000,
                1.125,
                1.250,
                1.375,
                1.500,
                1.625,
                1.750,
                1.875,
                2.000
            };

            var values = new List<double>(times.Length);
            foreach (double time in times)
            {
                double value = patchCalculator.Calculate(time, 0);
                values.Add(value);
            }
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
                    VoidResult result = documentManager.Save(rootDocument);
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
