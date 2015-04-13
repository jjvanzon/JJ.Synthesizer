using JJ.Business.Synthesizer.Constants;
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

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SampleOperatorWrapper
    {
        private Operator _operator;
        private ISampleRepository _sampleRepository;

        public SampleOperatorWrapper(Operator op, ISampleRepository sampleRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _operator = op;
            _sampleRepository = sampleRepository;
        }

        public int SampleID
        {
            get { return Int32.Parse(_operator.Data); }
            set { _operator.Data = value.ToString(); }
        }

        public Sample Sample
        {
            get
            {
                return _sampleRepository.TryGet(SampleID);
            }
            set
            {
                if (value == null)
                {
                    SampleID = 0;
                }
                else
                {
                    SampleID = value.ID;
                }
            }
        }

        public Outlet Result
        {
            get 
            {
                if (OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX >= _operator.Outlets.Count)
                {
                    throw new Exception(String.Format("_operator.Outlets does not have index [{0}].", OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX));
                }

                return _operator.Outlets[OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX]; 
            }
        }

        public static implicit operator Outlet(SampleOperatorWrapper wrapper)
        {
            return wrapper.Result;
        }

        public static implicit operator Operator(SampleOperatorWrapper wrapper)
        {
            return wrapper._operator;
        }
    }
}
