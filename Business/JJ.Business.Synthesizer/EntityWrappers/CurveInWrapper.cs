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
    public class CurveInWrapper
    {
        private Operator _operator;
        private ICurveRepository _curveRepository;

        public CurveInWrapper(Operator op, ICurveRepository curveRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _operator = op;
            _curveRepository = curveRepository;
        }

        public int CurveID
        {
            get { return Int32.Parse(_operator.Data); }
            set { _operator.Data = value.ToString(); }
        }

        public Curve Curve 
        {
            get
            {
                return _curveRepository.TryGet(CurveID);
            }
            set
            {
                if (value == null)
                {
                    CurveID = 0;
                }
                else
                {
                    CurveID = value.ID;
                }
            }
        }

        public Outlet Result
        {
            get 
            {
                if (OperatorConstants.CURVE_IN_RESULT_INDEX >= _operator.Outlets.Count)
                {
                    throw new Exception(String.Format("_operator.Outlets does not have index [{0}].", OperatorConstants.CURVE_IN_RESULT_INDEX));
                }
                return _operator.Outlets[OperatorConstants.CURVE_IN_RESULT_INDEX];
            }
        }

        public static implicit operator Outlet(CurveInWrapper wrapper)
        {
            return wrapper.Result;
        }

        public static implicit operator Operator(CurveInWrapper wrapper)
        {
            return wrapper._operator;
        }
    }
}
