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
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Wishes
{
    // Derived Audio Properties
    
    /// <inheritdoc cref="docs._audiopropertyextensionwishes"/>
    public static class AudioPropertyExtensionWishes
    {
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
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().SizeOfBitDepth();
        }

        public static int SizeOfBitDepth(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Bits * 8;
        }

        public static int SizeOfBitDepth(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SampleDataTypeHelper.SizeOf(entity.GetSampleDataTypeEnum());
        }

        public static int SizeOfBitDepth(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOf(entity.GetSampleDataTypeEnum());
        }

        public static int Bits(this Type sampleDataType)
            => SizeOf(sampleDataType) * 8;

        public static int Bits(this SampleDataTypeEnum enumValue)
            => enumValue.SizeOf() * 8;

        public static int Bits(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue;

        public static int Bits(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().Bits;
        }

        public static int Bits(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Bits(entity.GetSampleDataTypeEnum());
        }

        public static int Bits(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return Bits(entity.GetSampleDataTypeEnum());
        }

        public static int Channels(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetChannelCount();
        }

        public static int FrameSize(WavHeaderStruct wavHeader)
        {
            return SizeOfBitDepth(wavHeader) * wavHeader.ChannelCount;
        }

        public static int FrameSize(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().FrameSize();
        }
        
        public static int FrameSize(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return SizeOfBitDepth(info) * info.Channels;
        }

        public static int FrameSize(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int FrameSize(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int FrameCount(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Bytes == null) throw new ArgumentNullException(nameof(entity.Bytes));
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
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return FileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return FileExtension(entity.AudioFileFormat);
        }
        
        public static string FileExtension(this TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
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
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().MaxValue();
        }
        
        public static double MaxValue(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return MaxValue(info.Bits);
        }

        public static double MaxValue(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return MaxValue(entity.GetSampleDataTypeEnum());
        }

        public static double MaxValue(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
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
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().HeaderLength();
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
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

        public static double AudioLength(this WavHeaderStruct wavHeader) 
            => wavHeader.GetAudioInfo().AudioLength();
        
        public static double AudioLength(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().AudioLength();
        }

        public static double AudioLength(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (info.FrameCount == 0) return 0;
            if (info.Channels == 0) throw new Exception("info.Channels == 0");
            if (info.SamplingRate == 0) throw new Exception("info.SamplingRate == 0");
            return (double)info.FrameCount / info.Channels / info.SamplingRate;
        }

        public static double AudioLength(this Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return sample.GetDuration();
        }

        public static double AudioLength(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return audioFileOutput.Duration;
        }
        
        public static AudioFileFormatEnum AudioFormat(this Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return sample.GetAudioFileFormatEnum();
        }
        
        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return audioFileOutput.GetAudioFileFormatEnum();
        }

        public static AudioFileFormatEnum AudioFormat(this Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            return tape.Config.AudioFormat;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new ArgumentNullException(nameof(tapeConfig));
            return tapeConfig.AudioFormat;
        }
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
