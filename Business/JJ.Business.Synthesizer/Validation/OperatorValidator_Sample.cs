using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_Sample : OperatorValidator_Base
    {
        public OperatorValidator_Sample(Operator op)
            : base(op, OperatorTypeEnum.Sample, 0, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Data, PropertyDisplayNames.Data)
                .IsInteger();
        }
    }
}
