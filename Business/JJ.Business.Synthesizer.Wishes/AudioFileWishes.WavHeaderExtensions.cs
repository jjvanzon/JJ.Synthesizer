using System;
using System.IO;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    public static class WavHeaderExtensionsWishes
    {
        // WriteWavHeader (with BinaryWriter)
        
        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter bw,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
        {
            WriteWavHeader(
                bw, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                speakerSetupEnum, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter bw,
            int channelCount,
            int samplingRate,
            int sampleCount)
        {
            WriteWavHeader(
                bw, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                channelCount, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
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

        /// <summary> Overload that takes a more flat list of values. </summary>
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

        // WriteWavHeader (with Stream)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        // WriteWavHeader (with FilePath)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();
        
        // WriteWavHeader (with AudioFileOutput)
        
        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this AudioFileOutput audioFileOutput,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this AudioFileOutput audioFileOutput,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput,
            SampleDataTypeEnum sampleDataTypeEnum,
            SpeakerSetupEnum speakerSetupEnum,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput,
            SampleDataTypeEnum sampleDataTypeEnum,
            int channelCount,
            int samplingRate,
            int sampleCount)
            => throw new NotImplementedException();
        
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
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            return reader.ReadStruct<WavHeaderStruct>();
        }

        public static WavHeaderStruct ReadWavHeaderStruct(this Sample sample)
            => throw new NotImplementedException();

        public static WavHeaderStruct ReadWavHeaderStruct(this SampleOperator sampleOperator)
            => throw new NotImplementedException();

        public static WavHeaderStruct ReadWavHeaderStruct(this SampleOperatorWrapper wrapper)
            => throw new NotImplementedException();
        
        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileInfo audioFileInfo)
            => throw new NotImplementedException();

        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileOutput audioFileOutput)
            => throw new NotImplementedException();

        public static WavHeaderStruct GetWavHeaderStruct(this AudioFileOutputChannel audioFileOutputChannel)
            => throw new NotImplementedException();

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
 
        public static AudioFileInfo ReadAudioFileInfo(this Sample sample)
            => throw new NotImplementedException();

        public static AudioFileInfo ReadAudioFileInfo(this SampleOperator sampleOperator)
            => throw new NotImplementedException();

        public static AudioFileInfo ReadAudioFileInfo(this SampleOperatorWrapper wrapper)
            => throw new NotImplementedException();
        
        public static AudioFileInfo GetAudioFileInfo(this WavHeaderStruct audioFileInfo)
            => throw new NotImplementedException();

        public static AudioFileInfo GetAudioFileInfo(this AudioFileOutput audioFileOutput)
            => throw new NotImplementedException();

        public static AudioFileInfo GetAudioFileInfo(this AudioFileOutputChannel audioFileOutputChannel)
            => throw new NotImplementedException();
    }
}