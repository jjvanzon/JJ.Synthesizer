using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using System.Diagnostics;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._audiopropertywishes"/>
    public static class AudioPropertyExtensionWishes
    {
        // TODO: For all the object types
        // TODO: For all the enum(-like) types
        // TODO: Setters
        // TODO: Setters should return `this` for fluent chaining.
        // TODO: Shorthands like IsWav/IsRaw.
        // TODO: All the audio properties, even if they already exist as properties or otherwise.
        // TODO: Complete the conversions from enum to something else.

        public static int SizeOf(Type sampleDataType)
        {
            if (sampleDataType == typeof(Byte)) return 1;
            if (sampleDataType == typeof(Int16)) return 2;
            throw new ValueNotSupportedException(sampleDataType);
        }
        
        public static int SizeOf(this SampleDataTypeEnum enumValue)
            => SampleDataTypeHelper.SizeOf(enumValue);
        
        public static int SizeOfBitDepth(this int bits) => bits / 8;

        public static int SizeOfBitDepth(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue * 8;
        
        public static int SizeOfBitDepth(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().SizeOfBitDepth();
        }

        public static int SizeOfBitDepth(this AudioInfoWish info)
        {
            if (info == null) throw new NullException(() => info);
            return info.Bits * 8;
        }

        public static int SizeOfBitDepth(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return SampleDataTypeHelper.SizeOf(entity.GetSampleDataTypeEnum());
        }

        public static int SizeOfBitDepth(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return SizeOf(entity.GetSampleDataTypeEnum());
        }

        #region Bits
        
        public static int Bits(this Type sampleDataType)
            => SizeOf(sampleDataType) * 8;

        public static int Bits(this SampleDataTypeEnum enumValue)
            => enumValue.SizeOf() * 8;
        
        public static int Bits(this SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.GetBits;
        }

        public static SynthWishes Bits(this SynthWishes synthWishes, int bits)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.WithBits(bits);
            return synthWishes;
        }

        public static int Bits(this FlowNode flowNode)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return flowNode.GetBits;
        }

        public static FlowNode Bits(this FlowNode flowNode, int bits)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            flowNode.WithBits(bits);
            return flowNode;
        }

        public static int Bits(this ConfigWishes configWishes)
        {
            if (configWishes == null) throw new NullException(() => configWishes);
            return configWishes.GetBits;
        }

        public static ConfigWishes Bits(this ConfigWishes configWishes, int bits)
        {
            if (configWishes == null) throw new NullException(() => configWishes);
            configWishes.WithBits(bits);
            return configWishes;
        }

        internal static int Bits(this ConfigSection configSection)
        {
            if (configSection == null) throw new NullException(() => configSection);
            return configSection.Bits ?? default;
        }

        internal static ConfigSection Bits(this ConfigSection configSection, int bits)
        {
            if (configSection == null) throw new NullException(() => configSection);
            configSection.Bits = bits;
            return configSection;
        }

        public static int Bits(this Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            return Bits(buff.UnderlyingAudioFileOutput);
        }

        public static Buff Bits(this Buff buff, int bits)
        {
            if (buff == null) throw new NullException(() => buff);
            Bits(buff.UnderlyingAudioFileOutput, bits);
            return buff;
        }

        public static int Bits(this Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            return tape.Config.Bits;
        }

        public static Tape Bits(this Tape tape, int bits)
        {
            if (tape == null) throw new NullException(() => tape);
            tape.Config.Bits = bits;
            return tape;
        }

        public static int Bits(this TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.Bits;
        }

        public static TapeConfig Bits(this TapeConfig tapeConfig, int bits)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            tapeConfig.Bits = bits;
            return tapeConfig;
        }

        public static int Bits(this TapeAction tapeAction)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return tapeAction.Tape.Config.Bits;
        }

        public static TapeAction Bits(this TapeAction tapeAction, int bits)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            tapeAction.Tape.Config.Bits = bits;
            return tapeAction;
        }

        public static int Bits(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return Bits(entity.GetSampleDataTypeEnum());
        }
        
        public static Sample Bits(this Sample sample, int bits)
        {
            if (sample == null) throw new NullException(() => sample);
            sample.SetBits(bits);
            return sample;
        }
        
        public static int Bits(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            return Bits(audioFileOutput.GetSampleDataTypeEnum());
        }

        public static AudioFileOutput Bits(this AudioFileOutput audioFileOutput, int bits)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            audioFileOutput.SetBits(bits);
            return audioFileOutput;
        }

        public static int Bits(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue;

        public static WavHeaderStruct Bits(this WavHeaderStruct wavHeader, int bits) 
            => wavHeader.AudioInfoWish().Bits(bits).GetWavHeader();
        
        public static int Bits(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.BytesPerValue * 8;
        }

        public static AudioFileInfo Bits(this AudioFileInfo info, int bits)
        {
            if (info == null) throw new NullException(() => info);
            info.BytesPerValue = bits / 8;
            return info;
        }

        public static int Bits(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.Bits;
        }

        public static AudioInfoWish Bits(this AudioInfoWish infoWish, int bits)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.Bits = bits;
            return infoWish;
        }
        
        #endregion

        public static int Channels(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetChannelCount();
        }

        public static int FrameSize(WavHeaderStruct wavHeader)
        {
            return SizeOfBitDepth(wavHeader) * wavHeader.ChannelCount;
        }

        public static int FrameSize(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().FrameSize();
        }
        
        public static int FrameSize(this AudioInfoWish info)
        {
            if (info == null) throw new NullException(() => info);
            return SizeOfBitDepth(info) * info.Channels;
        }

        public static int FrameSize(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int FrameSize(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int FrameCount(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Bytes == null) throw new NullException(() => entity.Bytes);
            return entity.Bytes.Length - HeaderLength(entity) / FrameSize(entity);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum enumValue)
        {
            switch (enumValue)
            {
                case AudioFileFormatEnum.Wav: return ".wav";
                case AudioFileFormatEnum.Raw: return ".raw";
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormat enumEntity)
            => EnumFromEntityWishes.ToEnum(enumEntity).FileExtension();

        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        public static string FileExtension(this WavHeaderStruct wavHeader)
            => FileExtension(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return FileExtension(entity.AudioFileFormat);
        }
        
        public static string FileExtension(this TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.AudioFormat.FileExtension();
        }
        
        public static double MaxValue(this int bits) 
            => bits.ToSampleDataTypeEnum().MaxValue();
        
        public static double MaxValue(this SampleDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SampleDataTypeEnum.Float32: return 1;
                case SampleDataTypeEnum.Int16: return Int16.MaxValue;
                // ReSharper disable once PossibleLossOfFraction
                case SampleDataTypeEnum.Byte: return Byte.MaxValue / 2;
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }
        
        public static double MaxValue(this WavHeaderStruct wavHeader) 
            => MaxValue(wavHeader.Bits());
        
        public static double MaxValue(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().MaxValue();
        }
        
        public static double MaxValue(this AudioInfoWish info)
        {
            if (info == null) throw new NullException(() => info);
            return MaxValue(info.Bits);
        }

        public static double MaxValue(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return MaxValue(entity.GetSampleDataTypeEnum());
        }

        public static double MaxValue(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return MaxValue(entity.GetSampleDataTypeEnum());
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum enumValue)
        {
            switch (enumValue)
            {
                case AudioFileFormatEnum.Wav: return 44;
                case AudioFileFormatEnum.Raw: return 0;
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormat enumEntity)
            => EnumFromEntityWishes.ToEnum(enumEntity).HeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct wavHeader)
            => HeaderLength(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFileFormatEnum().HeaderLength();
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            return entity.GetAudioFileFormatEnum().HeaderLength();
        }

        public static int FileLengthNeeded(this AudioFileOutput entity, int courtesyFrames)
        {
            // CourtesyBytes to accomodate a floating-point imprecision issue in the audio loop.
            // Testing revealed 1 courtesy frame was insufficient, and 2 resolved the issue.
            // Setting it to 4 frames as a safer margin to prevent errors in the future.
            int courtesyBytes = FrameSize(entity) * courtesyFrames; 
            return HeaderLength(entity) +
                   FrameSize(entity) * (int)(entity.SamplingRate * entity.Duration) + courtesyBytes;
        }

        #region AudioLength
        
        public static double AudioLength(this WavHeaderStruct wavHeader) 
            => wavHeader.AudioInfoWish().AudioLength();
        
        public static double AudioLength(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().AudioLength();
        }

        public static double AudioLength(this AudioInfoWish info)
        {
            if (info == null) throw new NullException(() => info);
            if (info.FrameCount == 0) return 0;
            if (info.Channels == 0) throw new Exception("info.Channels == 0");
            if (info.SamplingRate == 0) throw new Exception("info.SamplingRate == 0");
            return (double)info.FrameCount / info.Channels / info.SamplingRate;
        }

        public static double AudioLength(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            return sample.GetDuration();
        }
        
        #endregion
        
        #region AudioFormat
        
        public static AudioFileFormatEnum AudioFormat(this SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.GetAudioFormat;
        }

        public static SynthWishes AudioFormat(this SynthWishes synthWishes, AudioFileFormatEnum audioFormat)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.WithAudioFormat(audioFormat);
            return synthWishes;
        }

        public static AudioFileFormatEnum AudioFormat(this FlowNode flowNode)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return flowNode.GetAudioFormat;
        }

        public static FlowNode AudioFormat(this FlowNode flowNode, AudioFileFormatEnum audioFormat)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            flowNode.WithAudioFormat(audioFormat);
            return flowNode;
        }

        public static AudioFileFormatEnum AudioFormat(this ConfigWishes configWishes)
        {
            if (configWishes == null) throw new NullException(() => configWishes);
            return configWishes.GetAudioFormat;
        }

        public static ConfigWishes AudioFormat(this ConfigWishes configWishes, AudioFileFormatEnum audioFormat)
        {
            if (configWishes == null) throw new NullException(() => configWishes);
            configWishes.WithAudioFormat(audioFormat);
            return configWishes;
        }

        internal static AudioFileFormatEnum AudioFormat(this ConfigSection configSection)
        {
            if (configSection == null) throw new NullException(() => configSection);
            return configSection.AudioFormat ?? default;
        }

        internal static ConfigSection AudioFormat(this ConfigSection configSection, AudioFileFormatEnum audioFormat)
        {
            if (configSection == null) throw new NullException(() => configSection);
            configSection.AudioFormat = audioFormat;
            return configSection;
        }

        public static AudioFileFormatEnum AudioFormat(this Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            return AudioFormat(buff.UnderlyingAudioFileOutput);
        }

        public static Buff AudioFormat(this Buff buff, AudioFileFormatEnum audioFormat, IContext context = null)
        {
            if (buff == null) throw new NullException(() => buff);
            AudioFormat(buff.UnderlyingAudioFileOutput, audioFormat, context);
            return buff;
        }

        public static AudioFileFormatEnum AudioFormat(this Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            return tape.Config.AudioFormat;
        }

        public static Tape AudioFormat(this Tape tape, AudioFileFormatEnum audioFormat)
        {
            if (tape == null) throw new NullException(() => tape);
            tape.Config.AudioFormat = audioFormat;
            return tape;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.AudioFormat;
        }

        public static TapeConfig AudioFormat(this TapeConfig tapeConfig, AudioFileFormatEnum audioFormat)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            tapeConfig.AudioFormat = audioFormat;
            return tapeConfig;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeAction tapeAction)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return tapeAction.Tape.Config.AudioFormat;
        }

        public static TapeAction AudioFormat(this TapeAction tapeAction, AudioFileFormatEnum audioFormat)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            tapeAction.Tape.Config.AudioFormat = audioFormat;
            return tapeAction;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeActions tapeActions)
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            return tapeActions.Tape.Config.AudioFormat;
        }

        public static TapeActions AudioFormat(this TapeActions tapeActions, AudioFileFormatEnum audioFormat)
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            tapeActions.Tape.Config.AudioFormat = audioFormat;
            return tapeActions;
        }

        public static AudioFileFormatEnum AudioFormat(this Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            return sample.GetAudioFileFormatEnum();
        }

        public static Sample AudioFormat(this Sample sample, AudioFileFormatEnum audioFormat, IContext context)
        {
            if (sample == null) throw new NullException(() => sample);
            sample.SetAudioFileFormatEnum(audioFormat, context);
            return sample;
        }

        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            return audioFileOutput.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput AudioFormat(this AudioFileOutput audioFileOutput, AudioFileFormatEnum audioFormat, IContext context)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            audioFileOutput.SetAudioFileFormatEnum(audioFormat, context);
            return audioFileOutput;
        }
    
        #endregion
    }

    // Info Type

    /// <inheritdoc cref="docs._audioinfowish"/>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class AudioInfoWish
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);

        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }
}
