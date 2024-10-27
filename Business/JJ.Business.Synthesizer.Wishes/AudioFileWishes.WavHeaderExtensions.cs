using System;
using System.IO;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;

// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public static class WavHeaderExtensionWishes_WriteFromValues
    {
        // With BinaryWriter

        // With TSampleDataType and SpeakerSetupEnum
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter writer, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetInfo.GetInfo(typeof(TSampleDataType), speakerSetupEnum, samplingRate, frameCount));

        // With TSampleDataType
        public static void WriteWavHeader<TSampleDataType>(
            this BinaryWriter writer, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetInfo.GetInfo(typeof(TSampleDataType), channelCount, samplingRate, frameCount));

        // With SpeakerSetupEnum
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, speakerSetupEnum, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));

        // With Stream

        // With TSampleDataType and SpeakerSetupEnum
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetInfo.GetInfo<TSampleDataType>(speakerSetupEnum, samplingRate, frameCount));
            
        // With TSampleDataType
        public static void WriteWavHeader<TSampleDataType>(
            this Stream stream, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetInfo.GetInfo<TSampleDataType>(channelCount, samplingRate, frameCount));

        // With SpeakerSetupEnum
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, speakerSetupEnum, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this Stream stream, SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));

        // With FilePath

        // With TSampleDataType and SpeakerSetupEnum
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetInfo.GetInfo(typeof(TSampleDataType), speakerSetupEnum, samplingRate, frameCount));

        // With TSampleDataType
        public static void WriteWavHeader<TSampleDataType>(
            this string filePath, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetInfo.GetInfo(typeof(TSampleDataType), channelCount, samplingRate, frameCount));

        // With SpeakerSetupEnum
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, speakerSetupEnum, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetInfo.GetInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));
    }

    public static class WavHeaderExtensionWishes_WriteFromObjects
    { 
        public static void WriteWavHeader(this BinaryWriter writer, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(writer, WavHeaderExtensionWishes_GetInfo.GetInfo(entity, frameCount));

        public static void WriteWavHeader(this BinaryWriter writer, AudioFileInfoWish info)
            => BinaryWriterExtensions.WriteStruct(writer, WavHeaderExtensionWishes_HeaderFromObjects.GetWavHeader(info));

        public static void WriteWavHeader(this BinaryWriter writer, WavHeaderStruct wavHeader)
            => BinaryWriterExtensions.WriteStruct(writer, wavHeader);
        
        public static void WriteWavHeader(this Stream stream, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(new BinaryWriter(stream), entity, frameCount);

        public static void WriteWavHeader(this Stream stream, AudioFileInfoWish info)
            => WriteWavHeader(new BinaryWriter(stream), info);

        public static void WriteWavHeader(this Stream stream, WavHeaderStruct wavHeader)
            => WriteWavHeader(new BinaryWriter(stream), wavHeader);
        
        public static void WriteWavHeader(this string filePath, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(filePath, WavHeaderExtensionWishes_HeaderFromObjects.GetWavHeader(entity, frameCount));

        public static void WriteWavHeader(this string filePath, AudioFileInfoWish info)
            => WriteWavHeader(filePath, WavHeaderExtensionWishes_HeaderFromObjects.GetWavHeader(info));

        public static void WriteWavHeader(this string filePath, WavHeaderStruct wavHeader)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, wavHeader);
        }

        // Inverse Arguments
        
        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, BinaryWriter writer, int frameCount)
            => WriteWavHeader(writer, audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, Stream stream, int frameCount)
            => WriteWavHeader(stream, audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, string filePath, int frameCount)
            => WriteWavHeader(filePath, audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioFileInfoWish audioFileInfo, BinaryWriter writer)
            => WriteWavHeader(writer, audioFileInfo);

        public static void WriteWavHeader(this AudioFileInfoWish audioFileInfo, Stream stream)
            => WriteWavHeader(stream, audioFileInfo);

        public static void WriteWavHeader(this AudioFileInfoWish audioFileInfo, string filePath)
            => WriteWavHeader(filePath, audioFileInfo);
    }

    public static class WavHeaderExtensionWishes_ReadInfo
    {
        public static AudioFileInfoWish ReadInfo(this string filePath)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(filePath).GetInfo();

        public static AudioFileInfoWish ReadInfo(this Stream stream)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(stream).GetInfo();

        public static AudioFileInfoWish ReadInfo(this BinaryReader reader)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(reader).GetInfo();
    }

    public static class WavHeaderExtensionWishes_ReadHeader
    { 
        public static WavHeaderStruct ReadWavHeader(this string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return ReadWavHeader(fileStream);
        }

        public static WavHeaderStruct ReadWavHeader(this Stream stream)
            => ReadWavHeader(new BinaryReader(stream));

        public static WavHeaderStruct ReadWavHeader(this BinaryReader reader)
            => BinaryWriterExtensions.ReadStruct<WavHeaderStruct>(reader);
    }

    public static class WavHeaderExtensionWishes_HeaderFromObjects
    { 
        public static WavHeaderStruct GetWavHeader(this AudioFileInfoWish info)
            => WavHeaderManager.CreateWavHeaderStruct(info.FromWish());

        public static WavHeaderStruct GetWavHeader(this Sample sample)
            => WavHeaderExtensionWishes_GetInfo.GetInfo(sample).GetWavHeader();


        public static WavHeaderStruct GetWavHeader(this SampleOperatorWrapper wrapper)
            => WavHeaderExtensionWishes_GetInfo.GetInfo(wrapper).GetWavHeader();

        public static WavHeaderStruct GetWavHeader(this AudioFileOutput audioFileOutput, int frameCount)
            => WavHeaderExtensionWishes_GetInfo.GetInfo(audioFileOutput, frameCount).GetWavHeader();

        public static WavHeaderStruct GetWavHeader(this AudioFileOutputChannel audioFileOutputChannel, int frameCount)
            => WavHeaderExtensionWishes_GetInfo.GetInfo(audioFileOutputChannel, frameCount).GetWavHeader();
    }

    public static class WavHeaderExtensionWishes_GetInfo
    {
        // Create Wish Version
        
        public static AudioFileInfoWish ToWish(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return new AudioFileInfoWish
            {
                Bits = info.BytesPerValue * 8,
                ChannelCount = info.ChannelCount,
                FrameCount = info.SampleCount,
                SamplingRate = info.SamplingRate
            };
        }

        public static AudioFileInfo FromWish(this AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return new AudioFileInfo
            {
                BytesPerValue = info.Bits / 8,
                ChannelCount = info.ChannelCount,
                SampleCount = info.FrameCount,
                SamplingRate = info.SamplingRate
            };
        }

        // From Loose Values
        
        public static AudioFileInfoWish GetInfo
            <TSampleDataType>(SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => new AudioFileInfoWish
            {
                Bits = typeof(TSampleDataType).GetBits(),
                ChannelCount = speakerSetupEnum.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioFileInfoWish GetInfo
            <TSampleDataType>(int channelCount, int samplingRate, int frameCount)
            => new AudioFileInfoWish
            {
                Bits = typeof(TSampleDataType).GetBits(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioFileInfoWish GetInfo(
            Type sampleDataType, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
            => new AudioFileInfoWish
            {
                Bits = sampleDataType.GetBits(),
                ChannelCount = speakerSetupEnum.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioFileInfoWish GetInfo(
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => new AudioFileInfoWish
            {
                Bits = sampleDataTypeEnum.GetBits(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioFileInfoWish GetInfo(
            Type sampleDataType, int channelCount, int samplingRate, int frameCount) 
            => new AudioFileInfoWish
        {
            Bits = sampleDataType.GetBits(),
            ChannelCount = channelCount,
            SamplingRate = samplingRate,
            FrameCount = frameCount
        };

        public static AudioFileInfoWish GetInfo(
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int samplingRate, int frameCount)
        {
            return new AudioFileInfoWish
            {
                Bits = sampleDataTypeEnum.GetBits(),
                ChannelCount = speakerSetupEnum.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        }

        // From Objects
        
        public static AudioFileInfoWish GetInfo(this WavHeaderStruct wavHeader)
            => WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader).ToWish();

        public static AudioFileInfoWish GetInfo(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new AudioFileInfoWish
            {
                Bits = entity.GetBits(),
                ChannelCount = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = entity.GetFrameCount()
            };
        }


        public static AudioFileInfoWish GetInfo(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetInfo(wrapper.Sample);
        }

        public static AudioFileInfoWish GetInfo(this AudioFileOutput entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var info = new AudioFileInfoWish
            {
                Bits = entity.GetBits(),
                ChannelCount = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = frameCount
            };
            return info;
        }

        public static AudioFileInfoWish GetInfo(this AudioFileOutputChannel entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var info = GetInfo(entity.AudioFileOutput, frameCount);
            info.ChannelCount = 1;
            return info;
        }
    }
}