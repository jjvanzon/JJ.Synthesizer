using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.OperatorData
{
    internal class SpeakerSetup_OperatorData_Validator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
    {
        public SpeakerSetup_OperatorData_Validator(string obj) 
            : base(obj)
        { }

        protected override void Execute()
        {
            string data = Object;

            if (DataPropertyParser.DataIsWellFormed(data))
            {
                string speakerSetupString = DataPropertyParser.TryGetString(Object, PropertyNames.SpeakerSetup);
                For(() => speakerSetupString, PropertyDisplayNames.SpeakerSetup)
                    .NotNullOrEmpty()
                    .IsEnum<SpeakerSetupEnum>()
                    .IsNot(SpeakerSetupEnum.Undefined);
            }
        }
    }
}
