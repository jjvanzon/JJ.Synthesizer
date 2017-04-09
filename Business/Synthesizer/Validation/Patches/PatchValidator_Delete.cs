using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_Delete : VersatileValidator<Patch>
    {
        private readonly IPatchRepository _patchRepository;

        public PatchValidator_Delete([NotNull] Patch obj, [NotNull] IPatchRepository patchRepository)
            : base(obj, postponeExecute: true)
        {
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            Patch lowerPatch = Obj;

            string lowerPatchIdentifier = ResourceFormatter.Patch + " " + ValidationHelper.GetUserFriendlyIdentifier(lowerPatch);

            IEnumerable<Operator> customOperators = lowerPatch.EnumerateDependentCustomOperators(_patchRepository);
            foreach (Operator op in customOperators)
            {
                string higherPatchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string higherOperatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCustomOperator(op, _patchRepository);

                ValidationMessages.Add(
                    nameof(Patch),
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(lowerPatchIdentifier, higherPatchPrefix + higherOperatorIdentifier));
            }
        }
    }
}