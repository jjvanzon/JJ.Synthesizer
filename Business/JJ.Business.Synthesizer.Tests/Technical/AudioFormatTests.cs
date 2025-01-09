using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using static System.Environment;
using static System.IO.Path;
using static System.Math;
using static System.MidpointRounding;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Tests.Accessors.FileWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Obsolete.SaveLegacyStatics;

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
        private const int    DECIMALS  = 4;

        [TestMethod] public void Test_AudioFormat_Raw_Mono__8Bit_Blocky_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 8,  Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono__8Bit_Blocky_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 8,  Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Mono__8Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 8,  Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono__8Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 8,  Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_16Bit_Blocky_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_16Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_16Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 16, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_32Bit_Blocky_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_32Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Mono_32Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Raw, 1, 32, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo__8Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 8,  Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo__8Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 8,  Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo__8Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 8,  Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo__8Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 8,  Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_16Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_16Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 16, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_32Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Raw_Stereo_32Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Raw, 2, 32, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono__8Bit_Blocky_Aligned     () => new AudioFormatTests().TestAudioFormat(Wav, 1, 8,  Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono__8Bit_Blocky_NonAligned  () => new AudioFormatTests().TestAudioFormat(Wav, 1, 8,  Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono__8Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Wav, 1, 8,  Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono__8Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Wav, 1, 8,  Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_16Bit_Block_Aligned      () => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_16Bit_Block_NonAligned   () => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_16Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_16Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Wav, 1, 16, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_32Bit_Block_Aligned      () => new AudioFormatTests().TestAudioFormat(Wav,  1, 32, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_32Bit_Block_NonAligned   () => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_32Bit_Linear_Aligned     () => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Mono_32Bit_Linear_NonAligned  () => new AudioFormatTests().TestAudioFormat(Wav, 1, 32, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo__8Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 8,  Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo__8Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 8,  Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo__8Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 8,  Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo__8Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 8,  Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_16Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_16Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 16, Line,  aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Block, aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_32Bit_Blocky_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Block, aligned: false);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_Aligned   () => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Line,  aligned: true);
        [TestMethod] public void Test_AudioFormat_Wav_Stereo_32Bit_Linear_NonAligned() => new AudioFormatTests().TestAudioFormat(Wav, 2, 32, Line,  aligned: false);
        
        void TestAudioFormat(
            AudioFileFormatEnum audioFormat,
            int channels,
            int bits,
            InterpolationTypeEnum interpolation,
            bool aligned,
            [CallerMemberName] string testName = null)
        {
            int      samplingRate   = aligned ? ALIGNED_SAMPLING_RATE : NON_ALIGNED_SAMPLING_RATE;
            var      freq           = aligned ? _[ALIGNED_FREQUENCY] : _[NON_ALIGNED_FREQUENCY];
            double   signalDuration = 1 / freq.Value * 16;
            double   reloadDuration = signalDuration * 1.01;
            int      fileNum        = 1;
            string   fileName, fileName0, fileName1;
            Tape     tape           = default;
            Buff     buff;
            FlowNode node, node0, node1;

            Log();
            Log("Old method: Save(() => Sine(A4).Volume(0.5)).Play();");
            Log("New method: Run(MySound); void MySound() => Sine(A4).Volume(0.5).Save().Play();");

            LogTitleStrong("Options");
            {
                WithPadding(0);
                WithChannels(channels);
                WithBits(bits);
                WithInterpolation(interpolation);
                WithAudioFormat(audioFormat);
                WithSamplingRate(samplingRate);
                LogConfig("", this);
            }
            
            WithAudioLength(signalDuration);
        
            LogTitleStrong("Materialize Signal Old"); Buff signalBuffOld;
            {
                fileName = testName + "_" + fileNum++ + "_" + nameof(signalBuffOld);
                buff = SaveLegacy(this, () => Signal(freq, testName).SaveChannels(fileName + "_Channel" + GetChannel), fileName);
                signalBuffOld = buff;
            }

            LogTitleStrong("Materialize Signal New"); Tape signalTapeNew;
            {
                fileName = testName + "_" + fileNum++ + "_" + nameof(signalTapeNew);
                Run(() => Signal(freq, testName).AfterRecord(x => tape = x, testName)
                                                .Save(fileName)
                                                .SaveChannels(fileName + "_Channel" + GetChannel));
                signalTapeNew = tape;
            }
            
            //return;
            
            WithAudioLength(reloadDuration);

            LogTitleStrong("Reload Sample into FlowNode Old"); FlowNode reloadedSampleNodeOld;
            {
                fileName = testName + "_" + fileNum++ + "_" + nameof(reloadedSampleNodeOld);
                node     = Sample(signalBuffOld, name: fileName);
                Save(node.UnderlyingSample(), fileName + "_UnderlyingSample");
                reloadedSampleNodeOld = node;
            }
            
            LogTitleStrong("Reload Sample into FlowNode New"); FlowNode reloadedSampleNodeChan0New, reloadedSampleNodeChan1New;
            {   
                fileName0 = testName + "_" + fileNum++ + "_" + nameof(reloadedSampleNodeChan0New);
                fileName1 = testName + "_" + fileNum++ + "_" + nameof(reloadedSampleNodeChan1New);
                node0     = Sample(signalTapeNew).SetName(fileName0);
                node1     = Sample(signalTapeNew).SetName(fileName1);
                Save(node0.UnderlyingSample(), fileName0 + "_" + nameof(node0.UnderlyingSample));
                Save(node1.UnderlyingSample(), fileName1 + "_" + nameof(node1.UnderlyingSample));
                reloadedSampleNodeChan0New = node0;
                reloadedSampleNodeChan1New = node1;
            }

            LogTitleStrong("Record Reloaded Sample Old"); Buff reloadedSampleBuffOld;
            {
                fileName = testName + "_" + fileNum++ + "_" + nameof(reloadedSampleBuffOld);
                buff = this.SaveLegacy(() => reloadedSampleNodeOld.SaveChannels(fileName + "_Channel" + GetChannel), fileName);
                reloadedSampleBuffOld = buff;
            }
            
            LogTitleStrong("Record Reloaded Sample New"); Tape reloadedSampleTapeNew;
            {
                fileName = testName + "_" + fileNum++ + "_" + nameof(reloadedSampleTapeNew);
                node0 = reloadedSampleNodeChan0New;
                node1 = reloadedSampleNodeChan1New;
                Run(() => (GetChannel == 0 ? node0 : node1).AfterRecord(x => tape = x)
                                                           .Save(fileName)
                                                           .SaveChannels(fileName + "_Channel" + GetChannel));
                reloadedSampleTapeNew = tape;
            }
            
            LogTitleStrong("Assert AudioFileOut Properties");
            {
                string filePath1Expectation = GetFullPath(testName + audioFormat.FileExtension());

                AssertAudioFileOutProperties(
                    signalBuffOld.UnderlyingAudioFileOutput,
                    audioFormat, channels, bits, samplingRate,
                    filePath1Expectation, signalDuration, testName);
            
                AssertAudioFileOutProperties(
                    signalTapeNew.UnderlyingAudioFileOutput,
                    audioFormat, channels, bits, samplingRate,
                    filePath1Expectation, signalDuration, testName);

                string filePath2Expectation = GetFullPath($"{testName}_Reloaded" + audioFormat.FileExtension());
            
                AssertAudioFileOutProperties(
                    reloadedSampleBuffOld.UnderlyingAudioFileOutput,
                    audioFormat, channels, bits, samplingRate,
                    filePath2Expectation, reloadDuration, testName);
            
                AssertAudioFileOutProperties(
                    reloadedSampleTapeNew.UnderlyingAudioFileOutput,
                    audioFormat, channels, bits, samplingRate,
                    filePath2Expectation, reloadDuration, testName);

                Log("Done");
            }
            
            LogTitleStrong("Assert Sample Properties");
            {
                AssertSampleProperties(
                    reloadedSampleNodeOld,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: signalDuration, testName);

                AssertSampleProperties(
                    reloadedSampleNodeChan0New,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: signalDuration, testName);

                AssertSampleProperties(
                    reloadedSampleNodeChan1New,
                    audioFormat, channels, bits, interpolation, samplingRate,
                    expectedDuration: signalDuration, testName);

                Log("Done");
            }
            
            // Mono

            if (channels == 1)
            {
                LogTitleStrong("Get Mono Values from Sample Reloaded Old"); double[] actualValuesMonoOld;
                {
                    actualValuesMonoOld = new[]
                    {
                        Calculate(reloadedSampleNodeOld, time: 0.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 1.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 2.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 3.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 4.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 5.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 6.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 7.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeOld, time: 8.0 / 8.0 / freq.Value)
                    };
                    Log("Done");
                }
                
                LogTitleStrong("Get Mono Values from Sample Reloaded New"); double[] actualValuesMonoNew;
                {
                    actualValuesMonoNew = new[]
                    {
                        Calculate(reloadedSampleNodeChan0New, time: 0.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 1.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 2.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 3.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 4.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 5.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 6.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 7.0 / 8.0 / freq.Value),
                        Calculate(reloadedSampleNodeChan0New, time: 8.0 / 8.0 / freq.Value)
                    };
                    
                    Log("Done");
                }
                
                LogTitleStrong("Values"); double[] expectedValuesMono; double valueTolerance;
                {
                    expectedValuesMono = new[]
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

                    valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                    double valueToleranceRequiredOld = expectedValuesMono.Zip(actualValuesMonoOld, (x,y) => Abs(x - y)).Max();
                    double valueToleranceRequiredNew = expectedValuesMono.Zip(actualValuesMonoNew, (x,y) => Abs(x - y)).Max();
                    Log($"{nameof(valueTolerance)}            = {valueTolerance}");
                    Log($"{nameof(valueToleranceRequiredOld)} = {valueToleranceRequiredOld}");
                    Log($"{nameof(valueToleranceRequiredNew)} = {valueToleranceRequiredNew}");
                    Log();
                    Log($"{nameof(expectedValuesMono)}  = {FormatValues(expectedValuesMono)}");
                    Log($"{nameof(actualValuesMonoOld)} = {FormatValues(actualValuesMonoOld)}");
                    Log($"{nameof(actualValuesMonoNew)} = {FormatValues(actualValuesMonoNew)}");
                    Log();
                }
                
                LogTitleStrong("Assert Mono Values Old");
                {
                    AreEqual(expectedValuesMono[0], actualValuesMonoOld[0], valueTolerance);
                    AreEqual(expectedValuesMono[1], actualValuesMonoOld[1], valueTolerance);
                    AreEqual(expectedValuesMono[2], actualValuesMonoOld[2], valueTolerance);
                    AreEqual(expectedValuesMono[3], actualValuesMonoOld[3], valueTolerance);
                    AreEqual(expectedValuesMono[4], actualValuesMonoOld[4], valueTolerance);
                    AreEqual(expectedValuesMono[5], actualValuesMonoOld[5], valueTolerance);
                    AreEqual(expectedValuesMono[6], actualValuesMonoOld[6], valueTolerance);
                    AreEqual(expectedValuesMono[7], actualValuesMonoOld[7], valueTolerance);
                    AreEqual(expectedValuesMono[8], actualValuesMonoOld[8], valueTolerance);
                }
                
                LogTitleStrong("Assert Mono Values New");
                {
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
            }
            
            // Stereo

            if (channels == 2)
            {
                LogTitleStrong("Get Values from Reloaded Sample Old"); double[] actualLeftValuesOld, actualRightValuesOld;
                {
                    actualLeftValuesOld = new []
                    {
                        reloadedSampleNodeOld.Calculate(time: 0.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 1.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 2.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 3.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 4.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 5.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 6.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 7.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeOld.Calculate(time: 8.0 / 8.0 / freq.Value, channel: 0)
                    };

                    actualRightValuesOld = new []
                    {
                        reloadedSampleNodeOld.Calculate(time: 0.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 1.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 2.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 3.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 4.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 5.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 6.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 7.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeOld.Calculate(time: 8.0 / 8.0 / freq.Value, channel: 1)
                    };
                    
                    Log("Done");
                }
                
                LogTitleStrong("Get Values from Reloaded Sample New"); double[] actualLeftValuesNew, actualRightValuesNew;
                {
                    actualLeftValuesNew = new []
                    {
                        reloadedSampleNodeChan0New.Calculate(time: 0.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 1.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 2.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 3.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 4.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 5.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 6.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 7.0 / 8.0 / freq.Value, channel: 0),
                        reloadedSampleNodeChan0New.Calculate(time: 8.0 / 8.0 / freq.Value, channel: 0)
                    };

                    actualRightValuesNew = new []
                    {
                        reloadedSampleNodeChan1New.Calculate(time: 0.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 1.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 2.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 3.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 4.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 5.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 6.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 7.0 / 8.0 / freq.Value, channel: 1),
                        reloadedSampleNodeChan1New.Calculate(time: 8.0 / 8.0 / freq.Value, channel: 1)
                    };

                    Log("Done");
                }
                
                LogTitleStrong("Value Info"); double[] expectedLeftValues, expectedRightValues; double valueTolerance;
                {
                    expectedLeftValues = new []
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

                    expectedRightValues = new []
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
                
                    // Value Tolerance
                    valueTolerance = GetValueTolerance(aligned, interpolation, bits);
                    double valueToleranceRequiredOld = 
                        expectedLeftValues.Concat(expectedRightValues)
                                          .Zip(actualLeftValuesOld.Concat(actualRightValuesOld), (x,y) => Abs(x - y)).Max();
                    double valueToleranceRequiredNew = 
                        expectedLeftValues.Concat(expectedRightValues)
                                          .Zip(actualLeftValuesNew.Concat(actualRightValuesNew), (x,y) => Abs(x - y)).Max();
                    Log();
                    Log("Value tolerance:");
                    Log();
                    Log($"Used         = {valueTolerance}");
                    Log($"Required old = {valueToleranceRequiredOld}");
                    Log($"Required new = {valueToleranceRequiredNew}");
                    Log();
                    Log("Left:");
                    Log();
                    Log($"Expected   = {FormatValues(expectedLeftValues )}");
                    Log($"Actual old = {FormatValues(actualLeftValuesOld)}");
                    Log($"Actual new = {FormatValues(actualLeftValuesNew)}");
                    Log();
                    Log("Right:");
                    Log();
                    Log($"Expected   = {FormatValues(expectedRightValues )}");
                    Log($"Actual old = {FormatValues(actualRightValuesOld)}");
                    Log($"Actual new = {FormatValues(actualRightValuesNew)}");
                    Log();
                }

                LogTitleStrong("Assert Left Values Old");
                {
                    AreEqual(expectedLeftValues[0], actualLeftValuesOld[0], valueTolerance);
                    AreEqual(expectedLeftValues[1], actualLeftValuesOld[1], valueTolerance);
                    AreEqual(expectedLeftValues[2], actualLeftValuesOld[2], valueTolerance);
                    AreEqual(expectedLeftValues[3], actualLeftValuesOld[3], valueTolerance);
                    AreEqual(expectedLeftValues[4], actualLeftValuesOld[4], valueTolerance);
                    AreEqual(expectedLeftValues[5], actualLeftValuesOld[5], valueTolerance);
                    AreEqual(expectedLeftValues[6], actualLeftValuesOld[6], valueTolerance);
                    AreEqual(expectedLeftValues[7], actualLeftValuesOld[7], valueTolerance);
                    AreEqual(expectedLeftValues[8], actualLeftValuesOld[8], valueTolerance);
                    Log("Done");
                }
                
                LogTitleStrong("Assert Values Right Old");
                {
                    AreEqual(expectedRightValues[0], actualRightValuesOld[0], valueTolerance);
                    AreEqual(expectedRightValues[1], actualRightValuesOld[1], valueTolerance);
                    AreEqual(expectedRightValues[2], actualRightValuesOld[2], valueTolerance);
                    AreEqual(expectedRightValues[3], actualRightValuesOld[3], valueTolerance);
                    AreEqual(expectedRightValues[4], actualRightValuesOld[4], valueTolerance);
                    AreEqual(expectedRightValues[5], actualRightValuesOld[5], valueTolerance);
                    AreEqual(expectedRightValues[6], actualRightValuesOld[6], valueTolerance);
                    AreEqual(expectedRightValues[7], actualRightValuesOld[7], valueTolerance);
                    AreEqual(expectedRightValues[8], actualRightValuesOld[8], valueTolerance);
                    Log("Done");
                }

                //LogTitleStrong("Assert Left Values New");
                //{
                //    AreEqual(expectedLeftValues[0], actualLeftValuesNew[0], valueTolerance);
                //    AreEqual(expectedLeftValues[1], actualLeftValuesNew[1], valueTolerance);
                //    AreEqual(expectedLeftValues[2], actualLeftValuesNew[2], valueTolerance);
                //    AreEqual(expectedLeftValues[3], actualLeftValuesNew[3], valueTolerance);
                //    AreEqual(expectedLeftValues[4], actualLeftValuesNew[4], valueTolerance);
                //    AreEqual(expectedLeftValues[5], actualLeftValuesNew[5], valueTolerance);
                //    AreEqual(expectedLeftValues[6], actualLeftValuesNew[6], valueTolerance);
                //    AreEqual(expectedLeftValues[7], actualLeftValuesNew[7], valueTolerance);
                //    AreEqual(expectedLeftValues[8], actualLeftValuesNew[8], valueTolerance);
                //    Log("Done");
                //}

                //LogTitleStrong("Assert Values Right New");
                //{
                //    AreEqual(expectedRightValues[0], actualRightValuesNew[0], valueTolerance);
                //    AreEqual(expectedRightValues[1], actualRightValuesNew[1], valueTolerance);
                //    AreEqual(expectedRightValues[2], actualRightValuesNew[2], valueTolerance);
                //    AreEqual(expectedRightValues[3], actualRightValuesNew[3], valueTolerance);
                //    AreEqual(expectedRightValues[4], actualRightValuesNew[4], valueTolerance);
                //    AreEqual(expectedRightValues[5], actualRightValuesNew[5], valueTolerance);
                //    AreEqual(expectedRightValues[6], actualRightValuesNew[6], valueTolerance);
                //    AreEqual(expectedRightValues[7], actualRightValuesNew[7], valueTolerance);
                //    AreEqual(expectedRightValues[8], actualRightValuesNew[8], valueTolerance);
                //    Log("Done");
                //}
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

        static void AssertAudioFileOutProperties(
            AudioFileOutput audioFileOutput, 
            AudioFileFormatEnum audioFileFormatEnum,
            int channels, 
            int bits, 
            int samplingRate,
            string filePathExpectation, 
            double expectedDuration,
            string callerMemberName)
        {
            // AudioFileOutput
            IsNotNull(() => audioFileOutput);
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
                
                string expectedContains = callerMemberName;
                
                (string expectedStart, int number, string expectedEnd) =
                    GetNumberedFilePathParts(filePathExpectation, "", "");
                
                // Tame ReSharper
                expectedStart = expectedStart;
                number = number;
                
                var values = new 
                { 
                    actualValue = audioFileOutput.FilePath,
                    expectedContains, 
                    expectedEnd
                };
                
                // File name comparison needs to be rather lenient, since file name can be the TapeDescriptor of a dummy Tape which is hard to simulate.
                
                //StringWishes.Contains
                IsTrue(audioFileOutput.FilePath.EndsWith(expectedEnd),
                    $"Tested Expression: audioFileOutput.FilePath.EndsWith(expectedEnd).{NewLine}{values}");
            
                IsTrue(audioFileOutput.FilePath.Contains(expectedContains, ignoreCase: true),
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
            string callerMemberName)
        {
            // Sample FlowNode
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
                string expectedName = PrettifyName(callerMemberName);
                NotNullOrEmpty(() => sample.Name);
                IsTrue(() => sampleOperator.Name.Contains(expectedName)); 
            }

            // Sample Duration
            double durationTolerance_RatherHigh = 0.02;
            AreEqual(expectedDuration, sample.GetDuration(), durationTolerance_RatherHigh);
            
            // Sample Outlet From Different Sources
            Outlet sampleOutlet_FromOperatorOutlets = sampleOperator.Outlets[0];
            IsNotNull(() => sampleOutlet_FromOperatorOutlets);
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