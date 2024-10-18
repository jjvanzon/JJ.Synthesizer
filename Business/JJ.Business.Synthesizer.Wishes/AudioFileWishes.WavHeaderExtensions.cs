using System;
using System.IO;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
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
            this BinaryWriter writer,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
            => WriteWavHeader(
                writer, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                speakerSetupEnum, samplingRate, sampleCount);

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter writer,
            int channelCount, int samplingRate, int sampleCount)
            => WriteWavHeader(
                writer, AudioFileExtensionWishes.GetSampleDataTypeEnum<TSampleDataType>(),
                channelCount, samplingRate, sampleCount);

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
            => WriteWavHeader(
                writer, sampleDataTypeEnum, speakerSetupEnum.GetChannelCount(), samplingRate, sampleCount);

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int sampleCount)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            WriteWavHeader(writer, new AudioFileInfo
            {
                BytesPerValue = sampleDataTypeEnum.SizeOf(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                SampleCount = sampleCount,
            });
        }

        public static void WriteWavHeader(
            this BinaryWriter writer, 
            AudioFileOutput audioFileOutput, int sampleCount)
        {
            WriteWavHeader(writer, new AudioFileInfo
            {
                BytesPerValue = audioFileOutput.SampleDataType.SizeOf(),
                ChannelCount = audioFileOutput.GetChannelCount(),
                SamplingRate = audioFileOutput.SamplingRate,
                SampleCount = sampleCount
            });
        }

        public static void WriteWavHeader(
            this BinaryWriter writer, 
            AudioFileInfo audioFileInfo)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            
            var wavHeaderStruct = WavHeaderManager.CreateWavHeaderStruct(audioFileInfo);

            writer.WriteStruct(wavHeaderStruct);
        }
        
        // WriteWavHeader (with Stream)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
            => new BinaryWriter(stream).WriteWavHeader<TSampleDataType>(speakerSetupEnum, samplingRate, sampleCount);

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream,
            int channelCount, int samplingRate, int sampleCount)
            => new BinaryWriter(stream).WriteWavHeader<TSampleDataType>(channelCount, samplingRate, sampleCount);

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
            => new BinaryWriter(stream).WriteWavHeader(sampleDataTypeEnum, speakerSetupEnum, samplingRate, sampleCount);

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int sampleCount)
            => new BinaryWriter(stream).WriteWavHeader(sampleDataTypeEnum, channelCount, samplingRate, sampleCount);

        public static void WriteWavHeader(
            this Stream stream,
            AudioFileOutput audioFileOutput, int sampleCount)
            => new BinaryWriter(stream).WriteWavHeader(audioFileOutput, sampleCount);

        public static void WriteWavHeader(
            this Stream stream,
            AudioFileInfo audioFileInfo)
            => new BinaryWriter(stream).WriteWavHeader(audioFileInfo);

        // WriteWavHeader (with FilePath)

        /// <summary> Overload that takes <see cref="TSampleDataType"/> and <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader<TSampleDataType>(stream, speakerSetupEnum, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes <see cref="TSampleDataType"/> </summary>
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath,
            int channelCount, int samplingRate, int sampleCount)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader<TSampleDataType>(stream, channelCount, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes <see cref="SpeakerSetupEnum"/>. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int sampleCount)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(stream, sampleDataTypeEnum, speakerSetupEnum, samplingRate, sampleCount);
        }

        /// <summary> Overload that takes a more flat list of values. </summary>
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int sampleCount)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(stream, sampleDataTypeEnum, channelCount, samplingRate, sampleCount);
        }
        
        public static void WriteWavHeader(
            this string filePath,
            AudioFileOutput audioFileOutput, int sampleCount)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(stream, audioFileOutput, sampleCount);
        }

        public static void WriteWavHeader(
            this string filePath,
            AudioFileInfo audioFileInfo)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(stream, audioFileInfo);
        }

        // WriteWavHeader (with AudioFileOutput)
        
        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, BinaryWriter writer, int sampleCount)
            => WriteWavHeader(writer, audioFileOutput, sampleCount);

        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, Stream stream, int sampleCount)
            => WriteWavHeader(stream, audioFileOutput, sampleCount);

        public static void WriteWavHeader(
            this AudioFileOutput audioFileOutput, string filePath, int sampleCount)
            => WriteWavHeader(filePath, audioFileOutput, sampleCount);

        // WriteWavHeader (with AudioFileInfo)

        public static void WriteWavHeader(
            this AudioFileInfo audioFileInfo, BinaryWriter writer)
            => WriteWavHeader(writer, audioFileInfo);

        public static void WriteWavHeader(
            this AudioFileInfo audioFileInfo, Stream stream)
            => WriteWavHeader(stream, audioFileInfo);

        public static void WriteWavHeader(
            this AudioFileInfo audioFileInfo, string filePath)
            => WriteWavHeader(filePath, audioFileInfo);

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