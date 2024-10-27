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
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable once PossibleLossOfFraction

namespace JJ.Business.Synthesizer.Wishes
{
    public static class AudioFileExtensionWishes
    {
        // Name

        public static Sample WithName(this Sample entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        public static AudioFileOutput WithName(this AudioFileOutput entity, string name)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.Name = name;
            return entity;
        }

        public static SampleOperatorWrapper WithName(this SampleOperatorWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");
            
            wrapper.Operator().WithName(name);
            wrapper.Sample.WithName(name);
            
            return wrapper;
        }

        // Calculation

        public static double Calculate(this Sample sample, double time, Channel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return Calculate(sample, time, channel.Index);
        }

        public static double Calculate(this Sample sample, double time, ChannelEnum channelEnum)
            => Calculate(sample, time, channelEnum.ToIndex());

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
                
        // Is / As
        
        public static bool IsSample(this Outlet entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(SampleOperator));

        public static bool IsSample(this Operator entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(SampleOperator));

        public static bool IsSample(this Inlet entity) 
            => OperatorExtensionsWishes.HasOperatorTypeName(entity, nameof(SampleOperator));

        internal static Sample GetSample(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSample(entity.Input);
        }

        public static Sample GetSample(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSample(entity.Operator);
        }

        public static Sample GetSample(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AsSampleOperator == null) throw new NullException(() => entity.AsSampleOperator);
            return entity.AsSampleOperator.Sample;
        }

        public static SampleOperatorWrapper GetSampleWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Input);
        }

        public static SampleOperatorWrapper GetSampleWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Operator);
        }

        public static SampleOperatorWrapper GetSampleWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new SampleOperatorWrapper(entity.AsSampleOperator);
        }

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
            => EntityToEnumWishes.ToEnum(enumEntity).GetFileExtension();

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this WavHeaderStruct wavHeader)
            => GetFileExtension(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFileExtension(wrapper.Sample);
        }

        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetFileExtension(entity.AudioFileFormat);
        }

        /// <inheritdoc cref="docs._fileextension"/>
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
            => EntityToEnumWishes.ToEnum(enumEntity).GetHeaderLength();

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this WavHeaderStruct wavHeader)
            => GetHeaderLength(AudioFileFormatEnum.Wav);

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this Sample entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }


        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetHeaderLength(wrapper.Sample);
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileOutput entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return entity.GetAudioFileFormatEnum().GetHeaderLength();
        }

        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetHeaderLength(entity.AudioFileOutput);
        }
        
        //// Setters with Side-Effects
        
        //// AudioFileOutputChannel.GetSpeakerSetup

        //public static SpeakerSetup GetSpeakerSetup(this AudioFileOutputChannel entity)
        //{
        //    if (entity == null) throw new ArgumentNullException(nameof(entity));
        //    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
        //    return entity.AudioFileOutput.SpeakerSetup;
        //}

        //public static SpeakerSetupEnum GetSpeakerSetupEnum(this AudioFileOutputChannel entity)
        //{
        //    if (entity == null) throw new ArgumentNullException(nameof(entity));
        //    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
        //    return entity.AudioFileOutput.GetSpeakerSetupEnum();
        //}

        // AudioFileOutput.SetSpeakerSetup_WithSideEffects

        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, SpeakerSetup speakerSetup, IContext context = null)
        //{
        //    CreateAudioFileOutputManager(context ?? CreateContext()).SetSpeakerSetup(audioFileOutput, speakerSetup);
        //}

        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, SpeakerSetupEnum speakerSetupEnum, IContext context = null)
        //{
        //    CreateAudioFileOutputManager(context ?? CreateContext()).SetSpeakerSetup(audioFileOutput, speakerSetupEnum);
        //}
        
        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutput audioFileOutput, int channelCount, IContext context = null)
        //    => SetSpeakerSetup_WithSideEffects(audioFileOutput, channelCount.ToSpeakerSetupEnum(), context);
        
        // Alternative Entry-Point AudioFileOutputChannel
        
        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetup_WithSideEffects(this AudioFileOutputChannel entity, SpeakerSetup speakerSetup, IContext context = null)
        //{
        //    if (entity == null) throw new ArgumentNullException(nameof(entity));
        //    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
        //    entity.AudioFileOutput.SetSpeakerSetup_WithSideEffects(speakerSetup, context);
        //}

        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetupEnum_WithSideEffects(this AudioFileOutputChannel entity, SpeakerSetupEnum speakerSetupEnum, IContext context = null)
        //{
        //    if (entity == null) throw new ArgumentNullException(nameof(entity));
        //    if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
        //    entity.AudioFileOutput.SetSpeakerSetup_WithSideEffects(speakerSetupEnum, context);
        //}

        ///// <inheritdoc cref="docs._setspeakersetup_withsideeffects"/>
        //public static void SetSpeakerSetupEnum_WithSideEffects(this AudioFileOutputChannel entity, int channelCount, IContext context = null)
        //    => SetSpeakerSetupEnum_WithSideEffects(entity, channelCount.ToSpeakerSetupEnum(), context);
        
        // AudioFileOutputChannel.GetSpeakerSetupChannel

        //public static SpeakerSetupChannel GetSpeakerSetupChannel(this AudioFileOutputChannel audioFileOutputChannel)
        //{
        //    IList<SpeakerSetupChannel> speakerSetupChannels =
        //        audioFileOutputChannel.GetSpeakerSetup()
        //                              .SpeakerSetupChannels;
            
        //    SpeakerSetupChannel speakerSetupChannel =
        //        speakerSetupChannels.Single(x => x.Index == audioFileOutputChannel.Index);

        //    speakerSetupChannel.Channel.SpeakerSetupChannels = speakerSetupChannels;

        //    return speakerSetupChannel;
        //}
    }
}