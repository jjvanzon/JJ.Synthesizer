﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Samples
{
    internal class SampleValidator_Delete : VersatileValidator<Sample>
    {
        private readonly ISampleRepository _sampleRepository;

        public SampleValidator_Delete([NotNull] Sample obj, [NotNull] ISampleRepository sampleRepository)
            : base(obj, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            bool hasOperators = EnumerateSampleOperators(Obj).Any();
            if (hasOperators)
            {
                // TODO: It might be handy to know what patch and possibly what operator still uses it.
                ValidationMessages.Add(PropertyNames.Sample, ResourceFormatter.CannotDeleteSampleBecauseHasOperators(Obj.Name));
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