using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
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
            "Direct use of enum-like entities is discourage. " +
            "Use the enums or integers instead, like AudioFormat/AudioFileFormatEnum and int Bits," +
            "and an integer instead of using ChannelEnum.";
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteEnumToEntityWishes
    {
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToEntity(this AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static Channel ToEntity(this ChannelEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IChannelRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static NodeType ToEntity(this NodeTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<INodeTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static SampleDataType ToEntity(this SampleDataTypeEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISampleDataTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup ToEntity(this SpeakerSetupEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<ISpeakerSetupRepository>(context);
            return repository.Get((int)enumValue);
        }
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteAudioFileExtensionWishes 
    { 
        [Obsolete(ObsoleteMessage)]
        public static int SizeOf(this SampleDataType enumEntity)
            => SampleDataTypeHelper.SizeOf(enumEntity);

        [Obsolete(ObsoleteMessage)]
        public static int GetBits(this SampleDataType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return EnumFromEntityWishes.ToEnum(enumEntity).Bits();
        }
    
        public static double GetNominalMax(this SampleDataType enumEntity)
            => EnumFromEntityWishes.ToEnum(enumEntity).MaxValue();
    }

    [Obsolete(ObsoleteMessage)]
    public static class ObsoleteSpecialEnumWishes
    {
        // ToIndex for ChannelEnum
                                
        [Obsolete(ObsoleteMessage)]
        public static int ToIndex(this ChannelEnum channelEnum)
        {
            switch (channelEnum)
            {
                case ChannelEnum.Single: return 0;
                case ChannelEnum.Left: return 0;
                case ChannelEnum.Right: return 1;
                default: throw new ArgumentOutOfRangeException(nameof(channelEnum), channelEnum, null);
            }
        }

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