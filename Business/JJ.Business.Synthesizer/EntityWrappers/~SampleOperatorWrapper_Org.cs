//using JJ.Business.Synthesizer.Constants;
//using JJ.Business.Synthesizer.Validation.Entities;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Persistence.Synthesizer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.EntityWrappers
//{
//    public class SampleOperatorWrapper
//    {
//        private SampleOperator _sampleOperator;

//        public SampleOperatorWrapper(SampleOperator sampleOperator)
//        {
//            if (sampleOperator == null) throw new NullException(() => sampleOperator);
//            _sampleOperator = sampleOperator;
//        }

//        public Sample Sample
//        {
//            get { return _sampleOperator.Sample; }
//            set { _sampleOperator.Sample = value; }
//        }

//        public Outlet Result
//        {
//            get 
//            {
//                if (OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX >= _sampleOperator.Operator.Outlets.Count)
//                {
//                    throw new Exception(String.Format("_operator.Outlets does not have index [{0}].", OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX));
//                }

//                return _sampleOperator.Operator.Outlets[OperatorConstants.SAMPLE_OPERATOR_RESULT_INDEX]; 
//            }
//        }

        //public static implicit operator Outlet(SampleOperatorWrapper wrapper)
        //{
        //    return wrapper.Result;
        //}

        //public static implicit operator SampleOperator(SampleOperatorWrapper wrapper)
        //{
        //    return wrapper._sampleOperator;
        //}

        //public static implicit operator Operator(SampleOperatorWrapper wrapper)
        //{
        //    return wrapper._sampleOperator.Operator;
        //}
//    }
//}
