using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

// ReSharper disable once PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes
{
    public static class AudioFileExtensionWishes
    {
        // Calculation

        public static double Calculate(this Sample sample, double time, Channel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return Calculate(sample, time, channel.Index);
        }

        public static double Calculate(this Sample sample, double time, ChannelEnum channelEnum)
            => Calculate(sample, time, channelEnum.GetChannelIndex());

        public static double Calculate(this Sample sample, double time, int channelIndex)
            => SampleCalculatorFactory.CreateSampleCalculator(sample).CalculateValue(channelIndex, time);

        // Validation

        public static Result Validate(this Sample sample)
            => new SampleValidator(sample).ToResult();

        public static Result Validate(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).ToResult();
        
        public static void Assert(this Sample sample)
            => new SampleValidator(sample).Verify();

        public static void Assert(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputValidator(audioFileOutput).Verify();

        public static IList<string> GetWarnings(this Sample sample)
            => new SampleWarningValidator(sample).ValidationMessages.Select(x => x.Text).ToList();

        public static IList<string> GetWarnings(this AudioFileOutput audioFileOutput)
            => new AudioFileOutputWarningValidator(audioFileOutput).ValidationMessages.Select(x => x.Text).ToList();

        // Conversions
        
        public static SpeakerSetupEnum GetSpeakerSetupEnum(this int channelCount)
        {
            switch (channelCount)
            {
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: throw new ValueNotSupportedException(channelCount);
            }
        }

        public static int GetChannelCount(this SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(speakerSetupEnum);
            }
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum<TSampleDataType>()
        {
            if (typeof(TSampleDataType) == typeof(short)) return SampleDataTypeEnum.Int16;
            if (typeof(TSampleDataType) == typeof(byte)) return SampleDataTypeEnum.Byte;
            throw new ValueNotSupportedException(typeof(TSampleDataType));
        }
        
        public static int GetChannelIndex(this ChannelEnum channelEnum, IContext context = null)
        {
            IChannelRepository channelRepository = CreateRepository<IChannelRepository>(context);
            Channel channel = channelRepository.Get((int)channelEnum);
            return channel.Index;
        }

        // Info
        
        public static int SizeOf(this SampleDataType enumEntity)
            => SampleDataTypeHelper.SizeOf(enumEntity);

        public static int SizeOf(this SampleDataTypeEnum enumValue)
            => SampleDataTypeHelper.SizeOf(enumValue);

        public static int GetFrameSize(this Sample sample)
            => sample.GetChannelCount() * sample.SampleDataType.SizeOf();

        public static string GetFileExtension(this AudioFileFormat enumEntity) 
            => ToEnum(enumEntity).GetFileExtension();
        
        /// <summary>
        /// Retrieves the file extension associated with the specified audio file format.
        /// </summary>
        /// <param name="enumValue">The audio file format enumeration value.</param>
        /// <returns>
        /// The file extension corresponding to the provided audio file format.
        /// A period (.) is included.
        /// </returns>
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
 
        public static double GetMaxAmplitude(this SampleDataType enumEntity) 
            => ToEnum(enumEntity).GetMaxAmplitude();

        public static double GetMaxAmplitude(this SampleDataTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SampleDataTypeEnum.Int16: return Int16.MaxValue;
                case SampleDataTypeEnum.Byte:  return Byte.MaxValue / 2;
                default:                 
                    throw new ValueNotSupportedException(enumValue);
            }
        }
 
        /// <returns>Length of a file header in bytes.</returns>
        public static int GetHeaderLength(this AudioFileFormat enumEntity) 
            => ToEnum(enumEntity).GetHeaderLength();

        /// <returns>Length of a file header in bytes.</returns>
        public static int GetHeaderLength(this AudioFileFormatEnum enumValue)
        {
            switch (enumValue)
            {
                case AudioFileFormatEnum.Wav: return WavHeaderConstants.WAV_HEADER_LENGTH;
                case AudioFileFormatEnum.Raw: return 0;
                default:
                    throw new ValueNotSupportedException(enumValue);
            }
        }

        // Enums
        
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

        // Helpers
        
        /// <summary>
        /// Creates a new repository, of the given interface type TInterface.
        /// If the context isn't provided, a brand new one is created, based on the settings from the config file.
        /// Depending on the use-case, creating a new context like that each time can be problematic.
        /// </summary>
        private static TInterface CreateRepository<TInterface>(IContext context = null) 
            => PersistenceHelper.CreateRepository<TInterface>(context ?? PersistenceHelper.CreateContext());
    }
}