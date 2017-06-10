using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
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
        public SampleValidator_Delete([NotNull] Sample sample, [NotNull] ISampleRepository sampleRepository)
            : base(sample)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            string sampleIdentifier = ResourceFormatter.Sample + " " + ValidationHelper.GetUserFriendlyIdentifier(sample);

            foreach (Operator op in EnumerateSampleOperators(sample, sampleRepository))
            {
                string patchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string operatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForSampleOperator(op, sampleRepository);

                ValidationMessages.Add(
                    nameof(Sample),
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(sampleIdentifier, patchPrefix + operatorIdentifier));
            }
        }

        private IEnumerable<Operator> EnumerateSampleOperators([NotNull] Sample sample, ISampleRepository sampleRepository)
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

                var wrapper = new Sample_OperatorWrapper(op, sampleRepository);

                if (wrapper.Sample == sample ||
                    wrapper.SampleID == sample.ID)
                {
                    yield return op;
                }
            }
        }
    }
}
