using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator : VersatileValidator
    {
        public AudioFileOutputValidator(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            For(() => audioFileOutput.Amplifier, ResourceFormatter.Amplifier)
                .NotNaN()
                .NotInfinity();

            For(() => audioFileOutput.StartTime, ResourceFormatter.StartTime)
                .NotNaN()
                .NotInfinity();

            For(() => audioFileOutput.TimeMultiplier, ResourceFormatter.TimeMultiplier)
                .NotNaN()
                .NotInfinity()
                .IsNot(0);

            For(() => audioFileOutput.Duration, ResourceFormatter.Duration)
                .NotNaN()
                .NotInfinity()
                .GreaterThan(0);

            For(() => audioFileOutput.SamplingRate, ResourceFormatter.SamplingRate).GreaterThan(0);
            For(() => audioFileOutput.AudioFileFormat, ResourceFormatter.AudioFileFormat).NotNull();
            For(() => audioFileOutput.SampleDataType, ResourceFormatter.SampleDataType).NotNull();
            For(() => audioFileOutput.SpeakerSetup, ResourceFormatter.SpeakerSetup).NotNull();

            TryValidateOutletReference(audioFileOutput);
        }

        private void TryValidateOutletReference(AudioFileOutput audioFileOutput)
        {
            bool mustValidate = audioFileOutput.Outlet != null &&
                                audioFileOutput.Document != null;

            if (!mustValidate)
            {
                return;
            }

            IEnumerable<Outlet> outletsEnumerable = 
                audioFileOutput.Document.Patches
                               .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
                               .SelectMany(x => x.Outlets);

            bool referenceIsValid = outletsEnumerable.Any(x => x.ID == audioFileOutput.Outlet.ID);

            if (!referenceIsValid)
            {
                ValidationMessages.AddNotInListMessage(nameof(Outlet), ResourceFormatter.Outlet, audioFileOutput.Outlet.ID);
            }
        }
    }
}