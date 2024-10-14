using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
// ReSharper disable once PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    /// <summary> I wish these things were in JJ.Synthesizer </summary>
    public static class AudioConversionExtensions
    {
        public static int ToChannelCount(this SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(speakerSetupEnum);
            }
        }

        public static SpeakerSetupEnum ToSpeakerSetupEnum(this int channelCount)
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
    }
}