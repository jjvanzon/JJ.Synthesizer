using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Warnings
{
    public class OperatorWarningValidator_Recursive : ValidatorBase<Operator>
    {
        private ISampleRepository _sampleRepository;
        private HashSet<object> _alreadyDone;

        public OperatorWarningValidator_Recursive(Operator obj, ISampleRepository sampleRepository, HashSet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object)) return;
            _alreadyDone.Add(Object);

            Execute<OperatorWarningValidator_Versatile>();

            if (Object.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
            {
                int sampleID;
                if (Int32.TryParse(Object.Data, out sampleID))
                {
                    Sample sample = _sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        Execute(new SampleWarningValidator(sample, _alreadyDone));
                    }
                }
            }

            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    Execute(new OperatorWarningValidator_Recursive(inlet.InputOutlet.Operator, _sampleRepository, _alreadyDone));
                }
            }
        }
    }
}
