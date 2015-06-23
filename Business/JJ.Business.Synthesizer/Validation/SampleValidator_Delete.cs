using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal class SampleValidator_Delete : FluentValidator<Sample>
    {
        private ISampleRepository _sampleRepository;

        public SampleValidator_Delete(Sample obj, ISampleRepository sampleRepository)
            : base(obj, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;

            Execute();
        }

        protected override void Execute()
        {
            bool hasOperators = EnumerateSampleOperators(Object).Any();
            if (hasOperators)
            {
                // TODO: It might be handy to know what patch and possibly what operator still uses it.
                ValidationMessages.Add(PropertyNames.Sample, MessageFormatter.CannotDeleteSampleBecauseHasOperators(Object.Name));
            }
        }

        private IEnumerable<Operator> EnumerateSampleOperators(Sample sample)
        {
            if (sample == null) throw new NullException(() => sample);

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
