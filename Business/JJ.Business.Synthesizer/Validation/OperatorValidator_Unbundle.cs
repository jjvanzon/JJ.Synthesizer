using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Unbundle : FluentValidator<Operator>
    {
        public OperatorValidator_Unbundle(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(OperatorTypeEnum.Unbundle);
            For(() => op.Inlets.Count, CommonTitleFormatter.EntityCount(PropertyDisplayNames.Inlets)).Is(1);
            For(() => op.Outlets.Count, CommonTitleFormatter.EntityCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
            For(() => op.Data, PropertyDisplayNames.Data).IsNull();
        }
    }
}