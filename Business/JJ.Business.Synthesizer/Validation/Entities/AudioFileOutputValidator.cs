﻿using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class AudioFileOutputValidator : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            AudioFileOutput audioFileOutput = Object;

            For(() => audioFileOutput.TimeMultiplier, PropertyDisplayNames.TimeMultiplier).IsNot(0);
            For(() => audioFileOutput.Duration, PropertyDisplayNames.Duration).Above(0.0);
            For(() => audioFileOutput.SamplingRate, PropertyDisplayNames.SamplingRate).Above(0);

            For(() => audioFileOutput.AudioFileFormat, PropertyDisplayNames.AudioFileFormat).NotNull();
            For(() => audioFileOutput.SampleDataType, PropertyDisplayNames.SampleDataType).NotNull();
            For(() => audioFileOutput.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();

            if (audioFileOutput.AudioFileFormat != null)
            {
                For(() => audioFileOutput.AudioFileFormat.ID, PropertyDisplayNames.AudioFileFormat)
                    .IsEnumValue<AudioFileFormatEnum>()
                    .IsNot(AudioFileFormatEnum.Undefined);
            }

            if (audioFileOutput.SampleDataType != null)
            {
                For(() => audioFileOutput.SampleDataType.ID, PropertyDisplayNames.SampleDataType)
                    .IsEnumValue<SampleDataTypeEnum>()
                    .IsNot(SampleDataTypeEnum.Undefined);
            }

            if (audioFileOutput.SpeakerSetup != null)
            {
                if (audioFileOutput.AudioFileOutputChannels.Count != audioFileOutput.SpeakerSetup.SpeakerSetupChannels.Count)
                {
                    ValidationMessages.Add(() => audioFileOutput.AudioFileOutputChannels.Count, MessagesFormatter.ChannelCountDoesNotMatchSpeakerSetup());
                }

                // Omit AudioFileOutputChannel.Index validation, for Mono Left Channel (=1) to work.
                return;

                IList<AudioFileOutputChannel> sortedAudioFileOutputChannels = audioFileOutput.AudioFileOutputChannels.OrderBy(x => x.Index).ToArray();
                IList<SpeakerSetupChannel> sortedSpeakerSetupChannels = audioFileOutput.SpeakerSetup.SpeakerSetupChannels.OrderBy(x => x.Channel.Index).ToArray();
                
                if (sortedAudioFileOutputChannels.Count == sortedSpeakerSetupChannels.Count)
                {
                    for (int i = 0; i < sortedAudioFileOutputChannels.Count; i++)
                    {
                        AudioFileOutputChannel audioFileOutputChannel = sortedAudioFileOutputChannels[i];
                        SpeakerSetupChannel speakerSetupChannel = sortedSpeakerSetupChannels[i];

                        if (audioFileOutputChannel.Index != speakerSetupChannel.Index)
                        {
                            string messageHeading = String.Format("{0} {1}: ", PropertyDisplayNames.Channel, i + 1);

                            ValidationMessages.Add(() => audioFileOutputChannel.Index, messageHeading + MessagesFormatter.ChannelIndexDoesNotMatchSpeakerSetup());
                        }
                    }
                }
            }
        }
    }
}