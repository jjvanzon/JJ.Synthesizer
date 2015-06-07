using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Validation;
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

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Validates a root operator and its descendant operators.
    /// Also validates related curves and samples.
    /// Makes sure that objects are only validated once to 
    /// prevent problems with circularities.
    /// </summary>
    public class OperatorValidator_Recursive : ValidatorBase<Operator>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private HashSet<object> _alreadyDone;

        /// <summary>
        /// Validates a root operator and its descendant operators.
        /// Also validates related curves and samples.
        /// Makes sure that objects are only validated once to 
        /// prevent problems with circularity.
        /// </summary>
        public OperatorValidator_Recursive(Operator obj, ICurveRepository curveRepository, ISampleRepository sampleRepository, HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            if (_alreadyDone.Contains(op))
            {
                return;
            }
            _alreadyDone.Add(op);

            string messagePrefix = ValidationHelper.GetMessagePrefix(op.OperatorTypeName, op.Name);

            Execute(new OperatorValidator_Versatile(op), messagePrefix);

            if (String.Equals(op.OperatorTypeName, PropertyNames.CurveIn))
            {
                int curveID;
                if (Int32.TryParse(op.Data, out curveID))
                {
                    Curve curve = _curveRepository.TryGet(curveID);
                    if (curve != null)
                    {
                        Execute(new CurveValidator(curve, _alreadyDone));
                    }
                }
            }

            if (String.Equals(op.OperatorTypeName, PropertyNames.SampleOperator))
            {
                int sampleID;
                if (Int32.TryParse(op.Data, out sampleID))
                {
                    Sample sample = _sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        Execute(new SampleValidator(sample, _alreadyDone));
                    }
                }
            }
            
            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    Execute(new OperatorValidator_Recursive(inlet.InputOutlet.Operator, _curveRepository, _sampleRepository, _alreadyDone));
                }
            }
        }
    }
}
