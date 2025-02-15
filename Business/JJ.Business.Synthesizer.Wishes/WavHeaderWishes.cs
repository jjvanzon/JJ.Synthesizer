using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.ToAudioInfoWishExtensions;

#pragma warning disable CS0618
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public static class WriteWavHeaderExtensions_FromValues
    {
        // With BinaryWriter
        
        public static void WriteWavHeader(
            this BinaryWriter writer, int bits, int channels, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(bits, channels, samplingRate, frameCount));
        
        public static void WriteWavHeader<TBits>(
            this BinaryWriter writer, int channels, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish<TBits>(channels, samplingRate, frameCount));
        
        public static void WriteWavHeader(
            this BinaryWriter writer, Type bitsType, int channels, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(bitsType, channels, samplingRate, frameCount));
        
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(bitsEnum, channelsEnum, samplingRate, frameCount));
        
        public static void WriteWavHeader(
            this BinaryWriter writer, SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount)
            => writer.WriteWavHeader(ToWish(bitsEntity, channelsEntity, samplingRate, frameCount));
        
        // With Stream
        
        public static void WriteWavHeader<TBits>(
            this Stream stream, int channels, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish<TBits>(channels, samplingRate, frameCount));
        
        public static void WriteWavHeader(
            this Stream stream,
            SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => stream.WriteWavHeader(ToWish(bitsEnum, channelsEnum, samplingRate, frameCount));
        
        // With FilePath
        
        public static void WriteWavHeader<TBits>(
            this string filePath, int channels, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(typeof(TBits), channels, samplingRate, frameCount));
        
        public static void WriteWavHeader(
            this string filePath,
            SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount)
            => filePath.WriteWavHeader(ToWish(bitsEnum, channelsEnum, samplingRate, frameCount));
    }
    
    public static class WriteWavHeader_FromObjects
    {
        public static void WriteWavHeader(this BinaryWriter writer, AudioFileOutput entity, int frameCount)
            => WriteWavHeader(writer, entity.ToWish(frameCount));
        
        public static void WriteWavHeader(this BinaryWriter writer, AudioInfoWish info)
            => writer.WriteStruct(info.ToWavHeader());
        
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
        // From Loose Values
        
        public static AudioInfoWish ToWish(int bits, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bits        .AssertBits        (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToWish<TBits>(int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = TypeToBits<TBits>(),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };

        public static AudioInfoWish ToWish(Type bitsType, int channels, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsType    .ToBits            (),
            Channels     = channels    .AssertChannels    (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToWish(SampleDataTypeEnum bitsEnum, SpeakerSetupEnum channelsEnum, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEnum    .ToBits            (),
            Channels     = channelsEnum.ToChannels        (),
            SamplingRate = samplingRate.AssertSamplingRate(),
            FrameCount   = frameCount  .AssertFrameCount  ()
        };
        
        public static AudioInfoWish ToWish(SampleDataType bitsEntity, SpeakerSetup channelsEntity, int samplingRate, int frameCount) => new AudioInfoWish
        {
            Bits         = bitsEntity    .ToBits            (),
            Channels     = channelsEntity.ToChannels        (),
            SamplingRate = samplingRate  .AssertSamplingRate(),
            FrameCount   = frameCount    .AssertFrameCount  ()
        };
        
        // From Objects

        public static AudioInfoWish ToWish(this AudioFileInfo info) => new AudioInfoWish
        {
            Bits         = info.Bits(),
            Channels     = info.Channels(),
            FrameCount   = info.FrameCount(),
            SamplingRate = info.SamplingRate()
        };
        
        public static AudioFileInfo FromWish(this AudioInfoWish infoWish) => new AudioFileInfo
        {
            BytesPerValue = infoWish.SizeOfBitDepth(),
            ChannelCount  = infoWish.Channels(),
            SampleCount   = infoWish.FrameCount(),
            SamplingRate  = infoWish.SamplingRate()
        };

        public static AudioInfoWish ToWish(this WavHeaderStruct wavHeader)
            => WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader).ToWish();
        
        public static AudioInfoWish ToWish(this Sample entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };
        
        public static AudioInfoWish ToWish(this AudioFileOutput entity, int frameCount) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = frameCount.AssertFrameCount()
        };
    }
}