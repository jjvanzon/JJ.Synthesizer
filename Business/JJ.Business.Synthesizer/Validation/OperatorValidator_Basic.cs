using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Basic : FluentValidator<Operator>
    {
        public OperatorValidator_Basic(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).NotInteger();
            For(() => Object.OperatorType, PropertyDisplayNames.OperatorType).NotNull();
            For(() => Object.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).IsEnumValue<OperatorTypeEnum>();
        }
    }
}