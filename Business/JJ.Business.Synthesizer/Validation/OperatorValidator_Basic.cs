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
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Basic : FluentValidator<Operator>
    {
        public OperatorValidator_Basic(Operator obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute(new NameValidator(Object.Name, required: false));

            For(() => Object.OperatorType, PropertyDisplayNames.OperatorType).NotNull();
            For(() => Object.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).IsEnumValue<OperatorTypeEnum>();
        }
    }
}