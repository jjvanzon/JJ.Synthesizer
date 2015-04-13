using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class PatchValidator : FluentValidator<Patch>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;

        public PatchValidator(Patch obj, ICurveRepository curveRepository, ISampleRepository sampleRepository)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;

            Execute();
        }

        protected override void Execute()
        {
            var alreadyDone = new HashSet<object>();

            string messagePrefix;
            if (String.IsNullOrEmpty(Object.Name))
            {
                messagePrefix = String.Format("{0}: ", PropertyDisplayNames.Patch);
            }
            else
            {
                messagePrefix = String.Format("{0} '{1}': ", PropertyDisplayNames.Patch, Object.Name);
            }

            foreach (Operator op in Object.Operators)
            {
                Execute(new RecursiveOperatorValidator(op, _curveRepository, _sampleRepository, alreadyDone));
            }
        }
    }
}
