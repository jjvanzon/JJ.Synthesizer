using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation.OperatorData;

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

            Execute(new InterpolationType_OperatorData_Validator(Object.Data));
            Execute(new SpeakerSetup_OperatorData_Validator(Object.Data));
        }
    }
}