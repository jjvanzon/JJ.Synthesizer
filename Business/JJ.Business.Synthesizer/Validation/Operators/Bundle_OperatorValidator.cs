using JJ.Data.Synthesizer;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Bundle_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Bundle_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Bundle, allowedDataKeys: new string[] { PropertyNames.Dimension })
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
            }
        }
    }
}