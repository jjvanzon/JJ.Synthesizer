using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;

// ReSharper disable InvokeAsExtensionMethod
// ReSharper disable RedundantIfElseBlock

namespace JJ.Business.Synthesizer.Wishes
{
    public static class EnumExtensionWishes
    {
        // Enum Setters
        
        // AudioFileOutput
        
        public static void SetAudioFileFormatEnum(
            this AudioFileOutput entity, AudioFileFormatEnum enumValue, IContext context = null)
        {
            var repository = CreateRepository<IAudioFileFormatRepository>(context);
            entity.SetAudioFileFormatEnum(enumValue, repository);
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

        // To ID

        public static int ToID(this AudioFileFormatEnum enumValue) => (int)enumValue;
        public static int ToID(this InterpolationTypeEnum enumValue) => (int)enumValue;
        public static int ToID(this NodeTypeEnum enumValue) => (int)enumValue;
        
        // Special Enum Wishes

        // (Conversion from one thing to the other / side effects)
        
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
            => audioFileOutput.AudioFileOutputChannels.SingleOrDefault(x => x.Channel() == channelEnum.Channel());
        
        public static AudioFileOutputChannel GetAudioFileOutputChannel(
            this AudioFileOutput audioFileOutput, ChannelEnum channelEnum)
            => audioFileOutput.AudioFileOutputChannels.Single(x => x.Channel() == channelEnum.Channel());
        
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