using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SampleOperatorWrapper
    {
        private SampleOperator _sampleOperator;

        public SampleOperatorWrapper(SampleOperator sampleOperator)
        {
            if (sampleOperator == null) throw new NullException(() => sampleOperator);
            _sampleOperator = sampleOperator;
        }

        public Sample Sample
        {
            get { return _sampleOperator.Sample; }
            set { _sampleOperator.Sample = value; }
        }

        public Outlet Result
        {
            get { return _sampleOperator.Operator.Outlets[OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX]; }
        }

        public static implicit operator Outlet(SampleOperatorWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}
