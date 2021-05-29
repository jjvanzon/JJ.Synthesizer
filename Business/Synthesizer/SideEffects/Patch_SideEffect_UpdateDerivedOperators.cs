using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Patch_SideEffect_UpdateDerivedOperators : ISideEffect
    {
        private readonly Patch _underlyingPatch;
        private readonly PatchToOperatorConverter _patchToOperatorConverter;

        public Patch_SideEffect_UpdateDerivedOperators(Patch underlyingPatch, RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _underlyingPatch = underlyingPatch ?? throw new NullException(() => underlyingPatch);

            _patchToOperatorConverter = new PatchToOperatorConverter(repositories);
        }

        public void Execute()
        {
            IEnumerable<Operator> derivedOperators = _underlyingPatch.EnumerateDerivedOperators();

            foreach (Operator derivedOperator in derivedOperators.ToArray())
            {
                _patchToOperatorConverter.Convert(_underlyingPatch, derivedOperator);
            }
        }
    }
}
