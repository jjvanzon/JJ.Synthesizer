using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
    internal class SpeakerSetup_DataProperty_Validator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public SpeakerSetup_DataProperty_Validator(string obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            string data = Obj;

            // ReSharper disable once InvertIf
            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string speakerSetupString = DataPropertyParser.TryGetString(Obj, nameof(Cache_OperatorWrapper.SpeakerSetup));

                For(() => speakerSetupString, ResourceFormatter.SpeakerSetup)
                    .NotNullOrEmpty()
                    .IsEnum<SpeakerSetupEnum>()
                    .IsNot(SpeakerSetupEnum.Undefined);
            }
        }
    }
}
