using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_ApplyUnderlyingPatch : ISideEffect
    {
        private Operator _operator;
        private CustomOperator_OperatorWrapper _custom_OperatorWrapper;
        private PatchToOperatorConverter _documentToOperatorConverter;

        public Operator_SideEffect_ApplyUnderlyingPatch(Operator op, PatchRepositories repositories)
        {
            if (op == null) throw new NullException(() => op);
            if (repositories == null) throw new NullException(() => repositories);

            _operator = op;
            _custom_OperatorWrapper = new CustomOperator_OperatorWrapper(_operator, repositories.PatchRepository);
            _documentToOperatorConverter = new PatchToOperatorConverter(repositories);
        }

        public void Execute()
        {
            Patch sourceUnderlyingPatch = _custom_OperatorWrapper.UnderlyingPatch;
            _documentToOperatorConverter.Convert(sourceUnderlyingPatch, _operator);
        }
    }
}
