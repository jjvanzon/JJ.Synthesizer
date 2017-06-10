using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    /// <summary> Only effective if Operator is CustomOperator. (Might change in the future.) </summary>
    internal class Operator_SideEffect_ApplyUnderlyingPatch : ISideEffect
    {
        private readonly Operator _operator;
        private readonly RepositoryWrapper _repositories;

        public Operator_SideEffect_ApplyUnderlyingPatch(Operator op, RepositoryWrapper repositories)
        {
            _operator = op ?? throw new NullException(() => op);
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        public void Execute()
        {
            bool mustExecute = MustExecute();
            // ReSharper disable once InvertIf
            if (mustExecute)
            {
                Patch sourceUnderlyingPatch = _operator.UnderlyingPatch;

                var converter = new PatchToOperatorConverter(_repositories);
                converter.Convert(sourceUnderlyingPatch, _operator);
            }
        }

        private bool MustExecute() => _operator.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator;
    }
}
