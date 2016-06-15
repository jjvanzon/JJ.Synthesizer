using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MakeContinuous_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public MakeContinuous_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.MakeContinuous,
                  allowedDataKeys: new string[] { PropertyNames.Dimension, PropertyNames.InterpolationType })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                // Dimension can be Undefined, but key must exist.
                string dimensionString = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);
                For(() => dimensionString, PropertyNames.Dimension)
                    .NotNullOrEmpty()
                    .IsEnum<DimensionEnum>();

                string interpolationTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);
                For(() => interpolationTypeString, PropertyDisplayNames.InterpolationType)
                    .NotNullOrEmpty()
                    .IsEnum<ResampleInterpolationTypeEnum>()
                    .IsNot(ResampleInterpolationTypeEnum.Undefined);
            }
        }
    }
}