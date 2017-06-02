using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator : VersatileValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            AudioFileOutput audioFileOutput = Obj;

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

            TryValidateOutletReference();
        }

        private void TryValidateOutletReference()
        {
            bool mustValidate = Obj.Outlet != null &&
                                Obj.Document != null;

            if (!mustValidate)
            {
                return;
            }

            IEnumerable<Outlet> outletsEnumerable = 
                Obj.Document.Patches
                               .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
                               .SelectMany(x => x.Outlets);

            bool referenceIsValid = outletsEnumerable.Any(x => x.ID == Obj.Outlet.ID);

            if (!referenceIsValid)
            {
                ValidationMessages.AddNotInListMessage(nameof(Outlet), ResourceFormatter.Outlet, Obj.Outlet.ID);
            }
        }
    }
}