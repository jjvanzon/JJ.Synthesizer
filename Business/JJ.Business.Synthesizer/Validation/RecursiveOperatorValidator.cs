using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Validation.Entities;
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
    /// <summary>
    /// Validates a root operator and its descendant operators.
    /// Also validates related curves and samples.
    /// Makes sure that objects are only validated once to 
    /// prevent problems with circularities.
    /// </summary>
    public class RecursiveOperatorValidator : ValidatorBase<Operator>
    {
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private ISet<object> _alreadyDone;

        /// <summary>
        /// Validates a root operator and its descendant operators.
        /// Also validates related curves and samples.
        /// Makes sure that objects are only validated once to 
        /// prevent problems with circularity.
        /// </summary>
        public RecursiveOperatorValidator(Operator obj, ICurveRepository curveRepository, ISampleRepository sampleRepository, ISet<object> alreadyDone = null)
            : base(obj, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone ?? new HashSet<object>();

            Execute();
        }

        protected override void Execute()
        {
            if (_alreadyDone.Contains(Object))
            {
                return;
            }
            _alreadyDone.Add(Object);

            string messagePrefix = String.Format("{0} '{1}': ", Object.OperatorTypeName, Object.Name);

            Execute(new VersatileOperatorValidator(Object), messagePrefix);

            if (String.Equals(Object.OperatorTypeName, PropertyNames.CurveIn))
            {
                int curveID;
                if (Int32.TryParse(Object.Data, out curveID))
                {
                    Curve curve = _curveRepository.TryGet(curveID);
                    if (curve != null)
                    {
                        if (!_alreadyDone.Contains(curve))
                        {
                            _alreadyDone.Add(curve);
                            Execute(new CurveValidator(curve));
                        }
                    }
                }
            }

            // TODO: Remove outcommented code.
            //CurveIn curveIn = Object.AsCurveIn;
            //if (curveIn != null)
            //{
            //    Curve curve = curveIn.Curve;
            //    if (curve != null)
            //    {
            //        if (!_alreadyDone.Contains(curve))
            //        {
            //            _alreadyDone.Add(curve);
            //            Execute(new CurveValidator(curve));
            //        }
            //    }
            //}

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
                            Execute(new SampleValidator(sample));
                        }
                    }
                }
            }

            //SampleOperator sampleOperator = Object.AsSampleOperator;
            //if (sampleOperator != null)
            //{
            //    Sample sample = sampleOperator.Sample;
            //    if (sample != null)
            //    {
            //        if (!_alreadyDone.Contains(sample))
            //        {
            //            _alreadyDone.Add(sample);
            //            Execute(new SampleValidator(sample));
            //        }
            //    }
            //}
            
            foreach (Inlet inlet in Object.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    Execute(new RecursiveOperatorValidator(inlet.InputOutlet.Operator, _curveRepository, _sampleRepository, _alreadyDone));
                }
            }
        }
    }
}
