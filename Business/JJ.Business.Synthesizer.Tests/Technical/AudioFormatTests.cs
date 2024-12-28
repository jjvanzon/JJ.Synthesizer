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
        //private const double DURATION  = 0.25;
        //private const double DURATION2 = DURATION * 1.01;
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
            [CallerMemberName] string testName = null)
        {
            
            LogLine("");
            LogLine("Options");
            LogLine("-------");
            LogLine("");
            
            int    samplingRate = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            double frequency    = aligned ? ALIGNED_FREQUENCY : NON_ALIGNED_FREQUENCY;
            double signalDuration = (1 / frequency) * 16;
            double reloadDuration = signalDuration * 1.01;
            int fileCount = 0;

            WithPadding(0);
            WithChannels(channels);
            WithBits(bits);
            WithInterpolation(interpolation);
            WithAudioFormat(audioFormat);
            WithSamplingRate(samplingRate);
            
            LogLine(this.ConfigLog(title: ""));
            
            WithAudioLength(signalDuration);
            fileCount++;
            
            LogLine("");
            LogLine("Materialize Signal with \"Record\" (Old Method)");
            LogLine("---------------------------------------------");
            
            Buff signalBuffOld = this.Record(() => Signal(_[frequency], testName), testName);
            
            IsNotNull(() => signalBuffOld);

            Save(signalBuffOld, testName + "_" + fileCount + "_" + nameof(signalBuffOld));

            AudioFileOutput signalAudioFileOutputOld = signalBuffOld.UnderlyingAudioFileOutput;

            LogLine("");
            LogLine("Materialize Signal with \"Run/Intercept\" (New Method)");
            LogLine("----------------------------------------------------");
            LogLine("");

            Tape signalTapeNew = null;
            
            Run(() => Signal(_[frequency], testName).AfterRecord(x => signalTapeNew = x, testName));
            
            IsNotNull(() => signalTapeNew);
            Buff signalBuffNew = signalTapeNew.Buff;
            AudioFileOutput signalAudioFileOutputNew = signalBuffNew.UnderlyingAudioFileOutput;
            
            Save(signalTapeNew, testName + "_" + ++fileCount + "_" + nameof(signalTapeNew));

            LogLine("");
            LogLine("Reload Sample in a FlowNode for \"Record\" (Old Method)");
            LogLine("-----------------------------------------------------");
            LogLine("");

            string reloadedNameOld = $"{testName}_ReloadedOld";
            FlowNode reloadedSampleFlowNodeOld = Sample(signalBuffOld, name: reloadedNameOld);
            Save(reloadedSampleFlowNodeOld.UnderlyingSample(), testName + "_" + ++fileCount + "_reloadedSampleFlowNodeOld_UnderlyingSample");

            LogLine("");
            LogLine("Reload Sample in a FlowNode for \"Run/Intercept\" (New Method)");
            LogLine("------------------------------------------------------------");
            LogLine("");
            
            FlowNode reloadedSampleFlowNodeChannel0New = Sample(signalTapeNew).SetName($"{testName}_ReloadedChannel0New");
            FlowNode reloadedSampleFlowNodeChannel1New = Sample(signalTapeNew).SetName($"{testName}_ReloadedChannel1New");
            Save(reloadedSampleFlowNodeChannel0New.UnderlyingSample(), testName + "_" + ++fileCount + "_reloadedSampleFlowNodeChannel0New_UnderlyingSample");
            Save(reloadedSampleFlowNodeChannel1New.UnderlyingSample(), testName + "_" + ++fileCount + "_reloadedSampleFlowNodeChannel1New_UnderlyingSample");

            LogLine("Done");
            
            WithAudioLength(reloadDuration);
            
            LogLine("");
            LogLine("Reload Sample with \"Record\" (Old Method)");
            LogLine("----------------------------------------");
            LogLine("");

            Buff reloadedSampleBuffOld = this.Record(() => reloadedSampleFlowNodeOld);
            IsNotNull(() => reloadedSampleBuffOld);
            
            Save(reloadedSampleBuffOld, testName + "_" + ++fileCount + "_" + nameof(reloadedSampleBuffOld));

            LogLine();
            LogLine("Reload Sample with \"Run/Intercept\" (New Method)");
            LogLine("-----------------------------------------------");
            LogLine("");

            Tape reloadedSampleTapeNew = null;
            
            Run(() => (GetChannel == 0 ? reloadedSampleFlowNodeChannel0New : reloadedSampleFlowNodeChannel1New).AfterRecord(x => reloadedSampleTapeNew = x));
            
            IsNotNull(() => reloadedSampleTapeNew);
            
            // Signal still correct here
            Save(reloadedSampleTapeNew, testName + "_" + ++fileCount + "_" + nameof(reloadedSampleTapeNew));
            
            LogLine("");
            LogLine("Assert AudioFileOutput Properties");
            LogLine("---------------------------------");
            LogLine("");

            string filePath1Expectation = GetFullPath(PrettifyName(testName) + audioFormat.FileExtension());
            
            AssertAudioFileOutputProperties(
                signalAudioFileOutputOld,
                audioFormat, channels, bits, samplingRate,
                filePath1Expectation, signalDuration, testName);
            
            AssertAudioFileOutputProperties(
                signalAudioFileOutputNew,
                audioFormat, channels, bits, samplingRate,
                filePath1Expectation, signalDuration, testName);

            string filePath2Expectation = GetFullPath(PrettifyName($"{testName}_Reloaded") + audioFormat.FileExtension());
            
            AssertAudioFileOutputProperties(
                reloadedSampleBuffOld.UnderlyingAudioFileOutput,
                audioFormat, channels, bits, samplingRate,
                filePath2Expectation, reloadDuration, testName);
            
            AssertAudioFileOutputProperties(
                reloadedSampleTapeNew.UnderlyingAudioFileOutput,
                audioFormat, channels, bits, samplingRate,
                filePath2Expectation, reloadDuration, testName);

            LogLine("Done");
            
            LogLine("");
            LogLine("Assert Sample Properties");
            LogLine("------------------------");
            LogLine("");

            string filePathExpectation = signalAudioFileOutputNew.FilePath;
            
            AssertSampleProperties(
                reloadedSampleFlowNodeOld,
                audioFormat, channels, bits, interpolation, samplingRate,
                expectedDuration: signalDuration, filePathExpectation, testName);

            AssertSampleProperties(
                reloadedSampleFlowNodeChannel0New,
                audioFormat, channels, bits, interpolation, samplingRate,
                expectedDuration: signalDuration, filePathExpectation, testName);

            AssertSampleProperties(
                reloadedSampleFlowNodeChannel1New,
                audioFormat, channels, bits, interpolation, samplingRate,
                expectedDuration: signalDuration, filePathExpectation, testName);

            LogLine("Done");

            // Mono

            if (channels == 1)
            {
                LogLine("");
                LogLine("Get Mono Values from Sample Reloaded from \"Record\" (Old Method)");
                LogLine("-----------------------------------------------------------------");
                LogLine("");

                double[] actualValuesMonoOld =
                {
                    Calculate(reloadedSampleFlowNodeOld, time: 0.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 1.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 2.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 3.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 4.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 5.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 6.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 7.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeOld, time: 8.0 / 8.0 / frequency)
                };
                
                // Tame ReSharper
                actualValuesMonoOld = actualValuesMonoOld;
                
                LogLine("");
                LogLine("Get Mono Values from Sample Reloaded from \"Run/Intercept\" (New Method)");
                LogLine("----------------------------------------------------------------------");
                LogLine("");

                double[] actualValuesMonoNew =
                {
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 0.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 1.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 2.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 3.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 4.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 5.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 6.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 7.0 / 8.0 / frequency),
                    Calculate(reloadedSampleFlowNodeChannel0New, time: 8.0 / 8.0 / frequency)
                };
                                                
                LogLine("Done");

                LogLine("");
                LogLine("Values");
                LogLine("------");
                LogLine("");

                double[] expectedValuesMono =
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
                expectedValuesMono = expectedValuesMono.Select(RoundValue).ToArray();

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequired = expectedValuesMono.Zip(actualValuesMonoNew, (x,y) => Abs(x - y)).Max();
                LogLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                LogLine($"{nameof(valueToleranceRequired)} = {valueToleranceRequired}");
                LogLine();
                LogLine($"{nameof(expectedValuesMono)}  = {FormatValues(expectedValuesMono)}");
                LogLine($"{nameof(actualValuesMonoNew)} = {FormatValues(actualValuesMonoNew)}");
                LogLine();
                
                AreEqual(expectedValuesMono[0], actualValuesMonoNew[0], valueTolerance);
                AreEqual(expectedValuesMono[1], actualValuesMonoNew[1], valueTolerance);
                AreEqual(expectedValuesMono[2], actualValuesMonoNew[2], valueTolerance);
                AreEqual(expectedValuesMono[3], actualValuesMonoNew[3], valueTolerance);
                AreEqual(expectedValuesMono[4], actualValuesMonoNew[4], valueTolerance);
                AreEqual(expectedValuesMono[5], actualValuesMonoNew[5], valueTolerance);
                AreEqual(expectedValuesMono[6], actualValuesMonoNew[6], valueTolerance);
                AreEqual(expectedValuesMono[7], actualValuesMonoNew[7], valueTolerance);
                AreEqual(expectedValuesMono[8], actualValuesMonoNew[8], valueTolerance);
            }

            // Stereo

            if (channels == 2)
            {
                LogLine("");
                LogLine("Get Values from Reloaded Sample from \"Record\" (New Method)");
                LogLine("----------------------------------------------------------");
                LogLine("");

                double[] actualLeftValuesOld =
                {
                    reloadedSampleFlowNodeOld.Calculate(time: 0.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 1.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 2.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 3.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 4.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 5.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 6.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 7.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeOld.Calculate(time: 8.0 / 8.0 / frequency, channel: 0)
                };

                double[] actualLeftValuesNew =
                {
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 0.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 1.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 2.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 3.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 4.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 5.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 6.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 7.0 / 8.0 / frequency, channel: 0),
                    reloadedSampleFlowNodeChannel0New.Calculate(time: 8.0 / 8.0 / frequency, channel: 0)
                };

                actualLeftValuesNew = actualLeftValuesNew; // Tame ReSharper.

                double[] actualRightValuesOld =
                {
                    reloadedSampleFlowNodeOld.Calculate(time: 0.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 1.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 2.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 3.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 4.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 5.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 6.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 7.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeOld.Calculate(time: 8.0 / 8.0 / frequency, channel: 1)
                };
                
                double[] actualRightValuesNew =
                {
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 0.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 1.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 2.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 3.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 4.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 5.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 6.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 7.0 / 8.0 / frequency, channel: 1),
                    reloadedSampleFlowNodeChannel1New.Calculate(time: 8.0 / 8.0 / frequency, channel: 1)
                };
                
                actualRightValuesNew = actualRightValuesNew; // Tame ReSharper.

                LogLine("Done");

                LogLine("");
                LogLine("Value Info");
                LogLine("----------");
                LogLine("");
                
                double[] expectedLeftValues =
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
                expectedLeftValues = expectedLeftValues.Select(RoundValue).ToArray();

                double[] expectedRightValues =
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
                expectedRightValues = expectedRightValues.Select(RoundValue).ToArray();

                double valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                double valueToleranceRequiredOld 
                    = expectedLeftValues.Concat(expectedRightValues)
                                        .Zip(actualLeftValuesOld.Concat(actualRightValuesOld), (x,y) => Abs(x - y)).Max();

                LogLine($"{nameof(valueTolerance)}         = {valueTolerance}");
                LogLine($"{nameof(valueToleranceRequiredOld)} = {valueToleranceRequiredOld}");
                LogLine();
                LogLine($"{nameof(expectedLeftValues)} = {FormatValues(expectedLeftValues)}");
                LogLine($"  {nameof(actualLeftValuesOld)} = {FormatValues(actualLeftValuesOld  )}");
                LogLine();
                LogLine($"{nameof(expectedRightValues)} = {FormatValues(expectedRightValues)}");
                LogLine($"  {nameof(actualRightValuesOld)} = {FormatValues(actualRightValuesOld  )}");
                LogLine();
                
                LogPrettyTitle("Assert Left Values Old");

                AreEqual(expectedLeftValues[0], actualLeftValuesOld[0], valueTolerance);
                AreEqual(expectedLeftValues[1], actualLeftValuesOld[1], valueTolerance);
                AreEqual(expectedLeftValues[2], actualLeftValuesOld[2], valueTolerance);
                AreEqual(expectedLeftValues[3], actualLeftValuesOld[3], valueTolerance);
                AreEqual(expectedLeftValues[4], actualLeftValuesOld[4], valueTolerance);
                AreEqual(expectedLeftValues[5], actualLeftValuesOld[5], valueTolerance);
                AreEqual(expectedLeftValues[6], actualLeftValuesOld[6], valueTolerance);
                AreEqual(expectedLeftValues[7], actualLeftValuesOld[7], valueTolerance);
                AreEqual(expectedLeftValues[8], actualLeftValuesOld[8], valueTolerance);

                LogLine("Done");
                
                LogPrettyTitle("Assert Right Values Old");
                
                AreEqual(expectedRightValues[0], actualRightValuesOld[0], valueTolerance);
                AreEqual(expectedRightValues[1], actualRightValuesOld[1], valueTolerance);
                AreEqual(expectedRightValues[2], actualRightValuesOld[2], valueTolerance);
                AreEqual(expectedRightValues[3], actualRightValuesOld[3], valueTolerance);
                AreEqual(expectedRightValues[4], actualRightValuesOld[4], valueTolerance);
                AreEqual(expectedRightValues[5], actualRightValuesOld[5], valueTolerance);
                AreEqual(expectedRightValues[6], actualRightValuesOld[6], valueTolerance);
                AreEqual(expectedRightValues[7], actualRightValuesOld[7], valueTolerance);
                AreEqual(expectedRightValues[8], actualRightValuesOld[8], valueTolerance);
            
                LogLine("Done");
                        
                //LogPrettyTitle("Assert Left Values New");

                //AreEqual(expectedLeftValues[0], actualLeftValuesNew[0], valueTolerance);
                //AreEqual(expectedLeftValues[1], actualLeftValuesNew[1], valueTolerance);
                //AreEqual(expectedLeftValues[2], actualLeftValuesNew[2], valueTolerance);
                //AreEqual(expectedLeftValues[3], actualLeftValuesNew[3], valueTolerance);
                //AreEqual(expectedLeftValues[4], actualLeftValuesNew[4], valueTolerance);
                //AreEqual(expectedLeftValues[5], actualLeftValuesNew[5], valueTolerance);
                //AreEqual(expectedLeftValues[6], actualLeftValuesNew[6], valueTolerance);
                //AreEqual(expectedLeftValues[7], actualLeftValuesNew[7], valueTolerance);
                //AreEqual(expectedLeftValues[8], actualLeftValuesNew[8], valueTolerance);
                
                //LogLine("Done");

                //LogPrettyTitle("Assert Right Values New");
                
                //AreEqual(expectedRightValues[0], actualRightValuesNew[0], valueTolerance);
                //AreEqual(expectedRightValues[1], actualRightValuesNew[1], valueTolerance);
                //AreEqual(expectedRightValues[2], actualRightValuesNew[2], valueTolerance);
                //AreEqual(expectedRightValues[3], actualRightValuesNew[3], valueTolerance);
                //AreEqual(expectedRightValues[4], actualRightValuesNew[4], valueTolerance);
                //AreEqual(expectedRightValues[5], actualRightValuesNew[5], valueTolerance);
                //AreEqual(expectedRightValues[6], actualRightValuesNew[6], valueTolerance);
                //AreEqual(expectedRightValues[7], actualRightValuesNew[7], valueTolerance);
                //AreEqual(expectedRightValues[8], actualRightValuesNew[8], valueTolerance);

                //LogLine("Done");
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
                
                int byteCountExpected  = (int)(audioFileFormatEnum.HeaderLength() + samplingRate * sample.FrameSize() * expectedDuration);
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