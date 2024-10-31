// Moved

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.Samples;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.docs;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable once PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes
{
    public static class AudioFileExtensionWishes
    {

        // Derived Fields

        public static int SizeOf(Type sampleDataType)
        {
            if (sampleDataType == typeof(Byte)) return 1;
            if (sampleDataType == typeof(Int16)) return 2;
            throw new ValueNotSupportedException(sampleDataType);
        }

        public static int SizeOf(this SampleDataTypeEnum enumValue)
            => SampleDataTypeHelper.SizeOf(enumValue);

        public static int SizeOf(this SampleDataType enumEntity)
            => SampleDataTypeHelper.SizeOf(enumEntity);

        public static int SizeOfSampleDataType(this WavHeaderStruct wavHeader) 
            => wavHeader.BitsPerValue * 8;

        public static int SizeOfSampleDataType(this AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return info.Bits * 8;
        }

        public static int SizeOfSampleDataType(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOf(entity.SampleDataType);
        }
        
        public static int SizeOfSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SizeOfSampleDataType(wrapper.Sample);
        }

        public static int SizeOfSampleDataType(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOf(entity.SampleDataType);
        }

        public static int SizeOfSampleDataType(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity.AudioFileOutput);
        }

        public static int GetBits(this Type sampleDataType)
            => SizeOf(sampleDataType) * 8;

        public static int GetBits(this SampleDataTypeEnum enumValue)
            => enumValue.SizeOf() * 8;

        public static int GetBits(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return EntityToEnumWishes.ToEnum(enumEntity).GetBits();
        }

        public static int GetBits(this WavHeaderStruct wavHeader)
            => wavHeader.BitsPerValue;

        public static int GetBits(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.SampleDataType);
        }
        
        public static int GetBits(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetBits(wrapper.Sample);
        }

        public static int GetBits(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.SampleDataType);
        }

        public static int GetBits(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetBits(entity.AudioFileOutput);
        }

        public static int GetFrameSize(WavHeaderStruct wavHeader)
        {
            return SizeOfSampleDataType(wavHeader) * wavHeader.ChannelCount;
        }

        public static int GetFrameSize(AudioFileInfoWish info)
        {
            if (info == null) throw new ArgumentNullException(nameof(info));
            return SizeOfSampleDataType(info) * info.ChannelCount;
        }

        public static int GetFrameSize(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity) * entity.GetChannelCount();
        }
        
        public static int GetFrameSize(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameSize(wrapper.Sample);
        }

        public static int GetFrameSize(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return SizeOfSampleDataType(entity) * entity.GetChannelCount();
        }

        public static int GetFrameSize(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFrameSize(entity.AudioFileOutput);
        }

        public static int GetFrameCount(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Bytes == null) throw new ArgumentNullException(nameof(entity.Bytes));
            return entity.Bytes.Length - GetHeaderLength(entity) / GetFrameSize(entity);
        }
        
        public static int GetFrameCount(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameCount(wrapper.Sample);
        }

        /// <inheritdoc cref="_fileextension"/>
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

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileFormat enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetFileExtension();

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this WavHeaderStruct wavHeader)
            => GetFileExtension(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFileExtension(wrapper.Sample);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileOutput);
        }

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

        public static double GetMaxAmplitude(this SampleDataType enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetMaxAmplitude();

        public static double GetMaxAmplitude(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.SampleDataType);
        }
        
        public static double GetMaxAmplitude(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetMaxAmplitude(wrapper.Sample);
        }

        public static double GetMaxAmplitude(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.SampleDataType);
        }

        public static double GetMaxAmplitude(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetMaxAmplitude(entity.AudioFileOutput);
        }

        /// <inheritdoc cref="_headerlength"/>
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

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileFormat enumEntity)
            => EntityToEnumWishes.ToEnum(enumEntity).GetHeaderLength();

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this WavHeaderStruct wavHeader)
            => GetHeaderLength(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetHeaderLength(wrapper.Sample);
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetHeaderLength(entity.AudioFileOutput);
        }
    }
}