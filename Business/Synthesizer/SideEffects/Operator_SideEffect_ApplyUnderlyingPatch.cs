using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_ApplyUnderlyingPatch : ISideEffect
    {
        private readonly Operator _entity;
        private readonly PatchRepositories _repositories;

        public Operator_SideEffect_ApplyUnderlyingPatch(Operator entity, PatchRepositories repositories)
        {
            if (entity == null) throw new NullException(() => entity);
            if (repositories == null) throw new NullException(() => repositories);

            _entity = entity;
            _repositories = repositories;
        }

        public void Execute()
        {
            bool mustExecute = MustExecute();
            if (mustExecute)
            {
                var wrapper = new CustomOperator_OperatorWrapper(_entity, _repositories.PatchRepository);
                Patch sourceUnderlyingPatch = wrapper.UnderlyingPatch;

                var converter = new PatchToOperatorConverter(_repositories);
                converter.Convert(sourceUnderlyingPatch, _entity);
            }
        }

        private bool MustExecute()
        {
            return _entity.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator;
        }
    }
}
