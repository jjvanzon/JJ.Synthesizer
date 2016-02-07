using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Resample : OperatorValidator_Base
    {
        public OperatorValidator_Resample(Operator obj)
            : base(obj, OperatorTypeEnum.Resample, expectedInletCount: 2, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string interpolationTypeString = OperatorDataParser.GetString(Object, PropertyNames.InterpolationType);

            For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                .NotNullOrEmpty()
                .IsEnum<ResampleInterpolationTypeEnum>()
                .IsNot(ResampleInterpolationTypeEnum.Undefined);
        }
    }
}
