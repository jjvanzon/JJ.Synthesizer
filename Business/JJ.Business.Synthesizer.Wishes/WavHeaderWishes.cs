using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.ToAudioInfoWishExtensions;

// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public static class WriteWavHeaderExtensions_FromValues
    {
        // With BinaryWriter

        // With TSampleDataType and SpeakerSetupEnum
        public static void WriteWavHeader<TBits>(
            this BinaryWriter writer, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(typeof(TBits), speakerSetup, samplingRate, frameCount));

        // With TDataType
        public static void WriteWavHeader<TBits>(
            this BinaryWriter writer, int channels, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(typeof(TBits), channels, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataTypeEnum sampleDataTypeEnum, int channels, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(sampleDataTypeEnum, channels, samplingRate, frameCount));

        // With Stream

        // With TDataType and SpeakerSetup
        public static void WriteWavHeader<TBits>(
            this Stream stream, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish<TBits>(speakerSetup, samplingRate, frameCount));
            
        // With TDataType
        public static void WriteWavHeader<TBits>(
            this Stream stream, int channels, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish<TBits>(channels, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this Stream stream, SampleDataTypeEnum sampleDataTypeEnum, int channels, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish(sampleDataTypeEnum, channels, samplingRate, frameCount));

        // With FilePath

        // With TDataType and SpeakerSetup
        public static void WriteWavHeader<TBits>(
            this string filePath, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(typeof(TBits), speakerSetup, samplingRate, frameCount));

        // With TDataType
        public static void WriteWavHeader<TBits>(
            this string filePath, int channels, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(typeof(TBits), channels, samplingRate, frameCount));

        // With SpeakerSetup
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(sampleDataTypeEnum, speakerSetup, samplingRate, frameCount));

        // With flat values
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum sampleDataTypeEnum, int channels, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(sampleDataTypeEnum, channels, samplingRate, frameCount));
    }

    public static class WriteWavHeader_FromObjects
    { 
        public static void WriteWavHeader(this BinaryWriter writer, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(writer, ToAudioInfoWishExtensions.ToWish(entity, frameCount));

        public static void WriteWavHeader(this BinaryWriter writer, AudioInfoWish info)
            => writer.WriteStruct(ToWavHeaderExtensions.ToWavHeader(info));

        public static void WriteWavHeader(this BinaryWriter writer, WavHeaderStruct wavHeader)
            => writer.WriteStruct(wavHeader);
        
        public static void WriteWavHeader(this Stream stream, AudioFileOutput entity, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader(entity, frameCount);

        public static void WriteWavHeader(this Stream stream, AudioInfoWish info)
            => new BinaryWriter(stream).WriteWavHeader(info);

        public static void WriteWavHeader(this Stream stream, WavHeaderStruct wavHeader)
            => new BinaryWriter(stream).WriteWavHeader(wavHeader);
        
        public static void WriteWavHeader(this string filePath, AudioFileOutput entity, int frameCount)
            => filePath.WriteWavHeader(entity.ToWavHeader(frameCount));
        
        public static void WriteWavHeader(this string filePath, AudioInfoWish info)
            => filePath.WriteWavHeader(info.ToWavHeader());

        
        public static void WriteWavHeader(this string filePath, WavHeaderStruct wavHeader)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                WriteWavHeader(fileStream, wavHeader);
        }

        // Inverse Arguments
        
        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, BinaryWriter writer, int frameCount)
            => writer.WriteWavHeader(audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, Stream stream, int frameCount)
            => stream.WriteWavHeader(audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioFileOutput audioFileOutput, string filePath, int frameCount)
            => filePath.WriteWavHeader(audioFileOutput, frameCount);

        public static void WriteWavHeader(this AudioInfoWish audioInfo, BinaryWriter writer)
            => writer.WriteWavHeader(audioInfo);

        public static void WriteWavHeader(this AudioInfoWish audioInfo, Stream stream)
            => stream.WriteWavHeader(audioInfo);

        public static void WriteWavHeader(this AudioInfoWish audioInfo, string filePath)
            => filePath.WriteWavHeader(audioInfo);
    }

    public static class ReadAudioInfoExtensions
    {
        public static AudioInfoWish ReadAudioInfo(this string filePath)
            => filePath.ReadWavHeader().ToWish();

        public static AudioInfoWish ReadAudioInfo(this Stream stream)
            => stream.ReadWavHeader().ToWish();

        public static AudioInfoWish ReadAudioInfo(this BinaryReader reader)
            => reader.ReadWavHeader().ToWish();
    }

    public static class ReadWavHeaderExtensions
    { 
        public static WavHeaderStruct ReadWavHeader(this string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return ReadWavHeader(fileStream);
        }

        public static WavHeaderStruct ReadWavHeader(this Stream stream)
            => new BinaryReader(stream).ReadWavHeader();

        public static WavHeaderStruct ReadWavHeader(this BinaryReader reader)
            => reader.ReadStruct<WavHeaderStruct>();
    }

    public static class ToWavHeaderExtensions
    { 
        public static WavHeaderStruct ToWavHeader(this AudioInfoWish info)
            => WavHeaderManager.CreateWavHeaderStruct(info.FromWish());

        public static WavHeaderStruct ToWavHeader(this Sample sample)
            => sample.ToWish().ToWavHeader();

        public static WavHeaderStruct ToWavHeader(this AudioFileOutput audioFileOutput, int frameCount)
            => audioFileOutput.ToWish(frameCount).ToWavHeader();
    }

    public static class ToAudioInfoWishExtensions
    {
        // Create Wish Version
        
        public static AudioInfoWish ToWish(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));

            return new AudioInfoWish
            {
                Bits = info.BytesPerValue * 8,
                Channels = info.ChannelCount,
                FrameCount = info.SampleCount,
                SamplingRate = info.SamplingRate
            };
        }

        public static AudioFileInfo FromWish(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new ArgumentNullException(nameof(infoWish));

            return new AudioFileInfo
            {
                BytesPerValue = infoWish.Bits / 8,
                ChannelCount = infoWish.Channels,
                SampleCount = infoWish.FrameCount,
                SamplingRate = infoWish.SamplingRate
            };
        }

        // From Loose Values
        
        public static AudioInfoWish ToWish
            <TSampleDataType>(SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = typeof(TSampleDataType).Bits(),
                Channels = speakerSetup.Channels(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioInfoWish ToWish
            <TSampleDataType>(int channels, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = typeof(TSampleDataType).Bits(),
                Channels = channels,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioInfoWish ToWish(
            Type sampleDataType, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = sampleDataType.Bits(),
                Channels = speakerSetup.Channels(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        
        public static AudioInfoWish ToWish(
            SampleDataTypeEnum sampleDataTypeEnum, int channels, int samplingRate, int frameCount)
            => new AudioInfoWish
            {
                Bits = sampleDataTypeEnum.Bits(),
                Channels = channels,
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };

        public static AudioInfoWish ToWish(
            Type sampleDataType, int channels, int samplingRate, int frameCount) 
            => new AudioInfoWish
        {
            Bits = sampleDataType.Bits(),
            Channels = channels,
            SamplingRate = samplingRate,
            FrameCount = frameCount
        };

        public static AudioInfoWish ToWish(
            SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetup, int samplingRate, int frameCount)
        {
            return new AudioInfoWish
            {
                Bits = sampleDataTypeEnum.Bits(),
                Channels = speakerSetup.Channels(),
                SamplingRate = samplingRate,
                FrameCount = frameCount
            };
        }

        // From Objects
        
        public static AudioInfoWish ToWish(this WavHeaderStruct wavHeader)
            => WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader).ToWish();

        public static AudioInfoWish ToWish(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new AudioInfoWish
            {
                Bits = entity.Bits(),
                Channels = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = entity.FrameCount()
            };
        }

        public static AudioInfoWish ToWish(this AudioFileOutput entity, int frameCount)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            var info = new AudioInfoWish
            {
                Bits = entity.Bits(),
                Channels = entity.GetChannelCount(),
                SamplingRate = entity.SamplingRate,
                FrameCount = frameCount
            };
            return info;
        }
    }
}