using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_UpdateDependentCustomOperators : ISideEffect
    {
        private readonly Operator _entity;
        private readonly PatchRepositories _repositories;

        public Operator_SideEffect_UpdateDependentCustomOperators(Operator entity, PatchRepositories repositories)
        {
            if (entity == null) throw new NullException(() => entity);
            if (repositories == null) throw new NullException(() => repositories);

            _entity = entity;
            _repositories = repositories;
        }

        public void Execute()
        {
            if (MustExecute())
            {
                ISideEffect sideEffect2 = new Patch_SideEffect_UpdateDependentCustomOperators(_entity.Patch, _repositories);
                sideEffect2.Execute();
            }
        }

        private bool MustExecute()
        {
            OperatorTypeEnum operatorTypeEnum = _entity.GetOperatorTypeEnum();

            bool mustExecute = operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
                               operatorTypeEnum == OperatorTypeEnum.PatchOutlet;

            return mustExecute;
        }
    }
}