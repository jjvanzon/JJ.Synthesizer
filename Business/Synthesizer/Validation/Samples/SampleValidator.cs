using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Samples
{
    internal class SampleValidator : VersatileValidator<Sample>
    {
        public SampleValidator(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Sample sample = Obj;

            ExecuteValidator(new NameValidator(sample.Name, required: false));

            For(() => sample.SamplingRate, PropertyDisplayNames.SamplingRate).GreaterThan(0);
            For(() => sample.Amplifier, PropertyDisplayNames.Amplifier).NotNaN().NotInfinity();
            For(() => sample.TimeMultiplier, PropertyDisplayNames.TimeMultiplier).NotNaN().NotInfinity().IsNot(0);
            For(() => sample.AudioFileFormat, PropertyDisplayNames.AudioFileFormat).NotNull();
            For(() => sample.SampleDataType, PropertyDisplayNames.SampleDataType).NotNull();
            For(() => sample.InterpolationType, PropertyDisplayNames.InterpolationType).NotNull();
            For(() => sample.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();
        }
    }
}
