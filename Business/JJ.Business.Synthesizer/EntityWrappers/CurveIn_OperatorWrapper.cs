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
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CurveIn_OperatorWrapper : OperatorWrapperBase
    {
        private ICurveRepository _curveRepository;

        public CurveIn_OperatorWrapper(Operator op, ICurveRepository curveRepository)
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
            get { return GetOutlet(OperatorConstants.CURVE_IN_RESULT_INDEX); }
        }

        public static implicit operator Outlet(CurveIn_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
