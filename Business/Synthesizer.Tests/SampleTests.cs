using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Data;
using JJ.Data.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Canonical;
using System;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SampleTests
    {
        static SampleTests()
        {
            TestHelper.SetConfigurationSections();
        }

        private const string OUTPUT_FILE_NAME = "AudioFileOutput.wav";
        private const int DEFAULT_SAMPLING_RATE = 44100;
        private const int DEFAULT_CHANNEL_COUNT = 1;
        private const int DEFAULT_CHANNEL_INDEX = 0;

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_Sample()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);
                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));
                Stream stream = TestHelper.GetViolin16BitMonoRawStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(stream, AudioFileFormatEnum.Raw);
                Sample sample = sampleInfo.Sample;

                {
                    VoidResult validationResult = sampleManager.Save(sample);
                    if (!validationResult.Successful)
                    {
                        throw new Exception(string.Join(Environment.NewLine, validationResult.Messages));
                    }
                }

                const double timeMultiplier = 1;
                double duration = sample.GetDuration(stream.Length);

                PatchManager x = new PatchManager(new PatchRepositories(repositories));
                x.CreatePatch();
                Outlet outlet = x.Stretch(x.Sample(sample), x.Number(timeMultiplier));
                IPatchCalculator patchCalculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());

                AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
                AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = OUTPUT_FILE_NAME;
                audioFileOutput.LinkTo(outlet);

                {
                    VoidResult validationResult = audioFileOutputManager.Save(audioFileOutput);
                    if (!validationResult.Successful)
                    {
                        throw new Exception(string.Join(Environment.NewLine, validationResult.Messages));
                    }
                }

                audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator );
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_InterpretedOperatorCalculator_Sample_WithWavHeader()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositories = PersistenceHelper.CreateRepositories(context);

                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();

                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));
                SampleInfo sampleInfo = sampleManager.CreateSample(stream, AudioFileFormatEnum.Wav);
                Sample sample = sampleInfo.Sample;

                PatchManager x = new PatchManager(new PatchRepositories(repositories));

                x.CreatePatch();

                Outlet outlet = x.Sample(sample);

                // Trigger SampleCalculation
                IPatchCalculator calculator = x.CreateCalculator(outlet, DEFAULT_SAMPLING_RATE, DEFAULT_CHANNEL_COUNT, DEFAULT_CHANNEL_INDEX, new CalculatorCache());
                double value = TestHelper.CalculateOneValue(calculator);
            }
        }


        [TestMethod]
        public void Test_Sample_WavHeaderDetection()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                var repositories = PersistenceHelper.CreateRepositories(context);

                SampleManager sampleManager = new SampleManager(new SampleRepositories(repositories));

                Stream wavStream = TestHelper.GetViolin16BitMono44100WavStream();
                Stream rawStream = TestHelper.GetViolin16BitMonoRawStream();

                SampleInfo wavSampleInfo = sampleManager.CreateSample(wavStream);
                SampleInfo rawSampleInfo = sampleManager.CreateSample(rawStream);
            }
        }
    }
}
