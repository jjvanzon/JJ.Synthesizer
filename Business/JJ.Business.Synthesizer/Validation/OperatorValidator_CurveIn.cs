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
    public class OperatorValidator_CurveIn : OperatorValidator_Base
    {
        public OperatorValidator_CurveIn(Operator op)
            : base(op, OperatorTypeEnum.CurveIn, 0, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.Data, PropertyDisplayNames.Data)
                .IsInteger();
        }
    }
}
