using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            AudioFileOutput audioFileOutput = Object;

            For(() => audioFileOutput.Amplifier, PropertyDisplayNames.Amplifier)
                .NotNaN()
                .NotInfinity();

            For(() => audioFileOutput.StartTime, PropertyDisplayNames.StartTime)
                .NotNaN()
                .NotInfinity();

            For(() => audioFileOutput.TimeMultiplier, PropertyDisplayNames.TimeMultiplier)
                .NotNaN()
                .NotInfinity()
                .IsNot(0);

            For(() => audioFileOutput.Duration, PropertyDisplayNames.Duration)
                .NotNaN()
                .NotInfinity()
                .GreaterThan(0);

            For(() => audioFileOutput.SamplingRate, PropertyDisplayNames.SamplingRate).GreaterThan(0);
            For(() => audioFileOutput.AudioFileFormat, PropertyDisplayNames.AudioFileFormat).NotNull();
            For(() => audioFileOutput.SampleDataType, PropertyDisplayNames.SampleDataType).NotNull();
            For(() => audioFileOutput.SpeakerSetup, PropertyDisplayNames.SpeakerSetup).NotNull();

            TryValidateOutletReference();
        }

        private void TryValidateOutletReference()
        {
            bool mustValidate = Object.Outlet != null &&
                                Object.Document != null;

            if (!mustValidate)
            {
                return;
            }

            IEnumerable<Outlet> outletsEnumerable = 
                Object.Document.Patches
                               .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
                               .SelectMany(x => x.Outlets);

            bool referenceIsValid = outletsEnumerable.Any(x => x.ID == Object.Outlet.ID);

            if (!referenceIsValid)
            {
                ValidationMessages.AddNotInListMessage(PropertyNames.Outlet, PropertyDisplayNames.Outlet, Object.Outlet.ID);
            }
        }
    }
}