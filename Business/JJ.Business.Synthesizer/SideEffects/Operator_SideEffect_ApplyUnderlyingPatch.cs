using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_ApplyUnderlyingPatch : ISideEffect
    {
        private Operator _operator;
        private OperatorWrapper_CustomOperator _custom_OperatorWrapper;
        private PatchToOperatorConverter _documentToOperatorConverter;

        public Operator_SideEffect_ApplyUnderlyingPatch(
            Operator op,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IPatchRepository patchRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (op == null) throw new NullException(() => op);

            _operator = op;
            _custom_OperatorWrapper = new OperatorWrapper_CustomOperator(_operator, patchRepository);
            _documentToOperatorConverter = new PatchToOperatorConverter(inletRepository, outletRepository, patchRepository, operatorTypeRepository, idRepository);
        }

        public void Execute()
        {
            Patch sourceUnderlyingPatch = _custom_OperatorWrapper.UnderlyingPatch;
            _documentToOperatorConverter.Convert(sourceUnderlyingPatch, _operator);
        }
    }
}
