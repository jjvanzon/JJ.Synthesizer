﻿// ReSharper disable UnusedVariable
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.IO;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Tests
{
    [TestCategory("Classic")]
    [TestClass]
    public class SampleTests
    {
        private const string OUTPUT_FILE_NAME = "AudioFileOutput.wav";

        [TestMethod]
        public void Test_Synthesizer_Sample()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                Stream stream = TestHelper.GetViolin16BitMonoRawStream();
                Sample sample = sampleManager.CreateSample(stream, AudioFileFormatEnum.Raw);

                IValidator sampleValidator = sampleManager.ValidateSample(sample);
                sampleValidator.Verify();

                double timeMultiplier = 1;
                double duration = sample.GetDuration();

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.TimeMultiply(x.Sample(sample), x.Value(timeMultiplier));

                AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(context);
                AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
                audioFileOutput.Duration = duration;
                audioFileOutput.FilePath = OUTPUT_FILE_NAME;
                audioFileOutput.AudioFileOutputChannels[0].LinkTo(outlet);

                IValidator audioFileOutputValidator = audioFileOutputManager.ValidateAudioFileOutput(audioFileOutput);
                audioFileOutputValidator.Verify();

                IAudioFileOutputCalculator calculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
                calculator.Execute();
            }
        }

        [TestMethod]
        public void Test_Synthesizer_Sample_WithWavHeader()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context);

                Stream stream = TestHelper.GetViolin16BitMono44100WavStream();

                SampleManager sampleManager = TestHelper.CreateSampleManager(context);
                Sample sample = sampleManager.CreateSample(stream, AudioFileFormatEnum.Wav);

                OperatorFactory x = TestHelper.CreateOperatorFactory(context);
                Outlet outlet = x.Sample(sample);

                // Trigger SampleCalculation
                var calculator = new OperatorCalculator(0);
                double value = calculator.CalculateValue(outlet, 0);
            }
        }

        [TestMethod]
        public void Test_Sample_WavHeaderDetection()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                SampleManager sampleManager = TestHelper.CreateSampleManager(context);

                Stream wavStream = TestHelper.GetViolin16BitMono44100WavStream();
                Stream rawStream = TestHelper.GetViolin16BitMonoRawStream();

                Sample wavSample = sampleManager.CreateSample(wavStream);
                Sample rawSample = sampleManager.CreateSample(rawStream);
            }
        }
    }
}
