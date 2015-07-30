using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Exceptions;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    public class PatchValidator_Recursive : FluentValidator<Patch>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IDocumentRepository _documentRepository;
        private HashSet<object> _alreadyDone;

        public PatchValidator_Recursive(
            Patch obj, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository, 
            HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            foreach (Operator op in Object.Operators)
            {
                Execute(new OperatorValidator_IsCircular(op));
                Execute(new OperatorValidator_Recursive(op, _curveRepository, _sampleRepository, _documentRepository, _alreadyDone));
            }
        }
    }
}
