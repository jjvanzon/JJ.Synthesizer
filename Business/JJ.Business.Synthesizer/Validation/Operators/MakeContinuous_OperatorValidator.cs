using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;

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

            ExecuteValidator(new Dimension_DataProperty_Validator(Object.Data));
            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(Object.Data));
        }
    }
}