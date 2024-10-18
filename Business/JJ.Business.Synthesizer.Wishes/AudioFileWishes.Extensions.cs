using System;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

// ReSharper disable once PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes
{
    /// <summary> I wish these things were in JJ.Synthesizer. </summary>
    public static class AudioConversionExtensionWishes
    {
        public static int GetChannelCount(this SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(speakerSetupEnum);
            }
        }

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this int channelCount)
        {
            switch (channelCount)
            {
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: throw new ValueNotSupportedException(channelCount);
            }
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum<TSampleDataType>()
        {
            if (typeof(TSampleDataType) == typeof(short)) return SampleDataTypeEnum.Int16;
            if (typeof(TSampleDataType) == typeof(byte)) return SampleDataTypeEnum.Byte;
            throw new ValueNotSupportedException(typeof(TSampleDataType));
        }
 
        public static int SizeOf(this SampleDataType sampleDataType)
            => SampleDataTypeHelper.SizeOf(sampleDataType);

        public static int SizeOf(this SampleDataTypeEnum sampleDataTypeEnum)
            => SampleDataTypeHelper.SizeOf(sampleDataTypeEnum);

        public static int GetFrameSize(this Sample sample)
            => sample.GetChannelCount() * sample.SampleDataType.SizeOf();

        public static string GetFileExtension(this AudioFileFormat entity) 
            => ToEnum(entity).GetFileExtension();
        
        /// <summary>
        /// Retrieves the file extension associated with the specified audio file format.
        /// </summary>
        /// <param name="audioFileFormatEnum">The audio file format enumeration value.</param>
        /// <returns>
        /// The file extension corresponding to the provided audio file format.
        /// A period (.) is included.
        /// </returns>
        public static string GetFileExtension(this AudioFileFormatEnum audioFileFormatEnum)
        {
            switch (audioFileFormatEnum)
            {
                case AudioFileFormatEnum.Wav: return ".wav";
                case AudioFileFormatEnum.Raw: return ".raw";
                default:
                    throw new ValueNotSupportedException(audioFileFormatEnum);
            }
        }
 
        public static double GetMaxAmplitude(this SampleDataType entity) 
            => ToEnum(entity).GetMaxAmplitude();

        public static double GetMaxAmplitude(this SampleDataTypeEnum sampleDataTypeEnum)
        {
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16: return Int16.MaxValue;
                case SampleDataTypeEnum.Byte:  return Byte.MaxValue / 2;
                default:                 
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
 
        /// <returns>Length of a file header in bytes.</returns>
        public static int GetHeaderLength(this AudioFileFormat entity) 
            => ToEnum(entity).GetHeaderLength();

        /// <returns>Length of a file header in bytes.</returns>
        public static int GetHeaderLength(this AudioFileFormatEnum audioFileFormatEnum)
        {
            switch (audioFileFormatEnum)
            {
                case AudioFileFormatEnum.Wav: return WavHeaderConstants.WAV_HEADER_LENGTH;
                case AudioFileFormatEnum.Raw: return 0;
                default:
                    throw new ValueNotSupportedException(audioFileFormatEnum);
            }
        }
 
        public static SampleDataTypeEnum ToEnum(this SampleDataType entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (SampleDataTypeEnum)entity.ID;
        }

        public static AudioFileFormatEnum ToEnum(this AudioFileFormat entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (AudioFileFormatEnum)entity.ID;
        }

        public static SpeakerSetupEnum ToEnum(this SpeakerSetup entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return (SpeakerSetupEnum)entity.ID;
        }
    }
}