using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
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
            For(() => sample.TimeMultiplier, PropertyDisplayNames.TimeMultiplier).IsNot(0);

            For(() => sample.AudioFileFormat, PropertyDisplayNames.AudioFileFormat).NotNull();
            For(() => sample.SampleDataType, PropertyDisplayNames.SampleDataType).NotNull();
            For(() => sample.InterpolationType, PropertyDisplayNames.InterpolationType).NotNull();
            For(() => sample.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();

            if (sample.AudioFileFormat != null)
            {
                For(() => sample.AudioFileFormat.ID, PropertyDisplayNames.AudioFileFormat)
                    .IsEnumValue<AudioFileFormatEnum>()
                    .IsNot(AudioFileFormatEnum.Undefined);
            }

            if (sample.SampleDataType != null)
            {
                For(() => sample.SampleDataType.ID, PropertyDisplayNames.SampleDataType)
                    .IsEnumValue<SampleDataTypeEnum>()
                    .IsNot(SampleDataTypeEnum.Undefined);
            }

            if (sample.InterpolationType != null)
            {
                For(() => sample.InterpolationType.ID, PropertyDisplayNames.InterpolationType)
                    .IsEnumValue<InterpolationTypeEnum>()
                    .IsNot(InterpolationTypeEnum.Undefined);
            }
        }
    }
}
