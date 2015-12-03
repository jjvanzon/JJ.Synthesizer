using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Converters;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Patch_SideEffect_UpdateDependentCustomOperators : ISideEffect
    {
        private readonly Patch _underlyingPatch;
        private readonly IPatchRepository _patchRepository;
        private readonly PatchToOperatorConverter _patchToOperatorConverter;

        public Patch_SideEffect_UpdateDependentCustomOperators(
            Patch underlyingPatch,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IPatchRepository patchRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _underlyingPatch = underlyingPatch;
            _patchRepository = patchRepository;

            _patchToOperatorConverter = new PatchToOperatorConverter(
                inletRepository, outletRepository, patchRepository, operatorTypeRepository, idRepository);
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
