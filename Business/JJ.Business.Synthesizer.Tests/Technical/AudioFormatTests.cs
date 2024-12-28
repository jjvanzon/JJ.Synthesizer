using System;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Environment;
using static System.IO.Path;
using static System.Math;
using static System.MidpointRounding;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.JJFrameworkIOWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ConvertToConstant.Local
// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioFormatTests : SynthWishes
    {
        private const int    ALIGNED_SAMPLING_RATE     = 8192;
        private const double ALIGNED_FREQUENCY         = 64;
        private const int    NON_ALIGNED_SAMPLING_RATE = 8191;
        private const double NON_ALIGNED_FREQUENCY     = 61;

        private const double VOLUME    = 0.50;
        private const double PANNING   = 0.25;
        private const double DURATION  = 0.25;
        private const double DURATION2 = DURATION * 1.01;
        private const int    DECIMALS  = 4;

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono__8Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono__8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono__8Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono__8Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_Aligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_16Bit_Linear_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_Aligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Mono_32Bit_Linear_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo__8Bit_Blocky_Aligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo__8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo__8Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo__8Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono__8Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono__8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono__8Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono__8Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Block_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Block_NonAligned()
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Mono_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo__8Bit_Blocky_Aligned()
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 8, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo__8Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 8, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo__8Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 8, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo__8Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 8, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Line, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Block, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Block, aligned: false);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_Aligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Line, aligned: true);

        [TestMethod]
        public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_NonAligned() 
            => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Line, aligned: false);

        void TestAudioFormat(
            AudioFileFormatEnum audioFormat,
            int channels,
            int bits,
            InterpolationTypeEnum interpolation,
            bool aligned,
            [CallerMemberName] string callerMemberName = null)
        {
            LogLine("");
            LogLine("Options");
            LogLine("-------");
            LogLine("");
            
            //WithDiskCache();
            
            int    samplingRate = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            double frequency    = aligned ? ALIGNED_FREQUENCY : NON_ALIGNED_FREQUENCY;

            WithPadding(0);
            WithChannels(channels);
            WithBits(bits);
            WithInterpolation(interpolation);
            WithAudioFormat(audioFormat);
            WithSamplingRate(samplingRate);
            
            LogLine(this.ConfigLog(title: ""));
            
            LogLine("");
            LogLine("Materialize Signal with \"Record\" (Old Method)");
            LogLine("---------------------------------------------");
            
            WithAudioLength(DURATION);
            Buff signalBuffOld = this.Record(() => Signal(_[frequency], callerMemberName), callerMemberName);
            IsNotNull(() => signalBuffOld);
            AudioFileOutput signalAudioFileOutputOld = signalBuffOld.UnderlyingAudioFileOutput;
            
            signalBuffOld.Save(callerMemberName + "_" + nameof(signalBuffOld));

            LogLine("");
            LogLine("Materialize Signal with \"Run/Intercept\" (New Method)");
            LogLine("----------------------------------------------------");
            LogLine("");

            WithAudioLength(DURATION);
            Tape signalTapeNew = null;
            Run(() => Signal(_[frequency], callerMemberName).AfterRecord(x => signalTapeNew = x, callerMemberName));
            IsNotNull(() => signalTapeNew);
            Buff signalBuffNew = signalTapeNew.Buff;
            AudioFileOutput signalAudioFileOutputNew = signalBuffNew.UnderlyingAudioFileOutput;
            
            signalTapeNew.Save(callerMemberName + "_" + nameof(signalTapeNew));

            LogLine("");
            LogLine("Reload Sample in a FlowNode");
            LogLine("---------------------------");
            LogLine("");
            
            FlowNode ReloadSampleOld()
            {
                string reloadedName = $"{callerMemberName}_Reloaded";
                FlowNode node = Sample(signalBuffOld, name: reloadedName);
                return node;
            }
            
            FlowNode ReloadSampleNew()
            {
                string reloadedName = $"{callerMemberName}_Reloaded";
                //FlowNode node = Sample(signalBuffNew, name: reloadedName);
                FlowNode node = Sample(signalTapeNew).SetName(reloadedName);
                return node;
            }
            
            LogLine("Done.");
                
            LogLine("");
            LogLine("Reload Sample with \"Record\" (Old Method)");
            LogLine("----------------------------------------");
            LogLine("");

            WithAudioLength(DURATION2);
            Buff reloadedSampleBuffOld = this.Record(ReloadSampleOld);
            IsNotNull(() => reloadedSampleBuffOld);
            
            reloadedSampleBuffOld.Save(callerMemberName + "_" + nameof(reloadedSampleBuffOld));

            LogLine();
            LogLine("Reload Sample with \"Run/Intercept\" (New Method)");
            LogLine("-----------------------------------------------");
            LogLine("");

            WithAudioLength(DURATION2);
            Tape reloadedSampleTapeNew = null;
            Run(() => ReloadSampleNew().AfterRecord(x => reloadedSampleTapeNew = x));
            IsNotNull(() => reloadedSampleTapeNew);
            
            reloadedSampleTapeNew.Save(callerMemberName + "_" + nameof(reloadedSampleTapeNew));
            
            LogLine("");
            LogLine("Assert AudioFileOutput Properties");
            LogLine("---------------------------------");
            LogLine("");

            string filePath1Expectation = GetFullPath(PrettifyName(callerMemberName) + audioFormat.FileExtension());
            
            AssertAudioFileOutputProperties(
                signalAudioFileOutputOld,
                audioFormat, channels, bits, samplingRate,
                filePath1Expectation, DURATION, callerMemberName);
            
            AssertAudioFileOutputProperties(
                signalAudioFileOutputNew,
                audioFormat, channels, bits, samplingRate,
                filePath1Expectation, DURATION, callerMemberName);

            string filePath2Expectation = GetFullPath(PrettifyName($"{callerMemberName}_Reloaded") + audioFormat.FileExtension());
            
            AssertAudioFileOutputProperties(
                reloadedSampleBuffOld.UnderlyingAudioFileOutput,
                audioFormat, channels, bits, samplingRate,
                filePath2Expectation, DURATION2, callerMemberName);
            
            AssertAudioFileOutputProperties(
                reloadedSampleTapeNew.UnderlyingAudioFileOutput,
                audioFormat, channels, bits, samplingRate,
                filePath2Expectation, DURATION2, callerMemberName);

            LogLine("Done.");
            
            LogLine("");
            LogLine("Assert Sample Properties");
            LogLine("------------------------");
            LogLine("");

            string filePathExpectation = signalAudioFileOutputNew.FilePath;

            FlowNode reloadedSampleOld = ReloadSampleOld();

            AssertSampleProperties(
                reloadedSampleOld,
                audioFormat, channels, bits, interpolation, samplingRate,
                expectedDuration: DURATION, filePathExpectation, callerMemberName);

            FlowNode reloadedSampleNew = ReloadSampleNew();

            AssertSampleProperties(
                reloadedSampleNew,
                audioFormat, channels, bits, interpolation, samplingRate,
                expectedDuration: DURATION, filePathExpectation, callerMemberName);

            LogLine("Done.");

            // Mono

            if (channels == 1)
            {
                LogLine("");
                LogLine("Get Values from Reloaded Sample");
                LogLine("-------------------------------");
                LogLine("");

                double[] actualValues =
                {
                    Calculate(reloadedSampleNew, time: 0.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 1.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 2.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 3.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 4.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 5.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 6.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 7.0 / 8.0 / frequency),
                    Calculate(reloadedSampleNew, time: 8.0 / 8.0 / frequency)
                };
                                                
                double[] expectedValues =
                {
                    VOLUME *       0.0,
                    VOLUME *  Sqrt(.5),
                    VOLUME *       1.0,
                    VOLUME *  Sqrt(.5),
                    VOLUME *       0.0,
                    VOLUME * -Sqrt(.5),
                    VOLUME *      -1.0,
                    VOLUME * -Sqrt(.5),
                    VOLUME *       0.0
                };
                expectedValues = expectedValues.Select(RoundValue).ToArray();

                LogLine("Done.");
                
                //LogLine("");
                //LogLine("Save Values");
                //LogLine("-----------");
                //LogLine("");
                //
                //Run(() => reloadedSample.AsWav().Save(callerMemberName + "_Values"));
                //WithAudioFormat(audioFormat);

                LogLine("");
                LogLine("Assert Values");
                LogLine("-------------");
                LogLine("");

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequired = expectedValues.Zip(actualValues, (x,y) => Abs(x - y)).Max();
                LogLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                LogLine($"{nameof(valueToleranceRequired)} = {valueToleranceRequired}");
                LogLine();
                LogLine($"{nameof(expectedValues)} = {FormatValues(expectedValues)}");
                LogLine($"{nameof(actualValues)}   = {FormatValues(actualValues  )}");
                LogLine();
                
                AreEqual(expectedValues[0], actualValues[0], valueTolerance);
                AreEqual(expectedValues[1], actualValues[1], valueTolerance);
                AreEqual(expectedValues[2], actualValues[2], valueTolerance);
                AreEqual(expectedValues[3], actualValues[3], valueTolerance);
                AreEqual(expectedValues[4], actualValues[4], valueTolerance);
                AreEqual(expectedValues[5], actualValues[5], valueTolerance);
                AreEqual(expectedValues[6], actualValues[6], valueTolerance);
                AreEqual(expectedValues[7], actualValues[7], valueTolerance);
                AreEqual(expectedValues[8], actualValues[8], valueTolerance);
            }

            // Stereo

            if (channels == 2)
            {
                LogLine("");
                LogLine("Get Values from Reloaded Sample");
                LogLine("-------------------------------");
                LogLine("");

                double[] actualL =
                {
                    reloadedSampleOld.Calculate(time: 0.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 1.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 2.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 3.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 4.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 5.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 6.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 7.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleOld.Calculate(time: 8.0 / 8.0 / frequency, channel: 0)
                };
                                
                double[] expectedL =
                {
                    VOLUME * 0.75 *       0.0,
                    VOLUME * 0.75 *  Sqrt(.5),
                    VOLUME * 0.75 *       1.0,
                    VOLUME * 0.75 *  Sqrt(.5),
                    VOLUME * 0.75 *       0.0,
                    VOLUME * 0.75 * -Sqrt(.5),
                    VOLUME * 0.75 *      -1.0,
                    VOLUME * 0.75 * -Sqrt(.5),
                    VOLUME * 0.75 *       0.0
                };
                expectedL = expectedL.Select(RoundValue).ToArray();

                double[] actualR =
                {
                    reloadedSampleOld.Calculate(time: 0.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 1.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 2.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 3.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 4.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 5.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 6.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 7.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleOld.Calculate(time: 8.0 / 8.0 / frequency, channel: 1)
                };
                
                double[] expectedR =
                {
                    VOLUME * 0.25 *       0.0,
                    VOLUME * 0.25 *  Sqrt(.5),
                    VOLUME * 0.25 *       1.0,
                    VOLUME * 0.25 *  Sqrt(.5),
                    VOLUME * 0.25 *       0.0,
                    VOLUME * 0.25 * -Sqrt(.5),
                    VOLUME * 0.25 *      -1.0,
                    VOLUME * 0.25 * -Sqrt(.5),
                    VOLUME * 0.25 *       0.0
                };
                expectedR = expectedR.Select(RoundValue).ToArray();

                LogLine("Done.");

                //LogLine("");
                //LogLine("Save Values");
                //LogLine("-----------");
                //LogLine("");

                //// TODO: Can't save channels separately this way.
                //// Stereo sample switches with channel (= Center = 0 = Left)
                //WithMono().WithCenter().AsWav();
                //Run(() => reloadedSample.Save(callerMemberName + "_ValuesLeft"));
                //Run(() => reloadedSample.Save(callerMemberName + "_ValuesRight"));
                //WithChannels(channels).WithAudioFormat(audioFormat);
                
                LogLine("");
                LogLine("Assert Values");
                LogLine("-------------");
                LogLine("");

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequired = expectedL.Concat(expectedR).Zip(actualL.Concat(actualR), (x,y) => Abs(x - y)).Max();

                LogLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                LogLine($"{nameof(valueToleranceRequired)} = {valueToleranceRequired}");
                LogLine();
                LogLine($"{nameof(expectedL)} = {FormatValues(expectedL)}");
                LogLine($"  {nameof(actualL)} = {FormatValues(actualL  )}");
                LogLine();
                LogLine($"{nameof(expectedR)} = {FormatValues(expectedR)}");
                LogLine($"  {nameof(actualR)} = {FormatValues(actualR  )}");
                LogLine();
                
                // Left

                AreEqual(expectedL[0], actualL[0], valueTolerance);
                AreEqual(expectedL[1], actualL[1], valueTolerance);
                AreEqual(expectedL[2], actualL[2], valueTolerance);
                AreEqual(expectedL[3], actualL[3], valueTolerance);
                AreEqual(expectedL[4], actualL[4], valueTolerance);
                AreEqual(expectedL[5], actualL[5], valueTolerance);
                AreEqual(expectedL[6], actualL[6], valueTolerance);
                AreEqual(expectedL[7], actualL[7], valueTolerance);
                AreEqual(expectedL[8], actualL[8], valueTolerance);

                // Right
                
                AreEqual(expectedR[0], actualR[0], valueTolerance);
                AreEqual(expectedR[1], actualR[1], valueTolerance);
                AreEqual(expectedR[2], actualR[2], valueTolerance);
                AreEqual(expectedR[3], actualR[3], valueTolerance);
                AreEqual(expectedR[4], actualR[4], valueTolerance);
                AreEqual(expectedR[5], actualR[5], valueTolerance);
                AreEqual(expectedR[6], actualR[6], valueTolerance);
                AreEqual(expectedR[7], actualR[7], valueTolerance);
                AreEqual(expectedR[8], actualR[8], valueTolerance);
            }
        }

        private FlowNode Signal(FlowNode frequency, string callerMemberName)
        {
            var sound = Sine(frequency) * VOLUME;
            if (IsStereo)
            {
                sound = sound.Panning(PANNING);
            }
            return sound.SetName(callerMemberName);
        }

        static void AssertAudioFileOutputProperties(
            AudioFileOutput audioFileOutput, 
            AudioFileFormatEnum audioFileFormatEnum,
            int channels, 
            int bits, 
            int samplingRate,
            string expectedFilePath, 
            double expectedDuration,
            string callerMemberName)
        {
            // AudioFileOutput
            IsNotNull(() => audioFileOutput.AudioFileFormat);
            IsNotNull(() => audioFileOutput.SampleDataType);
            IsNotNull(() => audioFileOutput.SpeakerSetup);
            
            AreEqual(samplingRate,        () => audioFileOutput.SamplingRate);
            AreEqual(0,                   () => audioFileOutput.StartTime);
            AreEqual(expectedDuration,    () => audioFileOutput.GetEndTime());
            AreEqual(expectedDuration,    () => audioFileOutput.Duration);
            AreEqual(audioFileFormatEnum, () => audioFileOutput.GetAudioFileFormatEnum());
            AreEqual(bits,                () => audioFileOutput.Bits());
            AreEqual(channels,            () => audioFileOutput.GetChannelCount());
            
            {
                IsNotNull(() => audioFileOutput.FilePath);
                
                string expectedContains = PrettifyName(callerMemberName);
                
                // ReSharper disable UnusedVariable
                (string expectedStart, int number, string expectedEnd) =
                    GetNumberedFilePathParts(expectedFilePath, "", "");
                // ReSharper restore UnusedVariable
                
                
                var values = new 
                { 
                    actualValue = audioFileOutput.FilePath,
                    expectedContains, 
                    expectedEnd
                };
                
                // Outcommented code lines, because file name can be the TapeDescriptor of a dummy Tape which is hard to simulate.

                //IsTrue(Exists(audioFileOutput.FilePath), "Tested expression: Exists(audioFileOutput.FilePath)");
                
                //IsTrue(audioFileOutput.FilePath.StartsWith(expectedStart),
                //    $"Tested Expression: audioFileOutput.FilePath.StartsWith(expectedStart).{NewLine}{values}");
                
                IsTrue(audioFileOutput.FilePath.EndsWith(expectedEnd),
                    $"Tested Expression: audioFileOutput.FilePath.EndsWith(expectedEnd).{NewLine}{values}");
            
                IsTrue(audioFileOutput.FilePath.Contains(expectedContains),
                    $"Tested Expression: audioFileOutput.FilePath.Contains(expectedContains).{NewLine}{values}");

            }
            
            IsTrue(audioFileOutput.ID > 0, "audioFileOutput.ID > 0");
            double expectedAmplifier = bits.MaxValue();
            AreEqual(expectedAmplifier, () => audioFileOutput.Amplifier);
            
            // AudioFileOutputChannels
            IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
            AreEqual(channels, () => audioFileOutput.AudioFileOutputChannels.Count);

            for (var i = 0; i < channels; i++)
            {
                AudioFileOutputChannel channel = audioFileOutput.AudioFileOutputChannels[i];
                IsNotNull(() => channel.Outlet);
                IsNotNull(() => channel.AudioFileOutput);
                AreEqual(i, () => channel.Index);
                IsNotNull(() => channel.AudioFileOutput);
                AreEqual(audioFileOutput, () => channel.AudioFileOutput);
            }
        }

        private void AssertSampleProperties(
            FlowNode sampleFlowNode, 
            AudioFileFormatEnum audioFileFormatEnum, 
            int channels, 
            int bits, 
            InterpolationTypeEnum interpolationTypeEnum,
            int samplingRate,
            double expectedDuration,
            string filePath,
            string callerMemberName)
        {
            // Sample Wrapper
            IsNotNull(() => sampleFlowNode);
            IsNotNull(() => sampleFlowNode.UnderlyingSample());

            // Sample Operator
            Operator sampleOperator = sampleFlowNode.UnderlyingOperator;
            IsNotNull(() => sampleOperator);
            AreEqual("SampleOperator", () => sampleOperator.OperatorTypeName);
            IsNull(() => sampleOperator.AsCurveIn);
            IsNull(() => sampleOperator.AsValueOperator);
            IsNotNull(() => sampleOperator.AsSampleOperator);
            {
                string expectedName = PrettifyName(callerMemberName);
                NotNullOrEmpty(() => sampleOperator.Name);
                IsTrue(() => sampleOperator.Name.Contains(expectedName)); 
            }

            // Sample Inlets
            IsNotNull(() => sampleOperator.Inlets);
            AreEqual(0, () => sampleOperator.Inlets.Count);

            // Sample Outlets
            IsNotNull(() => sampleOperator.Outlets);
            AreEqual(1, () => sampleOperator.Outlets.Count);
            IsNotNull(() => sampleOperator.Outlets[0]);

            // Sample Outlet
            Outlet sampleOutlet = sampleFlowNode;
            IsNotNull(() => sampleOutlet);
            IsNotNull(() => sampleOutlet.Operator);
            AreEqual(sampleOperator, () => sampleOutlet.Operator);
            AreEqual("Result", () => sampleOutlet.Name);
            IsNotNull(() => sampleOutlet.ConnectedInlets);
            AreEqual(0, () => sampleOutlet.ConnectedInlets.Count);
            IsNotNull(() => sampleOutlet.AsAudioFileOutputChannels);
            AreEqual(0, () => sampleOutlet.AsAudioFileOutputChannels.Count);

            // Sample
            Sample sample = sampleOperator.AsSampleOperator.Sample;
            AreEqual(1,                     () => sample.TimeMultiplier);
            AreEqual(true,                  () => sample.IsActive);
            AreEqual(0,                     () => sample.BytesToSkip);
            AreEqual(samplingRate,          () => sample.SamplingRate);
            AreEqual(bits,                  () => sample.Bits());
            AreEqual(channels,              () => sample.GetChannelCount());
            AreEqual(audioFileFormatEnum,   () => sample.GetAudioFileFormatEnum());
            AreEqual(interpolationTypeEnum, () => sample.GetInterpolationTypeEnum());

            // ByteCount
            {
                IsNotNull(() => sample.Bytes);
                NotEqual(0, () => sample.Bytes.Length);
                
                int byteCountExpected  = (int)(audioFileFormatEnum.HeaderLength() + samplingRate * sample.FrameSize() * DURATION);
                int byteCountTolerance = GetByteCountTolerance(bits, channels);
                
                string byteCountDescriptor = NewLine +
                    $"Byte count tolerance = {byteCountTolerance}{NewLine}" +
                    $"Byte count expected = {byteCountExpected}{NewLine}" +
                    $"Byte count actual = {sample.Bytes.Length}";
                
                AreEqual(byteCountExpected, sample.Bytes.Length, byteCountTolerance, byteCountDescriptor);
            }
            
            // Paths
            {
                filePath = filePath;
                //string expectedLocation = GetFullPath(filePath);
                //NotNullOrEmpty(() => sample.Location);
                //AreEqual(expectedLocation, () => sample.Location);

                string expectedName = PrettifyName(callerMemberName);
                NotNullOrEmpty(() => sample.Name);
                IsTrue(() => sampleOperator.Name.Contains(expectedName)); 
            }

            // Sample Duration
            double durationTolerance_RatherHigh = 0.02;
            AreEqual(expectedDuration, sample.GetDuration(), durationTolerance_RatherHigh);
            
            // Sample Outlet From Different Sources
            //Outlet sampleOutlet_ImplicitConversionFromWrapper = sampleWrapper;
            //Outlet sampleOutlet_FromWrapperResult             = sampleWrapper.Result;
            Outlet sampleOutlet_FromOperatorOutlets           = sampleOperator.Outlets[0];
            //IsNotNull(() => sampleOutlet_ImplicitConversionFromWrapper);
            //IsNotNull(() => sampleOutlet_FromWrapperResult);
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
            //AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromWrapperResult);
            //AreEqual(sampleOutlet_ImplicitConversionFromWrapper, () => sampleOutlet_FromOperatorOutlets);
        }

        // Helpers

        internal double GetValueTolerance(
            bool aligned,
            InterpolationTypeEnum interpolation,
            int bits)
        {
            // Worst: Blocky, Not Aligned

            if (!aligned && interpolation == Block && bits == 8)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolation == Block && bits == 16)
            {
                return 0.0325;
            }
            
            if (!aligned && interpolation == Block && bits == 32)
            {
                return 0.0325;
            }

            // Byte
            
            if (aligned && interpolation == Block && bits == 8)
            {
                return 0.008;
            }

            if (!aligned && interpolation == Line && bits == 8)
            {
                return 0.008;
            }

            if (aligned && interpolation == Line && bits == 8)
            {
                return 0.008;
            }

            // Int16 Not Aligned
            
            if (!aligned && interpolation == Line && bits == 16)
            {
                return 0.0006;
            }
            
            // Float32 Non-Aligned
            
            if (!aligned && interpolation == Line && bits == 32)
            {
                return 0.00015;
            }

            // Int16 Aligned

            if (aligned && interpolation == Block && bits == 16)
            {
                return 0.00008;
            }
            
            if (aligned && interpolation == Line && bits == 16)
            {
                return 0.00008;
            }

            // Float32 Aligned

            if (aligned && interpolation == Block && bits == 32)
            {
                return 0.000047;
            }
            
            if (aligned && interpolation == Line && bits == 32)
            {
                return 0.000047;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { interpolationTypeEnum = interpolation, bits });
        }
        
        private int GetByteCountTolerance(int bits, int channels)
            => 4 * bits.SizeOfBitDepth() * channels; // A tolerance of 4 audio frames.

        private double RoundValue(double x) => Round(x, DECIMALS, AwayFromZero);

        private string FormatValues(double[] values)
        {
            string result = string.Join("|", values.Select(FormatValue));
            return result;
        }

        private string FormatValue(double value)
        {
            // Length "-0.1234" = sign + 0 + . + decimals 
            int    length    = 1 + 1 + 1 + DECIMALS; 
            double rounded   = Round(value, DECIMALS, AwayFromZero);
            string formatted = rounded.ToString("F" + DECIMALS);
            string padded    = formatted.PadLeft(length) + " "; 
            return padded;
        }
    }
}