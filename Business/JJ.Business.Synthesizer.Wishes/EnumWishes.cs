using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    public static class EnumFromEntityWishes
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

    /// <inheritdoc cref="docs._setenumwishes"/>
    public static class EnumSetterWishes
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

        public static void SetNodeType(this Node entity, NodeTypeEnum enumValue, IContext context = null)
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
    }

    // To Int/ID

    public static class EnumToIDWishes
    {
        public static int ToID(this AudioFileFormatEnum enumValue) => (int)enumValue;
        public static int ToID(this ChannelEnum enumValue) => (int)enumValue;
        public static int ToID(this InterpolationTypeEnum enumValue) => (int)enumValue;
        public static int ToID(this SampleDataTypeEnum enumValue) => (int)enumValue;
        public static int ToID(this SpeakerSetupEnum enumValue) => (int)enumValue;
        public static int ToID(this NodeTypeEnum enumValue) => (int)enumValue;
    }

    // Specials (conversion from one thing to the other / side effects)

    public static class EnumSpecialWishes
    {
        // Bits
        
        public static void SetBits(this Sample entity, int bits, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.SetSampleDataTypeEnum(bits.ToSampleDataTypeEnum(), context);
        }
        
        public static void SetBits(this AudioFileOutput entity, int bits, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.SetSampleDataTypeEnum(bits.ToSampleDataTypeEnum(), context);
        }

        // Channels
       
        public static int GetChannels(this SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono: return 1;
                case SpeakerSetupEnum.Stereo: return 2;
                default: throw new ValueNotSupportedException(speakerSetupEnum);
            }
        }

        public static SpeakerSetupEnum ToSpeakerSetupEnum(this int? channels)
        {
            if (!Has(channels)) return SpeakerSetupEnum.Undefined;
            return channels.Value.ToSpeakerSetupEnum();
        }

        public static SpeakerSetupEnum ToSpeakerSetupEnum(this int channels)
        {
            switch (channels)
            {
                case 1: return SpeakerSetupEnum.Mono;
                case 2: return SpeakerSetupEnum.Stereo;
                default: throw new ValueNotSupportedException(channels);
            }
        }
        
        public static void SetChannels(this Sample entity, int? channels, IContext context = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (!Has(channels)) entity.SpeakerSetup = null;
            entity.SetChannels(channels.Value, context);
        }
        
        public static void SetChannels(this Sample entity, int channels, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            entity.SetSpeakerSetupEnum(channels.ToSpeakerSetupEnum(), repository);
        }

        // Channel
        
        public static int ToChannel(this ChannelEnum channelEnum, IContext context = null)
        {
            IChannelRepository channelRepository = CreateRepository<IChannelRepository>(context);
            Channel channelEnumEntity = channelRepository.Get((int)channelEnum);
            return channelEnumEntity.Index;
        }

        public static ChannelEnum ToChannelEnum(this int? channel, int channels)
            => ToChannelEnum(channel, channels.ToSpeakerSetupEnum());

        public static ChannelEnum ToChannelEnum(this int channel, int channels)
            => ToChannelEnum(channel, channels.ToSpeakerSetupEnum());

        public static ChannelEnum ToChannelEnum(this int? channel, SpeakerSetupEnum speakerSetupEnum)
            => channel.HasValue ? ToChannelEnum(channel.Value, speakerSetupEnum) : ChannelEnum.Undefined;

        public static ChannelEnum ToChannelEnum(this int channel, SpeakerSetupEnum speakerSetupEnum)
        {
            switch (speakerSetupEnum)
            {
                case SpeakerSetupEnum.Mono:
                    if (channel == 0) return ChannelEnum.Single;
                    break;
                
                case SpeakerSetupEnum.Stereo:
                    if (channel == 0) return ChannelEnum.Left;
                    if (channel == 1) return ChannelEnum.Right;
                    break;
            }
            
            throw new NotSupportedException(
                "Unsupported combination of values: " + new { speakerSetupEnum, channel });
        }

        
        // SampleDataType
       
        public static SampleDataTypeEnum ToSampleDataTypeEnum(this int bits)
        {
            switch (bits)
            {
                case 8: return SampleDataTypeEnum.Byte;
                case 16: return SampleDataTypeEnum.Int16;
                case 32: return SampleDataTypeEnum.Float32;
                default: throw new Exception($"Bits = {bits} not supported. Supported values: 8, 16, 32.");
            }
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum<TSampleDataType>()
        {
            if (typeof(TSampleDataType) == typeof(short)) return SampleDataTypeEnum.Int16;
            if (typeof(TSampleDataType) == typeof(byte)) return SampleDataTypeEnum.Byte;
            throw new ValueNotSupportedException(typeof(TSampleDataType));
        }

        // SpeakerSetupChannel by ChannelEnum

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
            this AudioFileOutput audioFileOutput, ChannelEnum channelEnum)
            => audioFileOutput.AudioFileOutputChannels.SingleOrDefault(x => x.Index == channelEnum.ToChannel());
        
        public static AudioFileOutputChannel GetAudioFileOutputChannel(
            this AudioFileOutput audioFileOutput, ChannelEnum channelEnum)
            => audioFileOutput.AudioFileOutputChannels.Single(x => x.Index == channelEnum.ToChannel());
        
        // SetNodeType for whole Curve

        /// <inheritdoc cref="docs._setnodetype"/>
        public static void SetNodeType(this Curve curve, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            var nodeTypeRepository = CreateRepository<INodeTypeRepository>(context);
            curve.Nodes.ForEach(x => x.SetNodeTypeEnum(nodeTypeEnum, nodeTypeRepository));
        }

        /// <inheritdoc cref="docs._trygetnodetype"/>
        public static NodeTypeEnum TryGetNodeType(this Curve curve)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));

            IList<NodeTypeEnum> distinctNodeTypeEnums = curve.Nodes.Select(x => x.GetNodeTypeEnum()).Distinct().ToArray();

            if (distinctNodeTypeEnums.Count == 1) return distinctNodeTypeEnums[0];
            else return NodeTypeEnum.Undefined;
        }
    }
}