using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Converters;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Patch_SideEffect_UpdateDependentCustomOperators : ISideEffect
    {
        private readonly Patch _underlyingPatch;
        private readonly IPatchRepository _patchRepository;
        private readonly PatchToOperatorConverter _patchToOperatorConverter;

        public Patch_SideEffect_UpdateDependentCustomOperators(Patch underlyingPatch, PatchRepositories repositories)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (repositories == null) throw new NullException(() => repositories);

            _underlyingPatch = underlyingPatch;
            _patchRepository = repositories.PatchRepository;

            _patchToOperatorConverter = new PatchToOperatorConverter(repositories);
        }

        public void Execute()
        {
            IList<Operator> customOperators = _underlyingPatch.EnumerateDependentCustomOperators(_patchRepository).ToArray();

            foreach (Operator customOperator in customOperators)
            {
                _patchToOperatorConverter.Convert(_underlyingPatch, customOperator);
            }
        }
    }
}
