using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.Curves;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary>
    /// Validates a root operator and its descendant operators.
    /// Also validates related curves and samples.
    /// Makes sure that objects are only validated once to 
    /// prevent problems with circularities.
    /// </summary>
    internal class Recursive_OperatorValidator : ValidatorBase<Operator>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IPatchRepository _patchRepository;
        private HashSet<object> _alreadyDone;

        /// <summary>
        /// Validates a root operator and its descendant operators.
        /// Also validates related curves and samples.
        /// Makes sure that objects are only validated once to 
        /// prevent problems with circularity.
        /// </summary>
        public Recursive_OperatorValidator(
            Operator obj, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository, 
            HashSet<object> alreadyDone)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _patchRepository = patchRepository;
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

            // Message prefix pattern broken here on purpose.
            // This to prevent long message prefixes due to recursive processing.
            string operatorMessagePrefix = ValidationHelper.GetMessagePrefix(op, _sampleRepository, _curveRepository, _patchRepository);

            ExecuteValidator(new Versatile_OperatorValidator(op, _patchRepository), operatorMessagePrefix);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.Curve)
            {
                int curveID;
                if (Int32.TryParse(op.Data, out curveID))
                {
                    Curve curve = _curveRepository.TryGet(curveID);
                    if (curve != null)
                    {
                        if (!_alreadyDone.Contains(curve))
                        {
                            _alreadyDone.Add(curve);

                            string curveMessagePrefix = ValidationHelper.GetMessagePrefix(curve);

                            ExecuteValidator(new CurveValidator_WithoutNodes(curve), curveMessagePrefix);
                            ExecuteValidator(new CurveValidator_Nodes(curve), curveMessagePrefix);
                        }
                    }
                }
            }

            if (operatorTypeEnum == OperatorTypeEnum.Sample)
            {
                int sampleID;
                if (Int32.TryParse(op.Data, out sampleID))
                {
                    Sample sample = _sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        if (!_alreadyDone.Contains(sample))
                        {
                            _alreadyDone.Add(sample);
                            ExecuteValidator(new SampleValidator(sample), ValidationHelper.GetMessagePrefix(sample));
                        }
                    }
                }
            }
            
            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    // Message prefix not used here on purpose. 
                    // This to prevent long message prefixes due to recursive processing.
                    ExecuteValidator(new Recursive_OperatorValidator(inlet.InputOutlet.Operator, _curveRepository, _sampleRepository, _patchRepository, _alreadyDone));
                }
            }
        }
    }
}
