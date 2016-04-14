using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Resample_OperatorValidator : OperatorValidator_Base
    {
        public Resample_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Resample,
                  expectedInletCount: 2,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.InterpolationType, PropertyNames.Dimension })
        { }

        protected override void Execute()
        {
            base.Execute();

            if (DataPropertyParser.DataIsWellFormed(Object))
            {
                string interpolationTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);
                For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                    .NotNullOrEmpty()
                    .IsEnum<ResampleInterpolationTypeEnum>()
                    .IsNot(ResampleInterpolationTypeEnum.Undefined);

                string dimensionString = DataPropertyParser.TryGetString(Object, PropertyNames.Dimension);
                For(() => dimensionString, PropertyNames.Dimension)
                    .NotNullOrEmpty()
                    .IsEnum<DimensionEnum>()
                    .IsNot(DimensionEnum.Undefined);
            }
        }
    }
}
