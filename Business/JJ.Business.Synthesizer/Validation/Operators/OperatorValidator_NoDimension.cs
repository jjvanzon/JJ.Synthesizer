using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_NoDimension : FluentValidator<Operator>
    {
        public OperatorValidator_NoDimension(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();
            For(() => Object.CustomDimensionName, PropertyDisplayNames.CustomDimensionName).IsNullOrEmpty();
        }
    }
}
