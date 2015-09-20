using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Data;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;

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

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_Sample()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                Stream stream = TestHelper.GetViolin16BitMonoRawStream();
                SampleInfo sampleInfo = sampleManager.CreateSample(stream, AudioFileFormatEnum.Raw);
                Sample sample = sampleInfo.Sample;

                IValidator sampleValidator = sampleManager.Validate(sample);
                sampleValidator.Verify();

                double timeMultiplier = 1;
                double duration = sample.GetDuration(stream.Length);

                PatchManager x = TestHelper.CreatePatchManager(context);
                Outlet outlet = x.SlowDown(x.Sample(sample), x.Number(timeMultiplier));

                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = OUTPUT_FILE_NAME;
                audioFileOutput.AudioFileOutputChannels[0].LinkTo(outlet);

                IValidator audioFileOutputValidator = audioFileOutputManager.Validate(audioFileOutput);
                audioFileOutputValidator.Verify();

                audioFileOutputManager.Execute(audioFileOutput);
            }
        }

        // Test engine crashes
        [TestMethod]
        public void Test_Synthesizer_InterpretedOperatorCalculator_Sample_WithWavHeader()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                RepositoryWrapper repositoryWrapper = PersistenceHelper.CreateRepositories(context);

                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();

                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                SampleInfo sampleInfo = sampleManager.CreateSample(stream, AudioFileFormatEnum.Wav);
                Sample sample = sampleInfo.Sample;

                PatchManager x = TestHelper.CreatePatchManager(context);
                Outlet outlet = x.Sample(sample);

                var patchManager = TestHelper.CreatePatchManager(repositoryWrapper);

                // Trigger SampleCalculation
                IPatchCalculator calculator = patchManager.CreateCalculator(outlet);
                double value = calculator.Calculate(0, 0);
            }
        }

        [TestMethod]
        public void Test_Sample_WavHeaderDetection()
        {
            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);

                Stream wavStream = TestHelper.GetViolin16BitMono44100WavStream();
                Stream rawStream = TestHelper.GetViolin16BitMonoRawStream();

                SampleInfo wavSampleInfo = sampleManager.CreateSample(wavStream);
                SampleInfo rawSampleInfo = sampleManager.CreateSample(rawStream);
            }
        }
    }
}
