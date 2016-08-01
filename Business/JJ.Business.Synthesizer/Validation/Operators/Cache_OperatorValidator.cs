using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Cache_OperatorValidator : OperatorValidator_Base
    {
        public Cache_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Cache,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal },
                expectedDataKeys: new string[]
                {
                    PropertyNames.InterpolationType,
                    PropertyNames.SpeakerSetup
                })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new InterpolationType_DataProperty_Validator(Object.Data));
            ExecuteValidator(new SpeakerSetup_DataProperty_Validator(Object.Data));
        }
    }
}