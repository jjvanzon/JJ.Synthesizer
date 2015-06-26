using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Common;
using JJ.Framework.Data;
using JJ.Framework.Validation;
using JJ.Framework.IO;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
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
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Infos;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        [TestMethod]
        public void Test_Synthesizer()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);
                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);

                var add = x.Add(x.Value(2), x.Value(3));
                var substract = x.Substract(add, x.Value(1));

                IOperatorCalculator calculator1 = patchManager.CreateCalculator(add);
                double value = calculator1.Calculate(0, 0);
                Assert.AreEqual(5, value, 0.0001);

                IOperatorCalculator calculator2 = patchManager.CreateCalculator(substract);
                value = calculator2.Calculate(0, 0);
                Assert.AreEqual(4, value, 0.0001);

                // Test recursive validator
                CultureHelper.SetThreadCulture("nl-NL");

                add.OperandA = null;
                var valueOperatorWrapper = new Value_OperatorWrapper(substract.OperandB.Operator);
                valueOperatorWrapper.Value = 0;
                substract.Operator.Inlets[0].Name = "134";

                IValidator validator2 = new OperatorValidator_Recursive(substract.Operator, repositoryWrapper.CurveRepository, repositoryWrapper.SampleRepository, alreadyDone: new HashSet<object>());
                IValidator warningValidator = new OperatorWarningValidator_Recursive(substract.Operator, repositoryWrapper.SampleRepository);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddValidator()
        {
            IValidator validator1 = new OperatorValidator_Add(new Operator
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

            IValidator validator2 = new OperatorValidator_Add(new Operator());

            bool isValid = validator1.IsValid &&
                           validator2.IsValid;
        }

        [TestMethod]
        public void Test_Synthesizer_WarningValidators()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                OperatorFactory factory = TestHelper.CreateOperatorFactory(context);

                IValidator validator1 = new OperatorWarningValidator_Add(factory.Add().Operator);
                IValidator validator2 = new OperatorWarningValidator_Value(factory.Value().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }

        [TestMethod]
        public void Test_Synthesizer_InterpretedOperatorCalculator_Adder()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory factory = TestHelper.CreateOperatorFactory(repositoryWrapper);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                Value_OperatorWrapper val1 = factory.Value(1);
                Value_OperatorWrapper val2 = factory.Value(2);
                Value_OperatorWrapper val3 = factory.Value(3);
                Adder_OperatorWrapper adder = factory.Adder(val1, val2, val3);

                IValidator validator = new OperatorValidator_Adder(adder.Operator);
                validator.Verify();

                IOperatorCalculator calculator = patchManager.CreateCalculator(true, adder);
                double value = calculator.Calculate(0, 0);

                adder.Operator.Inlets[0].Name = "qwer";
                IValidator validator2 = new OperatorValidator_Adder(adder.Operator);
                //validator2.Verify();
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ShorterCodeNotation()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);

                Substract_OperatorWrapper substract = x.Substract(x.Add(x.Value(2), x.Value(3)), x.Value(1));

                Substract_OperatorWrapper substract2 = x.Substract
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
        public void Test_Synthesizer_InterpretedOperatorCalculator_SineWithCurve_InterpretedMode()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                CurveFactory curveFactory = TestHelper.CreateCurveFactory(context);
                Curve curve = curveFactory.CreateCurve(1, 0, 1, 0.8, null, null, 0.8, 0);

                OperatorFactory f = TestHelper.CreateOperatorFactory(repositoryWrapper);
                Sine_OperatorWrapper sine = f.Sine(f.CurveIn(curve), f.Value(440));

                CultureHelper.SetThreadCulture("nl-NL");
                IValidator[] validators = 
                {
                    new CurveValidator(curve, alreadyDone: new HashSet<object>()), 
                    new OperatorValidator_Versatile(sine.Operator),
                    new OperatorWarningValidator_Versatile(sine.Operator)
                };
                validators.ForEach(x => x.Verify());

                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                var calculator = patchManager.CreateCalculator(false, sine);
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

        [TestMethod]
        public void Test_Synthesizer_TimePowerWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
                ISampleRepository sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(context);

                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                OperatorFactory operatorFactory = TestHelper.CreateOperatorFactory(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = operatorFactory.Sample(sample);
                Outlet effect = EntityFactory.CreateTimePowerEffectWithEcho(operatorFactory, sampleOperatorOutlet);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_TimePowerWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.Execute(audioFileOutput);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);
                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_MultiplyWithEcho()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
                ISampleRepository sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(context);

                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                OperatorFactory operatorFactory = TestHelper.CreateOperatorFactory(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                Outlet sampleOperatorOutlet = operatorFactory.Sample(sample);
                Outlet effect = EntityFactory.CreateMultiplyWithEcho(operatorFactory, sampleOperatorOutlet);

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.AudioFileOutputChannels[0].Outlet = effect;
                audioFileOutput.FilePath = "Test_Synthesizer_MultiplyWithEcho.wav";
                audioFileOutput.Duration = 6.5;

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.Execute(audioFileOutput);
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
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample);

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
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);

                Stream sampleStream = TestHelper.GetViolin16BitMono44100WavStream();
                Sample sample = sampleManager.CreateSample(sampleStream);
                sample.SamplingRate = 8000;
                sample.BytesToSkip = 100;

                var hardCodedCalculator = new HardCodedOperatorCalculator(sample);

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
        public void Test_Synthesizer_OptimizedOperatorCalculator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                Outlet outlet = x.Add(x.Value(1), x.Value(2));
                var calculator =  patchManager.CreateCalculator(false, outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(3.0, result, 0.0001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedOperatorCalculator_WithNullInlet()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(null, x.Value(2));
                IOperatorCalculator calculator = patchManager.CreateCalculator(outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(2.0, result, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedOperatorCalculator_Nulls()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(x.Value(1), x.Add(x.Value(2), null));
                IOperatorCalculator calculator = patchManager.CreateCalculator(outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(3.0, result, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedOperatorCalculator_NestedOperators()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Add(x.Add(x.Value(1), x.Value(2)), x.Value(4));
                IOperatorCalculator calculator = patchManager.CreateCalculator(outlet);
                double result = calculator.Calculate(0, 0);
                Assert.AreEqual(7.0, result, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedOperatorCalculator_TwoChannels()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet1 = x.Add(x.Add(x.Value(1), x.Value(2)), x.Value(4));
                Outlet outlet2 = x.Add(x.Value(5), x.Value(6));
                IOperatorCalculator calculator = patchManager.CreateCalculator(outlet1, outlet2);
                double result1 = calculator.Calculate(0, 0);
                double result2 = calculator.Calculate(0, 1);
                Assert.AreEqual(7.0, result1, 0.000000001);
                Assert.AreEqual(11.0, result2, 0.000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_OptimizedOperatorCalculator_InstanceIntegrity()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet sharedOutlet = x.Value(1);
                Outlet outlet1 = x.Add(sharedOutlet, x.Value(2));
                Outlet outlet2 = x.Add(sharedOutlet, x.Value(3));
                IOperatorCalculator calculator = patchManager.CreateCalculator(outlet1, outlet2);
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
                var repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(repositoryWrapper);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                Outlet outlet = x.Multiply(x.WhiteNoise(), x.Value(Int16.MaxValue));

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.FilePath = "Test_Synthesizer_WhiteNoiseOperator.wav";
                audioFileOutput.Duration = 20;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                // Execute once to fill cache(s).
                audioFileOutputManager.Execute(audioFileOutput);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.Execute(audioFileOutput);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                // Also test interpreted calculator
                IOperatorCalculator calculator = patchManager.CreateCalculator(false, outlet);
                double value = calculator.Calculate(0.2, 0);
                value = calculator.Calculate(0.2, 0);
                value = calculator.Calculate(0.3, 0);
                value = calculator.Calculate(0.3, 0);

                Assert.Inconclusive(message);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_ResampleOperator()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(repositoryWrapper);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                double duration = 2;
                int samplingRate = 44100;
                int alternativeSamplingRate = samplingRate / 32;

                Outlet whiteNoise = x.Multiply(x.WhiteNoise(), x.Value(Int16.MaxValue));
                Outlet resampledWhiteNoise = x.Resample(whiteNoise, x.Value(alternativeSamplingRate));

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WhiteNoise.wav";
                audioFileOutput.SamplingRate = samplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = whiteNoise;
                audioFileOutputManager.Execute(audioFileOutput);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WhiteNoise_WithLowerSamplingRate.wav";
                audioFileOutput.SamplingRate = alternativeSamplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = whiteNoise;
                audioFileOutputManager.Execute(audioFileOutput);

                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WhiteNoise_WithResampleOperator.wav";
                audioFileOutput.SamplingRate = samplingRate;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = resampledWhiteNoise;

                // Execute once to fill cache(s).
                audioFileOutputManager.Execute(audioFileOutput);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.Execute(audioFileOutput);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                //// Also test interpreted calculator
                //IOperatorCalculator calculator = patchManager.CreateCalculator(false, outlet);
                //double value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.3, 0);
                //value = calculator.Calculate(0.3, 0);

                Assert.Inconclusive(message);
            }
        }


        [TestMethod]
        public void Test_Synthesizer_ResampleOperator_WithVariableSamplingRate()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(context);

                OperatorFactory x = TestHelper.CreateOperatorFactory(repositoryWrapper);
                SampleManager sampleManager = TestHelper.CreateSampleManager(repositoryWrapper);
                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(repositoryWrapper);
                PatchManager patchManager = TestHelper.CreatePatchManager(repositoryWrapper);
                CurveFactory curveFactory = new CurveFactory(repositoryWrapper.CurveRepository, repositoryWrapper.NodeRepository, repositoryWrapper.NodeTypeRepository);

                double duration = 2;

                int samplingRate = 11025;
                int alternativeSamplingRate = samplingRate / 64;
                Curve curve = curveFactory.CreateCurve(duration, samplingRate, alternativeSamplingRate);

                //Curve curve = curveFactory.CreateCurve
                //(duration,
                //    44100, 44100, 44100, 44100,
                //    22050, 22050, 22050, 22050,
                //    11025, 11025, 11025, 11025,
                //    5512.5, 5512.5, 5512.5, 5512.5,
                //    2756.25, 2756.25, 2756.25, 2756.25,
                //    1378.125, 1378.125, 1378.125, 1378.125,
                //    689.0625, 689.0625, 689.0625, 689.0625,
                //    344.53125, 344.53125, 344.53125, 344.53125/*,
                //    172.265625, 172.265625, 172.265625, 172.265625,
                //    86.1328125, 86.1328125, 86.1328125, 86.1328125 */
                //);

                //Curve curve = curveFactory.CreateCurve(duration, 44100, 689.0625);
                //Curve curve = curveFactory.CreateCurve(duration, 5512.5, 5512.5);

                //Stream stream = TestHelper.GetViolin16BitMono44100WavStream();
                //Sample sample = sampleManager.CreateSample(stream);
                //sample.SamplingRate = 20000;
                //sample.BytesToSkip = 100;

                Outlet input = x.Multiply(x.WhiteNoise(), x.Value(32000));
                //Outlet input = x.Sample(sample);
                Outlet outlet = x.Resample(input, x.CurveIn(curve));

                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = "Test_Synthesizer_ResampleOperator_WithVariableSamplingRate.wav";
                audioFileOutput.SamplingRate = 44100;
                audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                // Execute once to fill cache(s).
                audioFileOutputManager.Execute(audioFileOutput);

                Stopwatch sw = Stopwatch.StartNew();
                audioFileOutputManager.Execute(audioFileOutput);
                sw.Stop();

                double ratio = sw.Elapsed.TotalSeconds / audioFileOutput.Duration;
                string message = String.Format("Ratio: {0:0.00}%, {1}ms.", ratio * 100, sw.ElapsedMilliseconds);

                //// Also test interpreted calculator
                //IOperatorCalculator calculator = patchManager.CreateCalculator(false, outlet);
                //double value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.2, 0);
                //value = calculator.Calculate(0.3, 0);
                //value = calculator.Calculate(0.3, 0);

                Assert.Inconclusive(message);
            }
        }
    }
}
