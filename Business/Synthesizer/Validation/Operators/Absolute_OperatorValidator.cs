using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Absolute_OperatorValidator : CustomOperator_OperatorValidator
    {
        public Absolute_OperatorValidator([NotNull] Operator op, [NotNull] IPatchRepository patchRepository) 
            : base(op, patchRepository)
        { }

        protected override void Execute()
        {
            Operator op = Obj;

            For(() => op.GetOperatorTypeEnum(), ResourceFormatter.OperatorType).Is(OperatorTypeEnum.Absolute);

            base.Execute();
        }
    }
}
