using JJ.Business.Synthesizer.Constants;
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

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sample_OperatorWrapper : OperatorWrapperBase
    {
        private ISampleRepository _sampleRepository;

        public Sample_OperatorWrapper(Operator op, ISampleRepository sampleRepository)
            : base(op)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

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
            get { return GetOutlet(OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Sample_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
