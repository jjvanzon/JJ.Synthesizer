using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.IO;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.WavHeaderWishes;

#pragma warning disable CS0618
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class WavHeaderWishes
    {
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
    }
    
    public static class ToWishExtensions
    {
        public static AudioInfoWish ToWish(this SynthWishes entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this FlowNode entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        internal static AudioInfoWish ToWish(this ConfigResolver entity, SynthWishes synthWishes) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount(synthWishes)
        };

        internal static AudioInfoWish ToWish(this ConfigSection entity) => new AudioInfoWish
        {
            Bits         = entity.Bits        ().CoalesceBits        (),
            Channels     = entity.Channels    ().CoalesceChannels    (),
            SamplingRate = entity.SamplingRate().CoalesceSamplingRate(),
            FrameCount   = entity.FrameCount  ().CoalesceFrameCount  ()
        };

        public static AudioInfoWish ToWish(this Tape entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeConfig entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeActions entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };

        public static AudioInfoWish ToWish(this TapeAction entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };
                
        public static AudioInfoWish ToWish(this Buff entity, int frameCount) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = frameCount.AssertFrameCount()
        };
                
        public static AudioInfoWish ToWish(this AudioFileOutput entity, int frameCount) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = frameCount.AssertFrameCount()
        };
        
        public static AudioInfoWish ToWish(this Sample entity) => new AudioInfoWish
        {
            Bits         = entity.Bits(),
            Channels     = entity.Channels(),
            SamplingRate = entity.SamplingRate(),
            FrameCount   = entity.FrameCount()
        };
        
        public static AudioFileInfo FromWish(this AudioInfoWish wish) => new AudioFileInfo
        {
            BytesPerValue = wish.SizeOfBitDepth(),
            ChannelCount  = wish.Channels(),
            SampleCount   = wish.FrameCount(),
            SamplingRate  = wish.SamplingRate()
        };

        public static AudioInfoWish ToWish(this AudioFileInfo info) => new AudioInfoWish
        {
            Bits         = info.Bits(),
            Channels     = info.Channels(),
            FrameCount   = info.FrameCount(),
            SamplingRate = info.SamplingRate()
        };

        public static AudioInfoWish ToWish(this WavHeaderStruct wavHeader)
        {
            var info = WavHeaderManager.GetAudioFileInfoFromWavHeaderStruct(wavHeader);
            return info.ToWish();
        }
    }
        
    public static class ToWavHeaderExtensions
    {
        public static WavHeaderStruct ToWavHeader(this SynthWishes entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this FlowNode entity)
            => entity.ToWish().ToWavHeader();
        
        internal static WavHeaderStruct ToWavHeader(this ConfigResolver entity, SynthWishes synthWishes)
            => entity.ToWish(synthWishes).ToWavHeader();
        
        internal static WavHeaderStruct ToWavHeader(this ConfigSection entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Tape entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeConfig entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeActions entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this TapeAction entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Buff entity, int frameCount)
            => entity.ToWish(frameCount).ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this AudioFileOutput entity, int frameCount)
            => entity.ToWish(frameCount).ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this Sample entity)
            => entity.ToWish().ToWavHeader();
        
        public static WavHeaderStruct ToWavHeader(this AudioFileInfo entity)
            => entity.ToWish().ToWavHeader();

        public static WavHeaderStruct ToWavHeader(this AudioInfoWish entity)
            => WavHeaderManager.CreateWavHeaderStruct(entity.FromWish());
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

    public static class ReadAudioInfoExtensions
    {
        public static AudioInfoWish ReadAudioInfo(this string filePath)
            => filePath.ReadWavHeader().ToWish();
        
        public static AudioInfoWish ReadAudioInfo(this Stream stream)
            => stream.ReadWavHeader().ToWish();
        
        public static AudioInfoWish ReadAudioInfo(this BinaryReader reader)
            => reader.ReadWavHeader().ToWish();
    }
    
    public static class WriteWavHeaderExtensions
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
    
        // From Objects
        
        public static void WriteWavHeader(this BinaryWriter writer, AudioFileOutput entity, int frameCount)
            => writer.WriteWavHeader(entity.ToWish(frameCount));
        
        public static void WriteWavHeader(this BinaryWriter writer, AudioInfoWish entity)
            => writer.WriteStruct(entity.ToWavHeader());
        
        public static void WriteWavHeader(this BinaryWriter writer, WavHeaderStruct entity)
            => writer.WriteStruct(entity);
        
        public static void WriteWavHeader(this Stream stream, AudioFileOutput entity, int frameCount)
            => new BinaryWriter(stream).WriteWavHeader(entity, frameCount);
        
        public static void WriteWavHeader(this Stream stream, AudioInfoWish entity)
            => new BinaryWriter(stream).WriteWavHeader(entity);
        
        public static void WriteWavHeader(this Stream stream, WavHeaderStruct entity)
            => new BinaryWriter(stream).WriteWavHeader(entity);
        
        public static void WriteWavHeader(this string filePath, AudioFileOutput entity, int frameCount)
            => filePath.WriteWavHeader(entity.ToWavHeader(frameCount));
        
        public static void WriteWavHeader(this string filePath, AudioInfoWish entity)
            => filePath.WriteWavHeader(entity.ToWavHeader());
        
        public static void WriteWavHeader(this string filePath, WavHeaderStruct entity)
        {
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                fileStream.WriteWavHeader(entity);
        }
        
        // Inverse Arguments
        
        public static void WriteWavHeader(this AudioFileOutput entity, BinaryWriter writer, int frameCount)
            => writer.WriteWavHeader(entity, frameCount);
        
        public static void WriteWavHeader(this AudioFileOutput entity, Stream stream, int frameCount)
            => stream.WriteWavHeader(entity, frameCount);
        
        public static void WriteWavHeader(this AudioFileOutput entity, string filePath, int frameCount)
            => filePath.WriteWavHeader(entity, frameCount);
        
        public static void WriteWavHeader(this AudioInfoWish entity, BinaryWriter writer)
            => writer.WriteWavHeader(entity);
        
        public static void WriteWavHeader(this AudioInfoWish entity, Stream stream)
            => stream.WriteWavHeader(entity);
        
        public static void WriteWavHeader(this AudioInfoWish entity, string filePath)
            => filePath.WriteWavHeader(entity);
    }
}