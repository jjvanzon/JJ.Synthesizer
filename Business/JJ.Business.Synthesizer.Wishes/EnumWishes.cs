using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Wishes.Helpers.PersistenceHelper;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
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

    /// <inheritdoc cref="docs._setenumwishes"/>
    public static class SetEnumWishes
    {
        // AudioFileOutput
        
        public static void SetAudioFormat(
            this AudioFileOutput entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            entity.SetAudioFileFormatEnum(enumValue, repository);
        }
        
        public static void SetBitDepth(
            this AudioFileOutput entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }

        // Node

        public static void SetNodeType(this Node entity, NodeTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<INodeTypeRepository>(context);
            entity.SetNodeTypeEnum(enumValue, repository);
        }

        // Sample

        public static void SetAudioFormat(this Sample entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            entity.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetInterpolation(this Sample entity, InterpolationTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            entity.SetInterpolationTypeEnum(enumValue, repository);
        }

        public static void SetBitDepth(this Sample entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSpeakerSetup(this Sample entity, SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            entity.SetSpeakerSetupEnum(enumValue, repository);
        }
    }

    /// <inheritdoc cref="docs._alternativeentrypointenumextensionwishes"/>
    public static class AlternativeEntryPointEnumExtensionWishes
    {
        // AudioFileOutputChannel.AudioFileFormat

        public static AudioFileFormat GetAudioFileFormatEnumEntity(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.AudioFileFormat;
        }

        public static AudioFileFormatEnum GetAudioFormat(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetAudioFileFormatEnum();
        }

        public static void SetAudioFileFormatEnumEntity(this AudioFileOutputChannel entity, AudioFileFormat enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            
            entity.AudioFileOutput.AudioFileFormat = enumEntity;
        }

        public static void SetAudioFormat(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetAudioFormat(
            this AudioFileOutputChannel entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetAudioFormat(enumValue, context);
        }
                
        // AudioFileOutputChannel.SampleDataType

        public static SampleDataType GetSampleDataTypeEnumEntity(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.SampleDataType;
        }

        public static SampleDataTypeEnum GetBitDepth(this AudioFileOutputChannel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            return entity.AudioFileOutput.GetSampleDataTypeEnum();
        }

        public static void SetSampleDataTypeEnumEntity(this AudioFileOutputChannel entity, SampleDataType enumEntity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SampleDataType = enumEntity;
        }

        public static void SetBitDepth(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetBitDepth(
            this AudioFileOutputChannel entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (entity.AudioFileOutput == null) throw new NullException(() => entity.AudioFileOutput);
            entity.AudioFileOutput.SetBitDepth(enumValue, context);
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
        
        public static SpeakerSetupEnum ToSpeakerSetup(this int channelCount)
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

        public static SampleDataTypeEnum GetBitDepth<TSampleDataType>()
        {
            if (typeof(TSampleDataType) == typeof(short)) return SampleDataTypeEnum.Int16;
            if (typeof(TSampleDataType) == typeof(byte)) return SampleDataTypeEnum.Byte;
            throw new ValueNotSupportedException(typeof(TSampleDataType));
        }

        public static int ToIndex(this ChannelEnum channel, IContext context = null)
        {
            IChannelRepository channelRepository = CreateRepository<IChannelRepository>(context);
            Channel channelEnumEntity = channelRepository.Get((int)channel);
            return channelEnumEntity.Index;
        }

        public static ChannelEnum ToChannel(this int channelIndex, SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    if (channelIndex == 0) return ChannelEnum.Single;
                    break;
                
                case SpeakerSetupEnum.Stereo:
                    if (channelIndex == 0) return ChannelEnum.Left;
                    if (channelIndex == 1) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channelIndex });
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

        /// <inheritdoc cref="docs._setnodetype"/>
        public static void SetNodeTypeEnumEntity(this Curve curve, NodeType nodeType)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            curve.Nodes.ForEach(x => x.NodeType = nodeType);
        }

        /// <inheritdoc cref="docs._setnodetype"/>
        public static void SetNodeType(this Curve curve, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            var nodeTypeRepository = CreateRepository<INodeTypeRepository>(context);
            curve.Nodes.ForEach(x => x.SetNodeTypeEnum(nodeTypeEnum, nodeTypeRepository));
        }

        /// <inheritdoc cref="docs._trygetnodetype"/>
        public static NodeType TryGetNodeTypeEnumEntity(this Curve curve)
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

        /// <inheritdoc cref="docs._trygetnodetype"/>
        public static NodeTypeEnum TryGetNodeType(this Curve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));

            IList<NodeTypeEnum> distinctNodeTypeEnums = curve.Nodes.Select(x => x.GetNodeTypeEnum()).Distinct().ToArray();

            if (distinctNodeTypeEnums.Count == 1) return distinctNodeTypeEnums[0];
            else return NodeTypeEnum.Undefined;
        }

        // Bits to 
        public static SampleDataTypeEnum ToBitDepth(this int bits)
        {
            switch (bits)
            {
                case 8: return SampleDataTypeEnum.Byte;
                case 16: return SampleDataTypeEnum.Int16;
                default: throw new Exception($"Bits = {bits} not supported.");
            }
        }
    }
}