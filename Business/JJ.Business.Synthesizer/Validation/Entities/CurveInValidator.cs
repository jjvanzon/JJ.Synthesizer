using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation.Entities
{
    public class CurveInValidator : OperatorValidatorBase
    {
        public CurveInValidator(Operator op)
            : base(op, PropertyNames.CurveIn, 0, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.AsCurveIn, PropertyDisplayNames.AsCurveIn)
                .NotNull();

            For(() => op.AsSampleOperator, PropertyDisplayNames.AsSampleOperator)
                .IsNull();

            For(() => op.AsValueOperator, PropertyDisplayNames.AsValueOperator)
                .IsNull();
        }
    }
}
