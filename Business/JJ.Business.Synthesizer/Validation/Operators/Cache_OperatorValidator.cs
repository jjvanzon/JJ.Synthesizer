using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Cache_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Cache_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Cache,
                expectedInletCount: 4,
                expectedOutletCount: 1,
                allowedDataKeys: new string[]
                {
                    PropertyNames.InterpolationType,
                    PropertyNames.SpeakerSetup,
                    PropertyNames.Dimension
                })
        { }

        protected override void Execute()
        {
            base.Execute();

            if (DataPropertyParser.DataIsWellFormed(Object))
            {
                string interpolationTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);
                For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                    .NotNullOrEmpty()
                    .IsEnum<InterpolationTypeEnum>()
                    .IsNot(InterpolationTypeEnum.Undefined);

                string speakerSetupString = DataPropertyParser.TryGetString(Object, PropertyNames.SpeakerSetup);
                For(() => speakerSetupString, PropertyDisplayNames.SpeakerSetup)
                    .NotNullOrEmpty()
                    .IsEnum<SpeakerSetupEnum>()
                    .IsNot(SpeakerSetupEnum.Undefined);
            }
        }
    }
}