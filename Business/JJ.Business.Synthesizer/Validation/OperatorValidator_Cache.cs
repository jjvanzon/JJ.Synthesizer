using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System;
using JJ.Framework.Validation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Mathematics;
using System.Globalization;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Cache : OperatorValidator_Base
    {
        public OperatorValidator_Cache(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Cache, 
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.SpeakerSetup, PropertyNames.InterpolationType })
        { }

        protected override void Execute()
        {
            base.Execute();

            string speakerSetupString = DataPropertyParser.TryGetString(Object, PropertyNames.SpeakerSetup);
            string interpolationTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);

            For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                .NotNullOrEmpty()
                .IsEnum<InterpolationTypeEnum>()
                .IsNot(InterpolationTypeEnum.Undefined);

            For(() => speakerSetupString, PropertyDisplayNames.SpeakerSetup)
                .NotNullOrEmpty()
                .IsEnum<SpeakerSetupEnum>()
                .IsNot(SpeakerSetupEnum.Undefined);
        }
    }
}
