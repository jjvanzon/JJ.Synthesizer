using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class SampleValidator : FluentValidator<Sample>
    {
        public SampleValidator(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            Sample sample = Object;

            For(() => sample.Name, PropertyDisplayNames.Name).NotInteger();
            For(() => sample.SamplingRate, PropertyDisplayNames.SamplingRate).Above(0);
            For(() => sample.AudioFileFormat, PropertyDisplayNames.AudioFileFormat).NotNull();
            For(() => sample.TimeMultiplier, PropertyDisplayNames.AudioFileFormat).IsNot(0);

            if (sample.AudioFileFormat != null)
            {
                For(() => sample.AudioFileFormat.ID, PropertyDisplayNames.AudioFileFormat)
                    .IsEnumValue<AudioFileFormatEnum>()
                    .IsNot(AudioFileFormatEnum.Undefined);
            }

            For(() => sample.SampleDataType, PropertyDisplayNames.SampleDataType).NotNull();

            if (sample.SampleDataType != null)
            {
                For(() => sample.SampleDataType.ID, PropertyDisplayNames.SampleDataType)
                    .IsEnumValue<SampleDataTypeEnum>()
                    .IsNot(SampleDataTypeEnum.Undefined);
            }

            For(() => sample.InterpolationType, PropertyDisplayNames.InterpolationType).NotNull();

            if (sample.InterpolationType != null)
            {
                For(() => sample.InterpolationType.ID, PropertyDisplayNames.InterpolationType)
                    .IsEnumValue<InterpolationTypeEnum>()
                    .IsNot(InterpolationTypeEnum.Undefined);
            }

            For(() => sample.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();

            For(() => sample.SampleChannels.Count, CommonTitlesFormatter.EntityCount(PropertyDisplayNames.SampleChannels))
                .Above(0);

            if (sample.SampleChannels.Count != sample.SpeakerSetup.SpeakerSetupChannels.Count)
            {
                ValidationMessages.Add(() => sample.SampleChannels.Count, MessagesFormatter.ChannelCountDoesNotMatchSpeakerSetup());
            }
            else
            {
                if (!sample.SampleChannels.All(x => x.RawBytes == null))
                {
                    // TODO: Can this not be done simpler?
                    if (sample.SampleChannels.Select(x => x.RawBytes.Length).Distinct().Count() != 1)
                    {
                        ValidationMessages.Add(() => sample.SampleChannels, MessagesFormatter.ChannelsMustAllHaveSameSize());
                    }
                }

                for (int i = 0; i < sample.SampleChannels.Count; i++)
                {
                    SampleChannel sampleChannel = sample.SampleChannels[i];
                    Channel expectedChannel = sample.SpeakerSetup.SpeakerSetupChannels[i].Channel;

                    string messageHeading = String.Format("{0} {1}: ", PropertyDisplayNames.SampleChannel, i + 1);
                    Execute(new SampleChannelValidator(sampleChannel, expectedChannel), messageHeading);
                }
            }
        }
    }
}
