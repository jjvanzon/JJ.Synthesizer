using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.PersistenceHelper;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    public static class MissingChannelEnumExtensionWishes
    {
        public static ChannelEnum GetChannelEnum(this SpeakerSetupChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Channel == null) return ChannelEnum.Undefined;
            return (ChannelEnum)entity.Channel.ID;
        }

        public static void SetChannelEnum(
            this SpeakerSetupChannel entity, ChannelEnum channelEnum, IChannelRepository channelRepository)
        {
            if (channelRepository == null) throw new NullException(() => channelRepository);
            entity.Channel = channelRepository.Get((int)channelEnum);
        }

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this SpeakerSetupChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.SpeakerSetup == null) return SpeakerSetupEnum.Undefined;
            return (SpeakerSetupEnum)entity.SpeakerSetup.ID;
        }

        public static SpeakerSetupEnum SetSpeakerSetupEnum(
            this SpeakerSetupChannel entity, 
            SpeakerSetupEnum speakerSetupEnum, 
            ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.SpeakerSetup = speakerSetupRepository.GetWithRelatedEntities((int)speakerSetupEnum);
            return (SpeakerSetupEnum)entity.SpeakerSetup.ID;
        }
    }

    public static class EntityToEnumWishes
    {
        public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }

        public static ChannelEnum ToEnum(this Channel enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (ChannelEnum)enumEntity.ID;
        }

        public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (InterpolationTypeEnum)enumEntity.ID;
        }

        public static NodeTypeEnum ToEnum(this NodeType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (NodeTypeEnum)enumEntity.ID;
        }

        public static SampleDataTypeEnum ToEnum(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (SampleDataTypeEnum)enumEntity.ID;
        }

        public static SpeakerSetupEnum ToEnum(this SpeakerSetup enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (SpeakerSetupEnum)enumEntity.ID;
        }
    }

    public static class EnumToEntityWishes
    {
        public static AudioFileFormat ToEntity(this AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            return repository.Get((int)enumValue);
        }

        public static Channel ToEntity(this ChannelEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IChannelRepository>(context);
            return repository.Get((int)enumValue);
        }

        public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        public static NodeType ToEntity(this NodeTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<INodeTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        public static SampleDataType ToEntity(this SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        public static SpeakerSetup ToEntity(this SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            return repository.Get((int)enumValue);
        }
    }

    /// <summary> With optional Context. </summary>
    public static class SetEnumWishes
    {
        // AudioFileOutput
        
        public static void SetAudioFileFormatEnum(
            this AudioFileOutput entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            entity.SetAudioFileFormatEnum(enumValue, repository);
        }
        
        public static void SetSampleDataTypeEnum(
            this AudioFileOutput entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }

        // Node

        public static void SetNodeTypeEnum(this Node entity, NodeTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<INodeTypeRepository>(context);
            entity.SetNodeTypeEnum(enumValue, repository);
        }

        // Sample

        public static void SetAudioFileFormatEnum(this Sample entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            entity.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetInterpolationTypeEnum(this Sample entity, InterpolationTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            entity.SetInterpolationTypeEnum(enumValue, repository);
        }

        public static void SetSampleDataTypeEnum(this Sample entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSpeakerSetupEnum(this Sample entity, SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            entity.SetSpeakerSetupEnum(enumValue, repository);
        }

        // SpeakerSetupChannel

        public static void SetChannelEnum(
            this SpeakerSetupChannel entity, ChannelEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IChannelRepository>(context);
            entity.SetChannelEnum(enumValue, repository);
        }

        public static void SetSpeakerSetupEnum(
            this SpeakerSetupChannel entity, SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            entity.SetSpeakerSetupEnum(enumValue, repository);
        }
    }

    /// <summary> Additional entity entry-points for enum-related extension. </summary>
    public static class AlternativeEntryPointEnumExtensionWishes
    {
        // AudioFileOutputChannel.AudioFileFormat

        public static AudioFileFormat GetAudioFileFormat(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.AudioFileFormat;
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetAudioFileFormatEnum();
        }

        public static void SetAudioFileFormat(this AudioFileOutputChannel entity, AudioFileFormat enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            
            entity.AudioFileOutput.AudioFileFormat = enumEntity;
        }

        public static void SetAudioFileFormatEnum(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetAudioFileFormatEnum(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetAudioFileFormatEnum(enumValue, context);
        }
                
        // AudioFileOutputChannel.SampleDataType

        public static SampleDataType GetSampleDataType(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.SampleDataType;
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetSampleDataTypeEnum();
        }

        public static void SetSampleDataType(this AudioFileOutputChannel entity, SampleDataType enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SampleDataType = enumEntity;
        }

        public static void SetSampleDataTypeEnum(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSampleDataTypeEnum(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperator.AudioFileFormat

        public static AudioFileFormat GetAudioFileFormat(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.AudioFileFormat;
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.GetAudioFileFormatEnum();
        }

        public static void SetAudioFileFormat(
            this SampleOperator entity, AudioFileFormat enumEntity, IAudioFileFormatRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.AudioFileFormat = enumEntity;
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperator entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperator entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetAudioFileFormatEnum(enumValue, context);
        }

        // SampleOperator.InterpolationType

        public static InterpolationType GetInterpolationType(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.InterpolationType;
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.GetInterpolationTypeEnum();
        }

        public static void SetInterpolationType(this SampleOperator entity, InterpolationType enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.InterpolationType = enumEntity;
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperator entity, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetInterpolationTypeEnum(enumValue, repository);
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperator entity, InterpolationTypeEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetInterpolationTypeEnum(enumValue, context);
        }

        // SampleOperator.SpeakerSetup

        public static SpeakerSetup GetSpeakerSetup(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.SpeakerSetup;
        }

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.GetSpeakerSetupEnum();
        }
        
        public static void SetSpeakerSetup(this SampleOperator entity, SpeakerSetup enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SpeakerSetup = enumEntity;
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperator entity, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetSpeakerSetupEnum(enumValue, repository);
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperator entity, SpeakerSetupEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetSpeakerSetupEnum(enumValue, context);
        }
        
        // SampleOperator.SampleDataType

        public static SampleDataType GetSampleDataType(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.SampleDataType;
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this SampleOperator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            return entity.Sample.GetSampleDataTypeEnum();
        }

        public static void SetSampleDataType(
            this SampleOperator entity, SampleDataType enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SampleDataType = enumEntity;
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperator entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperator entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.Sample == null) throw new NullException(() => entity.Sample);
            entity.Sample.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.AudioFileFormat

        public static AudioFileFormat GetAudioFileFormat(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.AudioFileFormat;
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetAudioFileFormatEnum();
        }

        public static void SetAudioFileFormat(this SampleOperatorWrapper wrapper, AudioFileFormat enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.AudioFileFormat = enumEntity;
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, context);
        }

        // SampleOperatorWrapper.InterpolationType

        public static InterpolationType GetInterpolationType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.InterpolationType;
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetInterpolationTypeEnum();
        }

        public static void SetInterpolationType(this SampleOperatorWrapper wrapper, InterpolationType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.InterpolationType = enumEntity;
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, repository);
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, context);
        }
        
        // SampleOperatorWrapper.SampleDataType
        
        public static SampleDataType GetSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SampleDataType;
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSampleDataTypeEnum();
        }

        public static void SetSampleDataType(this SampleOperatorWrapper wrapper, SampleDataType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SampleDataType = enumEntity;
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SpeakerSetup

        public static SpeakerSetup GetSpeakerSetup(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SpeakerSetup;
        }

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSpeakerSetupEnum();
        }
        
        public static void SetSpeakerSetup(this SampleOperatorWrapper wrapper, SpeakerSetup enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SpeakerSetup = enumEntity;
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, repository);
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, context);
        }

        // CurveIn.NodeType
        
        public static NodeType TryGetNodeType(this CurveIn curveIn)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            return SpecialEnumWishes.TryGetNodeType(curveIn.Curve);
        }

        public static NodeTypeEnum TryGetNodeTypeEnum(this CurveIn curveIn)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            return SpecialEnumWishes.TryGetNodeTypeEnum(curveIn.Curve);
        }

        public static void SetNodeType(this CurveIn curveIn, NodeType nodeType)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            SpecialEnumWishes.SetNodeType(curveIn.Curve, nodeType);
        }

        public static void SetNodeTypeEnum(this CurveIn curveIn, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (curveIn == null) throw new ArgumentNullException(nameof(curveIn));
            SpecialEnumWishes.SetNodeTypeEnum(curveIn.Curve, nodeTypeEnum, context);
        }

        // CurveInWrapper.NodeType

        public static NodeType TryGetNodeType(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeType(wrapper.Curve);
        }

        public static NodeTypeEnum TryGetNodeTypeEnum(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeTypeEnum(wrapper.Curve);
        }
        
        public static void SetNodeType(this CurveInWrapper wrapper, NodeType nodeType)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeType(wrapper.Curve, nodeType);
        }

        public static void SetNodeTypeEnum(
            this CurveInWrapper wrapper, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeTypeEnum(wrapper.Curve, nodeTypeEnum, context);
        }
    }

    // To Int/ID

    public static class EnumToIDExtensionWishes
    {
        public static int ToID(this AudioFileFormatEnum enumValue) => (int)enumValue;
        public static int ToID(this ChannelEnum enumValue) => (int)enumValue;
        public static int ToID(this InterpolationTypeEnum enumValue) => (int)enumValue;
        public static int ToID(this SampleDataTypeEnum enumValue) => (int)enumValue;
        public static int ToID(this SpeakerSetupEnum enumValue) => (int)enumValue;
        public static int ToID(this NodeTypeEnum enumValue) => (int)enumValue;
    }

    // Specials (conversion from one thing to the other / side effects)

    public static class SpecialEnumWishes
    {
        // SpeakerSetup Value Conversions
        
        public static SpeakerSetupEnum ToSpeakerSetupEnum(this int channelCount)
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

        public static int ToIndex(this ChannelEnum channelEnum, IContext context = null)
        {
            IChannelRepository channelRepository = PersistenceHelper.CreateRepository<IChannelRepository>(context);
            Channel channel = channelRepository.Get((int)channelEnum);
            return channel.Index;
        }

        // SpeakerSetupChannel by ChannelEnum

        public static SpeakerSetupChannel TryGetSpeakerSetupChannel(
            this SpeakerSetup speakerSetup, Channel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return TryGetSpeakerSetupChannel(speakerSetup, channel.ToEnum());
        }

        public static SpeakerSetupChannel GetSpeakerSetupChannel(
            this SpeakerSetup speakerSetup, Channel channel)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return GetSpeakerSetupChannel(speakerSetup, channel.ToEnum());
        }

        public static SpeakerSetupChannel GetSpeakerSetupChannel(
            this SpeakerSetup parent, ChannelEnum channelEnum)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            SpeakerSetupChannel child = TryGetSpeakerSetupChannel(parent, channelEnum);

            if (child == null)
            {
                throw new Exception($"{nameof(SpeakerSetupChannel)} not found for "+
                                    $"{nameof(channelEnum)} '{channelEnum}'.");
            }

            return child;
        }
        
        public static SpeakerSetupChannel TryGetSpeakerSetupChannel(
            this SpeakerSetup parent, ChannelEnum channelEnum)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            var children = parent.SpeakerSetupChannels
                                 .Where(x => x.Channel?.ToEnum() == channelEnum)
                                 .ToArray();
            
            switch (children.Length)
            { 
                case 1: return children[0];
                case 0: return null;
                default:
                    throw new Exception($"Multiple {nameof(SpeakerSetupChannel)}s not found with " +
                                        $"{nameof(channelEnum)} '{channelEnum}'.");
            }
        }

        // AudioFileOutputChannel by ChannelEnum

        public static AudioFileOutputChannel TryGetAudioFileOutputChannel(
            this AudioFileOutput audioFileOutput, ChannelEnum channelEnum, IContext context = null)
            => TryGetAudioFileOutputChannel(audioFileOutput, channelEnum.ToEntity(context));
        
        public static AudioFileOutputChannel GetAudioFileOutputChannel(
            this AudioFileOutput audioFileOutput, ChannelEnum channelEnum, IContext context = null)
            => GetAudioFileOutputChannel(audioFileOutput, channelEnum.ToEntity(context));

        public static AudioFileOutputChannel GetAudioFileOutputChannel(
            this AudioFileOutput parent, Channel channel)
        {
            AudioFileOutputChannel child = TryGetAudioFileOutputChannel(parent, channel);

            if (child == null)
            {
                throw new Exception($"{nameof(AudioFileOutputChannel)} not found for "+
                                    $"{nameof(channel)} '{channel.ToEnum()}'.");
            }

            return child;
        }
        public static AudioFileOutputChannel TryGetAudioFileOutputChannel(
            this AudioFileOutput parent, Channel channel)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));
            if (channel == null) throw new ArgumentNullException(nameof(channel));

            var children = parent.AudioFileOutputChannels
                                 .Where(x => x.Index == channel.Index)
                                 .ToArray();
            
            switch (children.Length)
            { 
                case 1: return children[0];
                case 0: return null;
                default:
                    throw new Exception($"Multiple {nameof(AudioFileOutputChannel)}s not found with " +
                                        $"{nameof(channel)} '{channel.ToEnum()}'.");
            }
        }
        
        // SetNodeTypeEnum for whole Curve

        /// <summary>
        /// Sets the node type (the type of interpolation) for a curve as a whole.
        /// This sets the node type of all the curve nodes.
        /// </summary>
        public static void SetNodeType(this Curve curve, NodeType nodeType)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            curve.Nodes.ForEach(x => x.NodeType = nodeType);
        }

        public static void SetNodeTypeEnum(this Curve curve, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            var nodeTypeRepository = CreateRepository<INodeTypeRepository>(context);
            curve.Nodes.ForEach(x => x.SetNodeTypeEnum(nodeTypeEnum, nodeTypeRepository));
        }

        /// <summary>
        /// Gets the node type (the type of interpolation) for a curve as a whole.
        /// This only works if all (but the last) node are set to the same node type.
        /// Otherwise, NodeTypeEnum.Undefined is returned.
        /// </summary>
        public static NodeType TryGetNodeType(this Curve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));

            IList<NodeType> distinctNodeTypes = curve.Nodes.Select(x => x.NodeType)
                                                     .GroupBy(x => x.ID)
                                                     .Select(x => x.First())
                                                     .ToArray();
            if (distinctNodeTypes.Count == 1)
            {
                return distinctNodeTypes[0];
            }
            else
            {
                return null;
            }
        }

        public static NodeTypeEnum TryGetNodeTypeEnum(this Curve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));

            IList<NodeTypeEnum> distinctNodeTypeEnums = curve.Nodes.Select(x => x.GetNodeTypeEnum()).Distinct().ToArray();

            if (distinctNodeTypeEnums.Count == 1) return distinctNodeTypeEnums[0];
            else return NodeTypeEnum.Undefined;
        }
    }
}