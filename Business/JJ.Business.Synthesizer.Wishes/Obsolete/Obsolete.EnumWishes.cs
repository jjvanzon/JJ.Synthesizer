using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class ObsoleteEnumWishesMessages
    {
        public const string ObsoleteMessage = 
            "Direct use of enum-like entities is discouraged. Use for legacy purposes. " +
            "Prefer using integers or enums directly, like AudioFormat/AudioFileFormatEnum," +
            "integer Bits, and int instead of ChannelEnum. ";
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteEnumToEntityWishes
    {

        [Obsolete(ObsoleteMessage)]
        public static Channel ToEntity(this ChannelEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<IChannelRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static NodeType ToEntity(this NodeTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<INodeTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static SampleDataType ToEntity(this SampleDataTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup ToEntity(this SpeakerSetupEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            return repository.Get((int)enumValue);
        }
    }
    
    
    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteEntityToEnumWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static ChannelEnum ToEnum(this Channel enumEntity)
        {
            if (enumEntity == default) return default;
            return (ChannelEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)]
        public static NodeTypeEnum ToEnum(this NodeType enumEntity)
        {
            if (enumEntity == default) return default;
            return (NodeTypeEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum ToEnum(this SampleDataType enumEntity)
        {
            if (enumEntity == default) return default;
            return (SampleDataTypeEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupEnum ToEnum(this SpeakerSetup enumEntity)
        {
            if (enumEntity == default) return default;
            return (SpeakerSetupEnum)enumEntity.ID;
        }
    }

    /// <inheritdoc cref="docs._setenumwishes"/>
    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteEnumSetterWishes
    {
        // AudioFileOutput
        
        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(this AudioFileOutput entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }
        
        // Sample
        
        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(this Sample entity, SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            entity.SetSampleDataTypeEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSpeakerSetupEnum(this Sample entity, SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            entity.SetSpeakerSetupEnum(enumValue, repository);
        }
    }

    // To ID

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteEnumToIDWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static int ToID(this ChannelEnum enumValue) => (int)enumValue;
        [Obsolete(ObsoleteMessage)]
        public static int ToID(this SampleDataTypeEnum enumValue) => (int)enumValue;
        [Obsolete(ObsoleteMessage)]
        public static int ToID(this SpeakerSetupEnum enumValue) => (int)enumValue;
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteSpecialEnumWishes
    {
        // ToIndex for ChannelEnum

        // SpeakerSetupChannel by Channel

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupChannel TryGetSpeakerSetupChannel(
            this SpeakerSetup speakerSetup, Channel channel)
        {
            if (speakerSetup == null) throw new ArgumentNullException(nameof(speakerSetup));
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return speakerSetup.TryGetSpeakerSetupChannel(channel.ToEnum());
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupChannel GetSpeakerSetupChannel(
            this SpeakerSetup speakerSetup, Channel channel)
        {
            if (speakerSetup == null) throw new ArgumentNullException(nameof(speakerSetup));
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            return speakerSetup.GetSpeakerSetupChannel(channel.ToEnum());
        }

        // AudioFileOutputChannel by Channel

        [Obsolete(ObsoleteMessage)]
        public static AudioFileOutputChannel GetAudioFileOutputChannel(
            this AudioFileOutput parent, Channel channel)
        {
            AudioFileOutputChannel child = TryGetAudioFileOutputChannel(parent, channel);

            if (child == null)
            {
                throw new Exception($"{nameof(AudioFileOutputChannel)} not found for " +
                                    $"{nameof(channel)} '{channel.ToEnum()}'.");
            }

            return child;
        }

        [Obsolete(ObsoleteMessage)]
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
 
        // SetNodeType for whole Curve
    
        /// <inheritdoc cref="docs._setnodetype"/>
        public static void SetNodeTypeEnumEntity(this Curve curve, NodeType nodeType)
        {
            if (curve == null) throw new ArgumentNullException(nameof(curve));
            curve.Nodes.ForEach(x => x.NodeType = nodeType);
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
    }
}