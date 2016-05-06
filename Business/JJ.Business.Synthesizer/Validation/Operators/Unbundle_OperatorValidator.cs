using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Unbundle_OperatorValidator : OperatorValidator_Base
    {
        public Unbundle_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Unbundle,
                  expectedInletCount: 1,
                  expectedOutletCount: obj.Outlets.Count, // TODO: Low priority: if obj is null, this fails.
                  allowedDataKeys: new string[] { PropertyNames.Dimension })
        { }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                // Dimension can be Undefined, but key must exist.
                string dimensionString = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);
                For(() => dimensionString, PropertyNames.Dimension)
                    .NotNullOrEmpty()
                    .IsEnum<DimensionEnum>();
            }

            base.Execute();
        }
    }
}