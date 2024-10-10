using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    /// <summary> I wish these things were in JJ.Synthesizer </summary>
    public static class WavHeaderWishes
    {
        // WriteWavHeader

        /// <summary> Overload that takes &lt;TSampleDataType&gt; and SpeakerSetupEnum. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter bw,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
        {
            WriteWavHeader(
                bw, AudioConversionExtensions.GetSampleDataTypeEnum<TSampleDataType>(),
                speakerSetupEnum, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes &lt;TSampleDataType&gt;. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter bw,
            int channelCount,
            int samplingRate,
            int sampleCount)
        {
            WriteWavHeader(
                bw, AudioConversionExtensions.GetSampleDataTypeEnum<TSampleDataType>(),
                channelCount, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes SpeakerSetupEnum. </summary>
        public static void WriteWavHeader(
            this BinaryWriter bw,
            SampleDataTypeEnum sampleDataTypeEnum,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
        {
            WriteWavHeader(
                bw, sampleDataTypeEnum, speakerSetupEnum.ToChannelCount(),
                samplingRate, sampleCount);
        }

        public static void WriteWavHeader(
            this BinaryWriter bw,
            SampleDataTypeEnum sampleDataTypeEnum,
            int channelCount,
            int samplingRate,
            int sampleCount)
        {
            var audioFileInfo = new AudioFileInfo
            {
                SamplingRate = samplingRate,
                SampleCount = sampleCount,
                BytesPerValue = SampleDataTypeHelper.SizeOf(sampleDataTypeEnum),
                ChannelCount = channelCount
            };

            var wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);

            bw.WriteStruct(wavHeaderStruct);
        }
    }
}