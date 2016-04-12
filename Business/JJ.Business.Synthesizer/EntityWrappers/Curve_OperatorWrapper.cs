using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Curve_OperatorWrapper : OperatorWrapperBase
    {
        private const int RESULT_INDEX = 0;

        private ICurveRepository _curveRepository;

        public Curve_OperatorWrapper(Operator op, ICurveRepository curveRepository)
            : base(op)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            _curveRepository = curveRepository;
        }

        public int? CurveID
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.CurveID); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.CurveID, value); }
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
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Curve_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
