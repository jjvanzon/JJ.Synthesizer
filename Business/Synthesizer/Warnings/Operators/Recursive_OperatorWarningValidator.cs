using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
    internal class Recursive_OperatorWarningValidator : ValidatorBase<Operator>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly HashSet<object> _alreadyDone;

        public Recursive_OperatorWarningValidator(Operator obj, ISampleRepository sampleRepository, HashSet<object> alreadyDone = null)
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

            foreach (Inlet inlet in Obj.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    ExecuteValidator(new Recursive_OperatorWarningValidator(inlet.InputOutlet.Operator, _sampleRepository, _alreadyDone));
                }
            }
        }
    }
}
