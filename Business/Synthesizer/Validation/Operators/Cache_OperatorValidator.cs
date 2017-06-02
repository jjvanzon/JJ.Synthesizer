using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Cache_OperatorValidator : OperatorValidator_Base
    {
        public Cache_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Cache,
                new[] { DimensionEnum.Signal, DimensionEnum.Start, DimensionEnum.End, DimensionEnum.SamplingRate},
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new[]
                {
                    nameof(Cache_OperatorWrapper.InterpolationType),
                    nameof(Cache_OperatorWrapper.SpeakerSetup)
                })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new InterpolationType_DataProperty_Validator(Obj.Data));
            ExecuteValidator(new SpeakerSetup_DataProperty_Validator(Obj.Data));
        }
    }
}