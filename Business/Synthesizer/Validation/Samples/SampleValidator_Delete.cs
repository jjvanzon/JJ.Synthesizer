using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Samples
{
    internal class SampleValidator_Delete : VersatileValidator<Sample>
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleValidator_Delete([NotNull] Sample obj, [NotNull] ISampleRepository sampleRepository)
            : base(obj, postponeExecute: true)
        {
            _sampleRepository = sampleRepository ?? throw new NullException(() => sampleRepository);

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            string sampleIdentifier = ResourceFormatter.Sample + " " + ValidationHelper.GetUserFriendlyIdentifier(Obj);

            foreach (Operator op in EnumerateSampleOperators(Obj))
            {
                string patchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string operatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForSampleOperator(op, _sampleRepository);

                ValidationMessages.Add(
                    nameof(Sample),
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(sampleIdentifier, patchPrefix + operatorIdentifier));
            }
        }

        private IEnumerable<Operator> EnumerateSampleOperators([NotNull] Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);
            if (sample.Document == null)
            {
                yield break;
            }

            foreach (Operator op in sample.Document.Patches.SelectMany(x => x.Operators))
            {
                if (op.GetOperatorTypeEnum() != OperatorTypeEnum.Sample)
                {
                    continue;
                }

                var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);

                if (wrapper.Sample == sample ||
                    wrapper.SampleID == sample.ID)
                {
                    yield return op;
                }
            }
        }
    }
}
