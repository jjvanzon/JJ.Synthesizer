using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Unbundle_OperatorValidator : FluentValidator<Operator>
    {
        public Unbundle_OperatorValidator(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(OperatorTypeEnum.Unbundle);
            For(() => op.Inlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets)).Is(1);
            For(() => op.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
            For(() => op.Data, PropertyDisplayNames.Data).IsNull();
        }
    }
}