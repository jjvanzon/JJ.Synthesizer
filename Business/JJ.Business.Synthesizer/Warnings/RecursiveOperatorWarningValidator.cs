using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class RecursiveOperatorWarningValidator : ValidatorBase<Operator>
    {
        private ISampleRepository _sampleRepository;
        private ISet<object> _alreadyDone;

        public RecursiveOperatorWarningValidator(Operator obj, ISampleRepository sampleRepository, ISet<object> alreadyDone = null)
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

            Execute<VersatileOperatorWarningValidator>();

            if (String.Equals(Object.OperatorTypeName, PropertyNames.SampleOperator))
            {
                int sampleID;
                if (Int32.TryParse(Object.Data, out sampleID))
                {
                    Sample sample = _sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        if (!_alreadyDone.Contains(sample))
                        {
                            _alreadyDone.Add(sample);
                            Execute(new SampleWarningValidator(sample));
                        }
                    }
                }
            }


            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    Execute(new RecursiveOperatorWarningValidator(inlet.InputOutlet.Operator, _sampleRepository, _alreadyDone));
                }
            }
        }
    }
}
