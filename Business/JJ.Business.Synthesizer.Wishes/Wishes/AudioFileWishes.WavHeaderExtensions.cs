using System;
using System.IO;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.IO;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class WavHeaderExtensionsWishes
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
        
        // Reading Wav Header

        public static WavHeaderStruct ReadWavHeaderStruct(this string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return ReadWavHeaderStruct(fileStream);
            }
        }

        public static WavHeaderStruct ReadWavHeaderStruct(this Stream stream)
            => ReadWavHeaderStruct(new BinaryReader(stream));

        public static WavHeaderStruct ReadWavHeaderStruct(this BinaryReader reader)
        {
            if (reader == null) throw new NullException(() => reader);
            return reader.ReadStruct<WavHeaderStruct>();
        }
    
        public static AudioFileInfo ReadAudioFileInfo(this string filePath)
        {
            WavHeaderStruct wavHeaderStruct = ReadWavHeaderStruct(filePath);
            return WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
        }

        public static AudioFileInfo ReadAudioFileInfo(this Stream stream)
        {
            WavHeaderStruct wavHeaderStruct = ReadWavHeaderStruct(stream);
            return WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
        }

        public static AudioFileInfo ReadAudioFileInfo(this BinaryReader reader)
        {
            WavHeaderStruct wavHeaderStruct = ReadWavHeaderStruct(reader);
            return WavHeaderStructToAudioFileInfoConverter.Convert(wavHeaderStruct);
        }
    }
}