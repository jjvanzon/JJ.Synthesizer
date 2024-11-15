using System;
using System.IO;
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
        public static void WriteWavHeader<TDataType>(
            this BinaryWriter writer, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(typeof(TDataType), speakerSetup, samplingRate, frameCount));

        // With TDataType
        public static void WriteWavHeader<TDataType>(
            this BinaryWriter writer, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(typeof(TDataType), channelCount, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this BinaryWriter writer,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                writer, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));

        // With Stream

        // With TDataType and SpeakerSetup
        public static void WriteWavHeader<TDataType>(
            this Stream stream, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo<TDataType>(speakerSetup, samplingRate, frameCount));
            
        // With TDataType
        public static void WriteWavHeader<TDataType>(
            this Stream stream, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo<TDataType>(channelCount, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this Stream stream, SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                stream, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));

        // With FilePath

        // With TDataType and SpeakerSetup
        public static void WriteWavHeader<TDataType>(
            this string filePath, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(typeof(TDataType), speakerSetup, samplingRate, frameCount));

        // With TDataType
        public static void WriteWavHeader<TDataType>(
            this string filePath, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(typeof(TDataType), channelCount, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => WavHeaderExtensionWishes_WriteFromObjects.WriteWavHeader(
                filePath, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sampleDataTypeEnum, channelCount, samplingRate, frameCount));
    }

    public static class WavHeaderExtensionWishes_WriteFromObjects
    { 
        public static void WriteWavHeader(this BinaryWriter writer, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(writer, WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(entity, frameCount));

        public static void WriteWavHeader(this BinaryWriter writer, AudioInfoWish info)
            => BinaryWriterExtensions.WriteStruct(writer, WavHeaderExtensionWishes_HeaderFromObjects.GetWavHeader(info));

        public static void WriteWavHeader(this BinaryWriter writer, WavHeaderStruct wavHeader)
            => BinaryWriterExtensions.WriteStruct(writer, wavHeader);
        
        public static void WriteWavHeader(this Stream stream, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(new BinaryWriter(stream), entity, frameCount);

        public static void WriteWavHeader(this Stream stream, AudioInfoWish info)
            => WriteWavHeader(new BinaryWriter(stream), info);

        public static void WriteWavHeader(this Stream stream, WavHeaderStruct wavHeader)
            => WriteWavHeader(new BinaryWriter(stream), wavHeader);
        
        public static void WriteWavHeader(this string filePath, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(filePath, WavHeaderExtensionWishes_HeaderFromObjects.GetWavHeader(entity, frameCount));

        public static void WriteWavHeader(this string filePath, AudioInfoWish info)
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

        public static void WriteWavHeader(this AudioInfoWish audioInfo, BinaryWriter writer)
            => WriteWavHeader(writer, audioInfo);

        public static void WriteWavHeader(this AudioInfoWish audioInfo, Stream stream)
            => WriteWavHeader(stream, audioInfo);

        public static void WriteWavHeader(this AudioInfoWish audioInfo, string filePath)
            => WriteWavHeader(filePath, audioInfo);
    }

    public static class WavHeaderExtensionWishes_ReadInfo
    {
        public static AudioInfoWish ReadAudioInfo(this string filePath)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(filePath).GetAudioInfo();

        public static AudioInfoWish ReadAudioInfo(this Stream stream)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(stream).GetAudioInfo();

        public static AudioInfoWish ReadAudioInfo(this BinaryReader reader)
            => WavHeaderExtensionWishes_ReadHeader.ReadWavHeader(reader).GetAudioInfo();
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
        public static WavHeaderStruct GetWavHeader(this AudioInfoWish info)
            => WavHeaderManager.CreateWavHeaderStruct(info.FromWish());

        public static WavHeaderStruct GetWavHeader(this Sample sample)
            => WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(sample).GetWavHeader();

        public static WavHeaderStruct GetWavHeader(this AudioFileOutput audioFileOutput, int frameCount)
            => WavHeaderExtensionWishes_GetAudioInfo.GetAudioInfo(audioFileOutput, frameCount).GetWavHeader();
    }

    public static class WavHeaderExtensionWishes_GetAudioInfo
    {
        // Create Wish Version
        
        public static AudioInfoWish ToWish(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return new AudioInfoWish
            {
                Bits = info.BytesPerValue * 8,
                ChannelCount = info.ChannelCount,
                FrameCount = info.SampleCount,
                SamplingRate = info.SamplingRate
            };
        }

        public static AudioFileInfo FromWish(this AudioInfoWish info)
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
        
        public static AudioInfoWish GetAudioInfo
            <TSampleDataType>(SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = typeof(TSampleDataType).GetBits(),
                ChannelCount = speakerSetup.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioInfoWish GetAudioInfo
            <TSampleDataType>(int channelCount, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = typeof(TSampleDataType).GetBits(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioInfoWish GetAudioInfo(
            Type sampleDataType, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = sampleDataType.GetBits(),
                ChannelCount = speakerSetup.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioInfoWish GetAudioInfo(
            SampleDataTypeEnum sampleDataTypeEnum, int channelCount, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = sampleDataTypeEnum.GetBits(),
                ChannelCount = channelCount,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioInfoWish GetAudioInfo(
            Type sampleDataType, int channelCount, int samplingRate, int frameCount) 
            => new AudioInfoWish
        {
            Bits = sampleDataType.GetBits(),
            ChannelCount = channelCount,
            SamplingRate = samplingRate,
            FrameCount = frameCount
        };

        public static AudioInfoWish GetAudioInfo(
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
        {
            return new AudioInfoWish
            {
                Bits = sampleDataTypeEnum.GetBits(),
                ChannelCount = speakerSetup.GetChannelCount(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        }

        // From Objects
        
        public static AudioInfoWish GetAudioInfo(this WavHeaderStruct wavHeader)
            => WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader).ToWish();

        public static AudioInfoWish GetAudioInfo(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new AudioInfoWish
            {
                Bits = entity.GetBits(),
                ChannelCount = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = entity.GetFrameCount()
            };
        }

        public static AudioInfoWish GetAudioInfo(this AudioFileOutput entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var info = new AudioInfoWish
            {
                Bits = entity.GetBits(),
                ChannelCount = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = frameCount
            };
            return info;
        }
    }
}