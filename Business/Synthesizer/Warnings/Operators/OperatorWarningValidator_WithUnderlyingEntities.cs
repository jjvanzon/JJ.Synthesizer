using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class OperatorWarningValidator_WithUnderlyingEntities : VersatileValidator<Operator>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly HashSet<object> _alreadyDone;

        /// <summary>
        /// Validates an operator, but not its descendant operators.
        /// Does validate underlying curves and samples.
        /// Makes sure that objects are only validated once to 
        /// prevent excessive validation messages.
        /// The reason that underlying entities such as samples and curves are validated here,
        /// is because even though it already happens when you validate a whole document,
        /// in some cases you do not validate the whole document, but a narrower scope,
        /// such as a patch.
        /// </summary>
        public OperatorWarningValidator_WithUnderlyingEntities(
            [NotNull] Operator obj,
            [NotNull] ISampleRepository sampleRepository,
            [CanBeNull] HashSet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Obj)) return;
            _alreadyDone.Add(Obj);

            ExecuteValidator(new Versatile_OperatorWarningValidator(Obj));

            if (Obj.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
            {
                int sampleID;
                if (int.TryParse(Obj.Data, out sampleID))
                {
                    Sample sample = _sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        byte[] bytes = _sampleRepository.TryGetBytes(sampleID);
                        ExecuteValidator(new SampleWarningValidator(sample, bytes, _alreadyDone));
                    }
                }
            }

            // There are no curve warnings.
        }
    }
}
