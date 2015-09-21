using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Curve : OperatorWrapperBase
    {
        private ICurveRepository _curveRepository;

        public OperatorWrapper_Curve(Operator op, ICurveRepository curveRepository)
            : base(op)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            _curveRepository = curveRepository;
        }

        public int? CurveID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
        }

        /// <summary> nullable </summary>
        public Curve Curve
        {
            get
            {
                int? curveID = CurveID;
                if (!curveID.HasValue)
                {
                    return null;
                }

                return _curveRepository.TryGet(curveID.Value);
            }
            set
            {
                if (value == null)
                {
                    CurveID = null;
                    return;
                }

                CurveID = value.ID;
            }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.CURVE_OPERATOR_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Curve wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
