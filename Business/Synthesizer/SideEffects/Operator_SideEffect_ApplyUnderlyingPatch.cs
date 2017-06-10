using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_ApplyUnderlyingPatch : ISideEffect
    {
        private readonly Operator _entity;
        private readonly RepositoryWrapper _repositories;

        public Operator_SideEffect_ApplyUnderlyingPatch(Operator entity, RepositoryWrapper repositories)
        {
            _entity = entity ?? throw new NullException(() => entity);
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        public void Execute()
        {
            bool mustExecute = MustExecute();
            // ReSharper disable once InvertIf
            if (mustExecute)
            {
                Patch sourceUnderlyingPatch = _entity.UnderlyingPatch;

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
