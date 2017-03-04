using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Warnings.Operators;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class PatchWarningValidator_Recursive : VersatileValidator<Patch>
    {
        private readonly ISampleRepository _sampleRepository;

        private readonly HashSet<object> _alreadyDone;

        public PatchWarningValidator_Recursive(Patch obj, ISampleRepository sampleRepository, HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            foreach (Operator op in Obj.Operators)
            {
                ExecuteValidator(new Recursive_OperatorWarningValidator(op, _sampleRepository, _alreadyDone));
            }
        }
    }
}
