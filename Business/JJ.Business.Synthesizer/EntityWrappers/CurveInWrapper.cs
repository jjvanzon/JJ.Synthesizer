using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CurveInWrapper
    {
        private CurveIn _curveIn;

        public CurveInWrapper(CurveIn curveIn)
        {
            if (curveIn == null) throw new NullException(() => curveIn);
            _curveIn = curveIn;
        }

        public Curve Curve
        {
            get { return _curveIn.Curve; }
            set { _curveIn.Curve = value; }
        }

        public Outlet Result
        {
            get 
            {
                if (OperatorConstants.CURVE_IN_RESULT_INDEX >= _curveIn.Operator.Outlets.Count)
                {
                    throw new Exception(String.Format("_operator.Outlets does not have index [{0}].", OperatorConstants.CURVE_IN_RESULT_INDEX));
                }
                return _curveIn.Operator.Outlets[OperatorConstants.CURVE_IN_RESULT_INDEX];
            }
        }

        public static implicit operator Outlet(CurveInWrapper wrapper)
        {
            return wrapper.Result;
        }

        //public static implicit operator CurveIn(CurveInWrapper wrapper)
        //{
        //    return wrapper._curveIn;
        //}

        public static implicit operator Operator(CurveInWrapper wrapper)
        {
            return wrapper._curveIn.Operator;
        }
    }
}
