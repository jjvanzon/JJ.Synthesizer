using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class SpeakerSetup_DataProperty_Validator : VersatileValidator
    {
        public SpeakerSetup_DataProperty_Validator(string data) 
        {
            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string speakerSetupString = DataPropertyParser.TryGetString(data, nameof(Cache_OperatorWrapper.SpeakerSetup));

                For(speakerSetupString, ResourceFormatter.SpeakerSetup)
                    .NotNullOrEmpty()
                    .IsEnum<SpeakerSetupEnum>()
                    .IsNot(SpeakerSetupEnum.Undefined);
            }
        }
    }
}
