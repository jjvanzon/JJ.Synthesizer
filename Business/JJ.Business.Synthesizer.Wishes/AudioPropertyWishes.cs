using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;

namespace JJ.Business.Synthesizer.Wishes
{
    // Derived Audio Properties
    
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

        public static int GetBits(this Type sampleDataType)
            => SizeOf(sampleDataType) * 8;

        public static int GetBits(this SampleDataTypeEnum enumValue)
            => enumValue.SizeOf() * 8;

        public static int GetBits(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue;

        public static int GetBits(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().Bits;
        }

        public static int GetBits(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.GetSampleDataTypeEnum());
        }

        public static int GetBits(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.GetSampleDataTypeEnum());
        }

        public static int GetFrameSize(WavHeaderStruct wavHeader)
        {
            return SizeOfBitDepth(wavHeader) * wavHeader.ChannelCount;
        }

        public static int GetFrameSize(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().GetFrameSize();
        }
        
        public static int GetFrameSize(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return SizeOfBitDepth(info) * info.Channels;
        }

        public static int GetFrameSize(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int GetFrameSize(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfBitDepth(entity) * entity.GetChannelCount();
        }

        public static int GetFrameCount(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Bytes == null) throw new ArgumentNullException(nameof(entity.Bytes));
            return entity.Bytes.Length - GetHeaderLength(entity) / GetFrameSize(entity);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileFormatEnum enumValue)
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
        public static string GetFileExtension(this AudioFileFormat enumEntity)
            => EnumFromEntityWishes.ToEnum(enumEntity).GetFileExtension();

        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        public static string GetFileExtension(this WavHeaderStruct wavHeader)
            => GetFileExtension(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }
        
        public static double GetNominalMax(this int bits) 
            => bits.ToSampleDataTypeEnum().GetNominalMax();
        
        public static double GetNominalMax(this SampleDataTypeEnum enumValue)
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
        
        public static double GetNominalMax(this WavHeaderStruct wavHeader) 
            => GetNominalMax(wavHeader.GetBits());
        
        public static double GetNominalMax(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().GetNominalMax();
        }
        
        public static double GetNominalMax(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return GetNominalMax(info.Bits);
        }

        public static double GetNominalMax(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetNominalMax(entity.GetSampleDataTypeEnum());
        }

        public static double GetNominalMax(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetNominalMax(entity.GetSampleDataTypeEnum());
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileFormatEnum enumValue)
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
        public static int GetHeaderLength(this AudioFileFormat enumEntity)
            => EnumFromEntityWishes.ToEnum(enumEntity).GetHeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int GetHeaderLength(this WavHeaderStruct wavHeader)
            => GetHeaderLength(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        public static int GetFileLengthNeeded(this AudioFileOutput entity, int extraBufferFrames)
        {
            // CourtesyBytes to accomodate a floating-point imprecision issue in the audio loop.
            // Testing revealed 1 courtesy frame was insufficient, and 2 resolved the issue.
            // Setting it to 4 frames as a safer margin to prevent errors in the future.
            int courtesyBytes = GetFrameSize(entity) * extraBufferFrames; 
            return GetHeaderLength(entity) +
                   GetFrameSize(entity) * (int)(entity.SamplingRate * entity.Duration) + courtesyBytes;
        }

        public static double GetAudioLength(this WavHeaderStruct wavHeader) 
            => wavHeader.GetAudioInfo().GetAudioLength();
        
        public static double GetAudioLength(this AudioFileInfo info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.ToWish().GetAudioLength();
        }

        public static double GetAudioLength(this AudioInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            if (info.FrameCount == 0) return 0;
            if (info.Channels == 0) throw new Exception("info.Channels == 0");
            if (info.SamplingRate == 0) throw new Exception("info.SamplingRate == 0");
            return (double)info.FrameCount / info.Channels / info.SamplingRate;
        }

        public static double GetAudioLength(this Sample sample)
        {
            if (sample == null) throw new ArgumentNullException(nameof(sample));
            return sample.GetDuration();
        }

        public static double GetAudioLength(this AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            return audioFileOutput.Duration;
        }
    }

    // Info Type

    /// <inheritdoc cref="docs._audioinfowish"/>
    public class AudioInfoWish
    {
        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }
}
