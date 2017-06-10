using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Absolute_OperatorValidator : CustomOperator_OperatorValidator
    {
        public Absolute_OperatorValidator([NotNull] Operator op) 
            : base(op)
        { }

        protected override void Execute()
        {
            Operator op = Obj;

            For(() => op.GetOperatorTypeEnum(), ResourceFormatter.OperatorType).Is(OperatorTypeEnum.Absolute);

            base.Execute();
        }
    }
}
