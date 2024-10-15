using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class WavHeaderExtensions
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
                bw, AudioConversionExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
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
                bw, AudioConversionExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
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
                bw, sampleDataTypeEnum, speakerSetupEnum.GetChannelCount(),
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